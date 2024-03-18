using API.Common.Extensions;
using CaptchaConfigurations.ActionFilter;
using CaptchaConfigurations.Services;
using DocumentFormat.OpenXml.Office2021.Excel.RichDataWebImage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.ExceptionHandling;
using Moneyon.PowerBi.API.Common.JWT;
using Moneyon.PowerBi.API.Controllers.Base;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Moneyon.PowerBi.API.Controllers.Security;

[ApiController]
[Route("api/security")]
public class UserSecurityController : AppBaseController
{
    private readonly UserSecurityService _userSecurityService;
    private readonly ICaptchaServices _captchaService;
    private readonly JwtTokenService   _jwtTokenService;
    public UserSecurityController(IHttpContextAccessor contextAccessor, UserSecurityService userSecurityService, ICaptchaServices captchaService, JwtTokenService jwtTokenService)
        :base(contextAccessor)
    {
        _userSecurityService = userSecurityService;
        _captchaService = captchaService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpGet]
    [Route("get-captcha")]
    public async Task<FileContentResult> GetCaptcha()
    {
        return await _captchaService.GetCaptcha();
    }

    [HttpPost]
    [Route("user-register")]
    [ValidateCaptcha]
    public async Task UserRegister([FromBody] UserRegisterDto req)
    {
        var res = await _userSecurityService.RegisterUser(req);
        //return await _jwtTokenService.GenerateTokenKey(res);
    }

    [HttpPost]
    [Route("user-register-active")]
    public async Task<UserTokens> UserRegisterActive([FromBody] UserRegisterActiveDto req)
    {
        var res = await _userSecurityService.UserActiveByOtp(req);
        return await _jwtTokenService.GenerateTokenKey(res);
    }

    [HttpPost]
    [Route("login-by-password")]
    [ValidateCaptcha]
    public async Task<UserTokens> LoginByPass(UserLogInByPasswordDto dto)
    {
        var user=await _userSecurityService.UserLoginByPass(dto);
        return await _jwtTokenService.GenerateTokenKey(user);
    }

    [HttpPost]
    [Route("login-by-otp")]
    [Route("forget-pass")]
    [ValidateCaptcha]
    public async Task LoginByOtp(UserLogInByOTPDto dto)
    {
        await _userSecurityService.UserLoginByOtp(dto);
    }

    [HttpPost]
    [Route("login-by-otp-validate")]
    public async Task<UserTokens> LoginByOtpValidate(UserRegisterActiveDto dto)
    {
        var user=await _userSecurityService.UserLoginByOtpValidate(dto);
        return await _jwtTokenService.GenerateTokenKey(user);
    }

    [HttpPost]
    [Route("forget-pass-validate")]
    public async Task ForgetPassValidate(ForgetPassValidateDto dto)
    {
        await _userSecurityService.UserForgetPassValidate(dto);
    }

    [HttpGet]
    [Route("identity")]
    [JWTAuthorization()]
    public async Task<UserIdentityDto> GetIdentity()
    {
        if (User is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        return await _userSecurityService.GetUserIdentity(User);
    }

    [HttpPost]
    [Route("logout")]
    public async Task UserLogout()
    {
        await HttpContext.SignOutAsync();
    }

}
