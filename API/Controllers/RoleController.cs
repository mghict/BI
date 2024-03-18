using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;
using Moneyon.PowerBi.API.Controllers.Base;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Moneyon.PowerBi.API.Controllers;

[Route("api/role")]
[ApiController]
public class RoleController:AppBaseController
{
    private readonly RoleService _roleService;

    public RoleController(IHttpContextAccessor contextAccessor, RoleService roleService)
        :base(contextAccessor)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.RolesView })]
    public async Task<DataResult<ShortRoleDto>> GetRoles([FromQuery]DataRequest request)
    {
        return await _roleService.GetAllPagable(request);
    }

    [HttpGet]
    [Route("all")]
    [JWTAuthorization()]
    public async Task<IEnumerable<ShortRoleDto>> GetRolesAll()
    {
        return await _roleService.GetAll();
    }

    [HttpGet]
    [Route("{roleId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.RolesView })]
    public async Task<RoleDto> GetRole(long roleId)
    {
        return await _roleService.GetRoleById(roleId);
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.RolesCreate })]
    public async Task CreateRole(RoleCreateDto dto)
    {
        await _roleService.CreateRole(dto);
    }

    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.RolesEdit })]
    public async Task UpdateRole(RoleUpdateDto dto)
    {
        await _roleService.UpdateRole(dto);
    }




}