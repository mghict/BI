using Moneyon.PowerBi.Common.ObjectMapper;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

[ObjectMapper]
public class UserMappingProfile : AutoMapper.Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserRegisterDto>().ReverseMap();

        CreateMap<User, UserIdentityDto>()
            .ForMember(des => des.Roles, src => src.MapFrom(src => src.GetRoles().ToArray()))
            .ForMember(des => des.Permissions, src => src.MapFrom(src =>src.GetPermission().ToArray()))
            .ForMember(des => des.DisplayName, src => src.MapFrom(src => src.Person!.DisplayName))
            .ForMember(des => des.UserName, src => src.MapFrom(src => src.UserName));
    }
}
