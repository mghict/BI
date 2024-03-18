using AutoMapper;
using Domain.Model.Common.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Collections;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Domain.Service.Services;

[AutoRegister()]
public class PackageService //: IPackageService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public PackageService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task CreatePackage(PackageCreateReqDto req)
    {
        if (req.PermissionId is null || req.PermissionId.Count()==0)
            throw new BizException(BizExceptionCode.PermissionNotNull);

        var package = _mp.Map<Package>(req);

        if (await _uw.PackageRepository.AnyAsync(p => p.Name.Trim().ToLower() == req.Name.Trim().ToLower()))
            throw new BizException(BizExceptionCode.NameIsExists);

        if (req.PermissionId is not null)
        {
            var reports = (await _uw.PermissionRepository.ReadAsync(filter: x => req.PermissionId.Contains(x.PermisionId))).ToList();
            package.Reports = reports;
        }

        await _uw.PackageRepository.InsertAsync(package);
        await _uw.CommitAsync();
    }
    public async Task UpdatePackage(PackageEditReqDto req)
    {
        if (await _uw.PackageRepository.AnyAsync(p => p.Name.Trim().ToLower() == req.Name.Trim().ToLower() && p.Id != req.Id))
            throw new BizException(BizExceptionCode.NameIsExists);

        var package = await _uw.PackageRepository.FirstOrDefaultAsync(filter: p => p.Id == req.Id, include: i => i.Include(p => p.Reports));
        if (package is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        _mp.Map(req, package);

        foreach (var Permission in package.Reports)
        {
            package.Reports.Remove(Permission);
        }

        if ( req.PermissionId is not null)
        {
            var reports = (await _uw.PermissionRepository.ReadAsync(filter: x => req.PermissionId.Contains(x.PermisionId))).ToList();
            if (reports!.Count()>0)
            {
                package.Reports=reports;
            }
        }

        await _uw.CommitAsync();


    }
    public async Task DeletePackage(int packageId)
    {
        // اگر اشتراک فعال وجود داشت حذف نشود
        await _uw.PackageRepository.DeleteAsync(packageId);
        await _uw.CommitAsync();
    }
    public async Task<DataResult<PackageDto>> PackageList(DataRequest req)
    {
        var packages = await _uw.PackageRepository.ReadPagableAsync(req);
        return _mp.MapDataResult<Package, PackageDto>(packages);
    }
    public async Task<IEnumerable<PackageDto>> GetPackages()
    {
        var packages = await _uw.PackageRepository.ReadAsync();
        return _mp.MapCollection<Package, PackageDto>(packages);
    }
    public async Task<PackageDto> GetPackage(long id)
    {
        var package = await _uw.PackageRepository.FirstOrDefaultAsync(_ => _.Id == id, include: _ => _.Include(per => per.Reports!));
        if (package is null) throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<PackageDto>(package);
    }
    public async Task<IEnumerable<ShowPackagesDto>> ShowPackages()
    {
        var packages= await _uw.PackageRepository.ReadAsync(filter:p=>p.Active==true && p.Reports!.Any(o=>o.Type==PermissionType.Report), 
                                                            include: _=>_.Include(p=>p.Reports!).ThenInclude(p=>p.Report!),
                                                            orderBy: o=>o.OrderBy(p=>p.IsSuperiorDiscount).ThenByDescending(p=>p.Discount));

        return _mp.MapCollection<Package, ShowPackagesDto>(packages!.Where(p=>p.Reports!.Count()>0));
    }
}
