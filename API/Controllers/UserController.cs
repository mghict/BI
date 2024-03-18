using CaptchaConfigurations.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.PowerBi.API.Controllers.Base;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Moneyon.PowerBi.API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController:AppBaseController
{
    private readonly UserService userService;

    public UserController(IHttpContextAccessor contextAccessor, UserService userService)
        :base(contextAccessor)
    {
        this.userService = userService;
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.UsersView })]
    public async Task<DataResult<ShortPersonDto>> GetPersonsList([FromQuery]DataRequest request)
    {
        return await userService.GetPersons(request);
    }

    [HttpGet]
    [Route("{personId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.UsersView })]
    public async Task<PersonDto> GetPerson(Guid personId)
    {
        return await userService.GetPerson(personId);
    }

    [HttpPut]
    [Route("change-role")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.UsersChangeRoles})]
    public async Task EditRoles(EditPersonRoleDto dto)
    {
        await userService.EditPersonRole(dto);
    }

    [HttpPut]
    [Route("reset-pass")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.UsersResetPassword })]
    [ValidateCaptcha]
    public async Task AdminResetPass(AdminResetPasswordDto dto)
    {
        await userService.AdminUserPassReset(dto);
    }

    [HttpGet]
    [Route("profile")]
    [JWTAuthorization()]
    public async Task<PersonDto> GetProfile()
    {
        if (User is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        return await userService.GetUserProfile(User.PersonId);
    }

    [HttpPut]
    [Route("edit-profile")]
    [JWTAuthorization()]
    public async Task EditProfile(EditProfileDto dto)
    {
        if (User is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        await userService.EditProfile(dto,User.PersonId);
    }

    [HttpPut]
    [Route("reset-pass-profile")]
    [JWTAuthorization()]
    [ValidateCaptcha]
    public async Task UserResetPasswordProfile(ResetPasswordDto dto)
    {
        if (User is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        await userService.UserPassReset(dto, User.Id);
    }
}
