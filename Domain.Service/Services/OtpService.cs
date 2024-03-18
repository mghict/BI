using AutoMapper;
using Domain.Model.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Service.IServices;



namespace Moneyon.PowerBi.Domain.Service.Services;



//[AutoRegister(typeof(IOtpService))]
[AutoRegister]
public class OtpService //: IOtpService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;
    private readonly ISmsService _smsService;
    public OtpService(IUnitOfWork uw, IMapper mp, ISmsService smsService)
    {
        _uw = uw;
        _mp = mp;
        _smsService = smsService;
    }

    public async Task SendOtp(string mobileNumber, long userId)
    {
        var otp = new OTP(OTPType.SignIn, mobileNumber, userId);
        await _uw.OTPRepository.InsertAsync(otp);
        await _uw.CommitAsync();

        await _smsService.SendOTP(mobileNumber, otp.Value);
    }

    public async Task<OTP> ValidateOtp(string mobileNumber,string otpValue)
    {
        var otp= await _uw.OTPRepository.FirstOrDefaultAsync(filter:p=>p.MobileNumber == mobileNumber &&
                                                                      p.Type==OTPType.SignIn && 
                                                                      p.IsUsed==false &&
                                                                      p.Value.Trim()==otpValue.Trim(),
                                                              orderBy:p=>p.OrderByDescending(d=>d.SendDate)
                                                            );
        if (otp is null)
            throw new BizException(BizExceptionCode.OtpIsNotValid);

        if (otp.SendDate.AddMinutes(2) < DateTime.Now)
            throw new BizException(BizExceptionCode.ReceiveOtpLimit);

        //otp.IsUsed = true;
        //await _uw.CommitAsync();
        return otp;
    }
}
