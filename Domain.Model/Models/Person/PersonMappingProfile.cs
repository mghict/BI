using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Domain.Modeling;

[ObjectMapper]
public class PersonMappingProfile : AutoMapper.Profile
{
    public PersonMappingProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(des=>des.Id,src=>src.MapFrom(src=>src.Code))
            .ForMember(des=>des.Roles,src=>src.MapFrom(src=>src.User!.Roles))
            .ForMember(des => des.Addresses, src => src.MapFrom(src => src.Addresses!.LastOrDefault()));

        CreateMap<Person, PersonShortDto>()
            .ForMember(des => des.Id, src => src.MapFrom(src => src.Code));

        CreateMap<Person, ShortPersonDto>()
            .ForMember(des => des.Id, src => src.MapFrom(src => src.Code ))
            .ForMember(des => des.UserName, src => src.MapFrom(src => src.User!.UserName ))
            .ForMember(des => des.IsActive, src => src.MapFrom(src => src.User!.IsActive ));

        CreateMap<UserRegisterDto, Person>();

        CreateMap<EditProfileDto, Person>()
            .ForMember(des => des.Addresses, src => src.Ignore());

        CreateMap< Person, UserIdentityDto>()
            .ForMember(des => des.Roles, src => src.MapFrom(src => src.User!.GetRoles().ToArray()))
            .ForMember(des => des.Permissions, src => src.MapFrom(src => src.User!.GetPermission().ToArray()))
            .ForMember(des => des.DisplayName, src => src.MapFrom(src => src.DisplayName))
            .ForMember(des => des.IsActive, src => src.MapFrom(src => src.User!.IsActive))
            .ForMember(des => des.UserName, src => src.MapFrom(src => src.User!.UserName)); 


    }
}