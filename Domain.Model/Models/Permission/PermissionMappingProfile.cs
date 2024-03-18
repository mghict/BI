namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class PermissionMappingProfile : AutoMapper.Profile
{
    public PermissionMappingProfile()
    {
        CreateMap<Permission, PermissionDto>();
    }
}