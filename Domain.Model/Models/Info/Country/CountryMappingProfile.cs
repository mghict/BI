namespace Moneyon.PowerBi.Domain.Modeling;

public class CountryMappingProfile : AutoMapper.Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Country, CountryDto>().ReverseMap();
    }
}
