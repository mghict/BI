using AutoMapper;
using Domain.Model.Common.Data;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Model.Modeling;


namespace Domain.Service.Services;

[AutoRegister()]
public class PermissionService //: IPermissionService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public PermissionService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task<IEnumerable<PermissionDto>?> GetAllPermissionReportAsync()
    {
        var permissons= await _uw.PermissionRepository.ReadAsync(filter:p=>p.Type==PermissionType.Report);
        return _mp.MapCollection<Permission,PermissionDto>(permissons);
    }

    public async Task<IEnumerable<PermissionDto>?> GetAllPermissionAccessAsync()
    {
        var permissons = await _uw.PermissionRepository.ReadAsync(filter: p => p.Type == PermissionType.AccessForm ,
                                                                  orderBy: _=>_.OrderBy(o=>o.PermisionId));
        return _mp.MapCollection<Permission, PermissionDto>(permissons);
    }
}
