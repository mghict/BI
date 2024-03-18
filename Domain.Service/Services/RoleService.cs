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
using System.Diagnostics.CodeAnalysis;
using System.Security;

namespace Moneyon.PowerBi.Domain.Service.Services;

[AutoRegister]
public class RoleService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public RoleService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task CreateRole(RoleCreateDto dto)
    {
        var existsTitle = await _uw.RoleRepository.AnyAsync(p => p.Title.Trim() == dto.Title.Trim());
        if (existsTitle) throw new BizException(BizExceptionCode.NameIsExists);

        var role = _mp.Map<Role>(dto);

        var permissions = await GetPermission(dto.PermissionId);


        if (permissions.Count() > 0)
            role.Permissions = permissions.ToList();

        await _uw.RoleRepository.InsertAsync(role);
        await _uw.CommitAsync();
    }

    public async Task UpdateRole(RoleUpdateDto dto)
    {
        var existsTitle = await _uw.RoleRepository.AnyAsync(filter:p => p.Title.Trim() == dto.Title.Trim() && p.Id != dto.Id);
        if (existsTitle) throw new BizException(BizExceptionCode.NameIsExists);

        var role = await _uw.RoleRepository.FirstOrDefaultAsync(_ => _.Id == dto.Id,include: i=>i.Include(p=>p.Permissions!));
        if (role is null) throw new BizException(BizExceptionCode.DataNotFound);

        _mp.Map<Role>(dto);
        
        role.Permissions.Clear();

        var permissions = await GetPermission(dto.PermissionId);
        if (permissions.Count() > 0)
            role.Permissions = permissions.ToList();

        await _uw.CommitAsync();
    }

    public async Task<DataResult<ShortRoleDto>> GetAllPagable(DataRequest dataRequest)
    {
        var roles = await _uw.RoleRepository.ReadPagableAsync(dataRequest);
        return _mp.MapDataResult<Role, ShortRoleDto>(roles);
    }

    public async Task<IEnumerable<ShortRoleDto>> GetAll()
    {
        var roles = await _uw.RoleRepository.ReadAsync();
        return _mp.MapCollection<Role, ShortRoleDto>(roles);
    }

    public async Task<RoleDto> GetRoleById(long id)
    {
        var role = await _uw.RoleRepository.FirstOrDefaultAsync(filter: _ => _.Id == id,
                                                                include: i => i.Include(per => per.Permissions!));
        if (role is null) throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<RoleDto>(role);
    }

    private async Task<IEnumerable<Permission>> GetPermission(IEnumerable<long>? _permission)
    {

        List<Permission> permissions = new List<Permission>();
        foreach (var per in _permission!)
        {
            var permission = await _uw.PermissionRepository.FirstOrDefaultAsync(p => p.PermisionId == per);
            if (permission is not null)
            {
                permissions.Add(permission);
            }
        }

        return permissions;


    }
}