namespace Moneyon.PowerBi.Domain.Modeling;

public class AddressMappingProfile : AutoMapper.Profile
{
    public AddressMappingProfile()
    {
        CreateMap<Address, AddressDto>()
            .ForMember(des=>des.CityName,src=>src.MapFrom(src=>src.City!.CityName))
            .ForMember(des=>des.ProvinceName,src=>src.MapFrom(src=>src.City!.Province!.ProvinceName))
            .ForMember(des=>des.CountryName,src=>src.MapFrom(src=>src.City!.Province!.Country!.CountryName))
            .ForMember(des => des.ProvinceId, src => src.MapFrom(src => src.City!.Province!.Id))
            .ForMember(des => des.CountryId, src => src.MapFrom(src => src.City!.Province!.Country!.Id));

        CreateMap<AddressCreateDto, Address>();
            
    }
}
