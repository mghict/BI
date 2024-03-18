using AutoMapper;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<RoleCreateDto,Role>()
            .ForMember(des=>des.Permissions,src=>src.Ignore());

        CreateMap<RoleUpdateDto, Role>()
            .ForMember(des => des.Permissions, src => src.Ignore());

        CreateMap<Role, ShortRoleDto>().ReverseMap();

        CreateMap<Role, RoleDto>().ReverseMap();
    }
}
