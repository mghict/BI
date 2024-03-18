using Domain.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;
using Moneyon.PowerBi.API.Controllers.Base;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.API.Controllers;

[Route("api/report")]
[ApiController]
public class ReportController : AppBaseController
{
    private readonly ReportService _reportService;

    public ReportController(IHttpContextAccessor contextAccessor, ReportService reportService)
        : base(contextAccessor)
    {
        _reportService = reportService;
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] {PermissionEnum.ReportView})]
    public async Task<DataResult<ReportDto>> GetReports([FromQuery] DataRequest request)
    {
        return await _reportService.GetReports(request);
    }

    [HttpGet]
    [Route("{reportId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ReportView })]
    public async Task<ReportDto> GetReport(long reportId)
    {
        return await _reportService.GetReport(reportId);
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ReportCreate })]
    public async Task CreateReport(ReportCreateDto dto)
    {
        await _reportService.CreateReport(dto);
    }

    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ReportEdit })]
    public async Task UpdateReport(ReportEditDto dto)
    {
        await _reportService.EditReport(dto);
    }
}
