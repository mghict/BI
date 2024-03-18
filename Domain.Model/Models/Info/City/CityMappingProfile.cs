namespace Moneyon.PowerBi.Domain.Modeling;

public class CityMappingProfile : AutoMapper.Profile
{
    public CityMappingProfile()
    {
        CreateMap<City, CityDto>().ReverseMap();
    }
}
