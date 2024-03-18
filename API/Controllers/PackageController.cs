using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Moneyon.PowerBi.API.Controllers;

[Route("api/package")]
[ApiController]
public class PackageController : ControllerBase
{
    private readonly PackageService _packageService;

    public PackageController(PackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.PackagesView})]
    public async Task<DataResult<PackageDto>> PackageList([FromQuery] DataRequest req)
    {
        return await _packageService.PackageList(req);
    }

    [HttpGet]
    [Route("{packageId}")]
    [JWTAuthorization()]
    public async Task<PackageDto> PackageById(long packageId)
    {
        return await _packageService.GetPackage(packageId);
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.PackagesCreate })]
    public async Task CreatePackage(PackageCreateReqDto dto)
    {
        await _packageService.CreatePackage(dto);
    }

    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.PackagesEdit })]
    public async Task UpdatePackage(PackageEditReqDto dto)
    {
        await _packageService.UpdatePackage(dto);
    }

    [HttpGet]
    [Route("packages")]
    [JWTAuthorization()]
    public async Task<IEnumerable<PackageDto>> PackagesList()
    {
        return await _packageService.GetPackages();
    }

    [HttpGet]
    [Route("show-packages")]
    public async Task<IEnumerable<ShowPackagesDto>> ShowPackages()
    {
        return await _packageService.ShowPackages();
    }
}
