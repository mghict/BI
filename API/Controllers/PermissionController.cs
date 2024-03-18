using DocumentFormat.OpenXml.Bibliography;
using Domain.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.PowerBi.API.Controllers.Base;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.API.Controllers;

[Route("api/permission")]
[ApiController]
public class PermissionController : AppBaseController
{
    private readonly PermissionService _permissionService;

    public PermissionController(IHttpContextAccessor contextAccessor, PermissionService permissionService)
        :base(contextAccessor)
    {
        _permissionService = permissionService;
    }

    [HttpGet]
    [Route("report")]
    [JWTAuthorization()]
    public async Task<IEnumerable<PermissionDto>?> GetAllPermissonReport()
    {
        return await _permissionService.GetAllPermissionReportAsync();
    }

    [HttpGet]
    [Route("access")]
    [JWTAuthorization()]
    public async Task<IEnumerable<PermissionDto>?> GetAllPermissonAccess()
    {
        return await _permissionService.GetAllPermissionAccessAsync();
    }
}
