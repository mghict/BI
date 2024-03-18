using AutoMapper;
using DocumentFormat.OpenXml.Drawing;
using Domain.Model.Common.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Common.Extensions;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using Moneyon.PowerBi.Domain.Service.IServices;

namespace Moneyon.PowerBi.Domain.Service.Services;

//[AutoRegister(typeof(IUserSecurityService))]
[AutoRegister]
public class UserSecurityService// : IUserSecurityService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;
    private readonly OtpService _otpService;
    private readonly Random _rnd;
    //private readonly IHttpContextAccessor _httpContext;

    public UserSecurityService(IUnitOfWork uw, IMapper mp, OtpService otpService)
    {
        _uw = uw;
        _mp = mp;
        _rnd = new Random();
        //_httpContext = httpContext;
        _otpService = otpService;
    }

    //public async Task<SendOTPResponse> SendOTP(SendOTPRequest request)
    //{
    //    var user = await _uw.UserRepository.SingleOrDefaultAsync(x => x.UserName == request.Mobile);
    //    if (user is null)
    //    {
    //        user = new User(null, null, request.Mobile, null, false);
    //        await _uw.UserRepository.InsertAsync(user);
    //        await _uw.CommitAsync();
    //    }


    //    //send otp and 
    //    await OTPSendLimit(request.Mobile);
    //    var otpCode = (_rnd.Next(9999, 99999)).ToString();
    //    await _smsService.SendOTP(request.Mobile, otpCode);

    //    //insert Otp
    //    var otp = new OTP(OTPType.SignIn, request.Mobile, otpCode, user.Id);
    //    await _uw.OTPRepository.InsertAsync(otp);
    //    await _uw.CommitAsync();

    //    return new SendOTPResponse()
    //    {
    //        IsNewUser = (user is not null) ? false : true,
    //        Token = otp.Token
    //    };
    //}

    //public async Task OTPSendLimit(string mobile)
    //{
    //    var otp = await _uw.OTPRepository.ReadAsync(x => x.MobileNumber == mobile &&
    //                        x.IsUsed == false && (x.SendDate.AddMinutes(2) > DateTime.Now));
    //    if (otp.Count() > 0)
    //        throw new BizException(BizExceptionCode.SendOtpLimit);
    //}

    //public async Task OTPReceiveLimit(OTP otp)
    //{
    //    var isExpired = (otp.SendDate.AddMinutes(2) < DateTime.Now);

    //    if (isExpired)
    //        throw new BizException(BizExceptionCode.ReceiveOtpLimit);
    //}


    public async Task<UserIdentityDto> RegisterUser(UserRegisterDto registerReq)
    {
        var role = await _uw.RoleRepository.ReadAsync(filter:p => p.Title.Trim().ToLower() == Role.User.Trim().ToLower());

        var person = await _uw.PersonRepository.FirstOrDefaultAsync(p => p.NationalCode == registerReq.NationalCode, "User");
        if (person is not null && person.User is not null && person.User.IsActive)
        {
            throw new BizException(BizExceptionCode.UserExists);
        }

        if (person is not null && person.User is not null && !person.User.IsActive)
        {
            person.User.Password = registerReq.Password.Hash();
            await _uw.CommitAsync();

            await _otpService.SendOtp(registerReq.MobileNumber, person.User.Id);
            return _mp.Map<UserIdentityDto>(person);
        }

        Person personEntity = new Person()
        {
            MobileNumber = registerReq.MobileNumber,
            NationalCode = registerReq.NationalCode,
            User = new User(registerReq.MobileNumber, registerReq.Password, role.Where(_ => _.Title.Trim().ToLower() == Role.User.Trim().ToLower()).ToList(), false),
            Accounts=new List<Account>() { new Account(5, AccountKey.Wallet, $"کیف پول") }
        };

        await _uw.PersonRepository.InsertAsync(personEntity);
        await _uw.CommitAsync();

        await _otpService.SendOtp(registerReq.MobileNumber, personEntity.User.Id);

        person = await _uw.PersonRepository.FirstOrDefaultAsync(p => p.NationalCode == registerReq.NationalCode, "User,User.Roles,User.Roles.Permissions");

        return _mp.Map<UserIdentityDto>(person);
    }

    public async Task<UserIdentityDto> UserActiveByOtp(UserRegisterActiveDto dto)
    {
        DateTime today = DateTime.Now.Date;

        var person=await _uw.PersonRepository.FirstOrDefaultAsync(filter:p=>p.MobileNumber == dto.MobileNumber,
                                                          include:p=>p.Include(s=>s.User!)
                                                                      .ThenInclude(r=>r.Roles!)
                                                                      .ThenInclude(per=>per.Permissions!)
                                                                      .Include(p=>p.Subscription!.Where(p=>p.Active==true && 
                                                                                                           p.StartDate <= today && 
                                                                                                           (p.ExpireDate>=today || p.ExpireDate ==null )))
                                                                      .ThenInclude(p=>p.Package!)
                                                                      .ThenInclude(p=>p.Reports!));

        if (person is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        var otp=await _otpService.ValidateOtp(dto.MobileNumber, dto.Otp);
        
        otp.IsUsed = true;
        person!.User!.IsActive = true;

        await _uw.CommitAsync();

        return _mp.Map<UserIdentityDto>(person);
    }
    
    public async Task<UserIdentityDto> GetUserIdentityAsync(LoginDto dto)
    {
        var result = await _uw.UserRepository.FirstOrDefaultAsync(filter: p => p.UserName == dto.UserName && p.Password == dto.Password.Hash(),
                                                            includeProperties: "Person,Roles,Roles.Permissions");

        if (result is null) throw new BizException(BizExceptionCode.UserNotFound);

        return _mp.Map<UserIdentityDto>(result);
    }

    public async Task<UserHeaderDto?> GetPersonByUserName(string userName)
    {
        var result = await _uw.UserRepository.FirstOrDefaultAsync(filter: p => p.UserName == userName,
                                                            includeProperties: "Person,Roles,Roles.Permissions");

        return result is null ? null : new UserHeaderDto() { User = result };
    }

    public async Task<UsersToken?> GetLastTokenAsync(long userId)
    {

        var token=await _uw.UsersTokenRepository.FirstOrDefaultAsync(filter:p=>p.UserId == userId && p.IsExpire==false, orderBy: p=>p.OrderByDescending(c=>c.CreateDate));
        return token;

    }

    public async Task CreateUserToken(string userName, string token)
    {
        var user = await _uw.UserRepository.FirstOrDefaultAsync(p => p.UserName == userName);
        if (user is not null)
        {
            var userToken = new UsersToken()
            {
                Token = token,
                UserId = user.Id
            };
            await _uw.UsersTokenRepository.InsertAsync(userToken);
            await _uw.CommitAsync();
        }
    }

    public async Task<UserIdentityDto> UserLoginByPass(UserLogInByPasswordDto dto)
    {
        var person =await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.MobileNumber == dto.UserName &&
                                                                           p.User!.Password==dto.Password.Hash() &&
                                                                           p.User!.IsActive==true,
                                                              include: p => p.Include(s => s.User!)
                                                                      .ThenInclude(r => r.Roles!)
                                                                      .ThenInclude(per => per.Permissions!));

        if (person is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        return _mp.Map<UserIdentityDto>(person);
    }

    public async Task UserLoginByOtp(UserLogInByOTPDto dto)
    {
        var person =await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.MobileNumber == dto.UserName &&
                                                                           p.User!.IsActive == true,
                                                              include: p => p.Include(s => s.User!));

        if (person is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        await _otpService.SendOtp(person.MobileNumber, person.User!.Id);
    }

    public async Task<UserIdentityDto> UserLoginByOtpValidate(UserRegisterActiveDto dto)
    {
        var person = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.MobileNumber == dto.MobileNumber &&
                                                                            p.User!.IsActive == true,
                                                              include: p => p.Include(s => s.User!)
                                                                      .ThenInclude(r => r.Roles!)
                                                                      .ThenInclude(per => per.Permissions!));

        if (person is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        var otp = await _otpService.ValidateOtp(dto.MobileNumber, dto.Otp);

        otp.IsUsed = true;

        await _uw.CommitAsync();

        return _mp.Map<UserIdentityDto>(person);
    }

    public async Task UserForgetPassValidate(ForgetPassValidateDto dto)
    {
        var person = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.MobileNumber == dto.MobileNumber &&
                                                                                 p.User!.IsActive == true,
                                                              include: p => p.Include(s => s.User!)
                                                                      .ThenInclude(r => r.Roles!)
                                                                      .ThenInclude(per => per.Permissions!));

        if (person is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        var otp = await _otpService.ValidateOtp(dto.MobileNumber, dto.Otp);

        otp.IsUsed = true;
        person.User!.Password = dto.Password.Hash();

        await _uw.CommitAsync();

        //return _mp.Map<UserIdentityDto>(person);
    }

    public async Task<UserIdentityDto> GetUserIdentity(User? user)
    {
        if (user is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        var userEntity=await _uw.PersonRepository.FirstOrDefaultAsync(filter : p=>p.User!.Id == user.Id,
                                                                      include: i=>i.Include(u=>u.User!).ThenInclude(r=>r.Roles!).ThenInclude(p=>p.Permissions!));
        if (userEntity is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        return _mp.Map<UserIdentityDto>(userEntity);
    }

    public async Task<bool> CheckUserReportAccess(long reportId,User? user)
    {
        if (user is null) return false;
        if (user.GetPermission() == null) return false;

        var report = await _uw.ReportRepository.FirstOrDefaultAsync(filter:p => p.Id == reportId,
                                                                    include: i=>i.Include(p=>p.Permission));
        if (report is null) return false;

        

        return false;
    }

    //public async Task<UserLogInResDto> UserLogInByOtp(UserLogInByOTPDto loginreq)
    //{
    //    var user = await _uw.UserRepository.SingleAsync(x => x.Mobile == loginreq.Mobile, x => x.OTPs, x => x.Roles);
    //    var otp = user.OTPs.Single(x => x.Value == loginreq.Otp && x.Token == loginreq.Token && !x.IsUsed);
    //    await OTPReceiveLimit(otp);
    //    var userRoles = user.Roles.Select(X => X.Title).ToArray();

    //    return new UserLogInResDto()
    //    {
    //        UserId = user.Id,
    //        RolesTitles = userRoles
    //    };
    //}

    //public async Task<UserLogInResDto> UserLogInByPassword(UserLogInByPasswordDto loginReq)
    //{
    //    var user = await _uw.UserRepository.SingleAsync(x =>x.UserName == loginReq.UserName && x.Password == loginReq.Password.Hash(), "Roles,Roles.Permissions,Person");
    //    return new UserLogInResDto()
    //    {
    //        UserId = user.Id,
    //        RolesTitles = userRoles,
    //    };

    //}

    //public async Task<UserIdentityDto> UserIdentity(int userId)
    //{
    //    var user = await _uw.UserRepository.SingleAsync(filter: x => x.Id == userId, "Roles,Roles.Permissions,Person");            
    //    return new UserIdentityDto(user.UserName,user.Person.DisplayName, user.GetRoles().ToArray(), user.GetPermission().ToArray());
    //}

}
