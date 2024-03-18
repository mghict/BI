using AutoMapper;
using Domain.Model.Common.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Model.Modeling;


namespace Domain.Service.Services;

[AutoRegister]
public class ReportService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public ReportService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task CreateReport(ReportCreateDto dto)
    {
        var reportName=await _uw.ReportRepository.AnyAsync(p=>p.LatinName.Trim().ToLower()==dto.LatinName.ToLower().Trim());
        if (reportName) throw new BizException(BizExceptionCode.NameIsExists);

        var report = _mp.Map<Report>(dto);
        
        await _uw.ReportRepository.InsertAsync(report);
        await _uw.CommitAsync();
    }

    public async Task EditReport(ReportEditDto dto)
    {
        var reportName = await _uw.ReportRepository.AnyAsync(p => p.LatinName.Trim().ToLower() == dto.LatinName.ToLower().Trim() && p.Id!=dto.Id);
        if (reportName) throw new BizException(BizExceptionCode.NameIsExists);

        var report = await _uw.ReportRepository.FirstOrDefaultAsync(filter:p => p.Id == dto.Id,
                                                                    include: i=>i.Include(p=>p.Permission));
        if (report is null) throw new BizException(BizExceptionCode.DataNotFound);

        _mp.Map(dto,report);
        report.Permission.Description = dto.Title;
        report.Permission.Name = dto.LatinName;

        await _uw.CommitAsync();

    }

    public async Task<ReportDto> GetReport(long id)
    {
        var report = await _uw.ReportRepository.FirstOrDefaultAsync(p => p.Id == id);

        if (report is null) throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<ReportDto>(report);
    }

    public async Task<DataResult<ReportDto>> GetReports(DataRequest request)
    {
        var reports=await _uw.ReportRepository.ReadPagableAsync(request);
        return _mp.MapDataResult<Report,ReportDto>(reports);
    }
}