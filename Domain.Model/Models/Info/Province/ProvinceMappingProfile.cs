namespace Moneyon.PowerBi.Domain.Modeling;

public class ProvinceMappingProfile : AutoMapper.Profile
{
    public ProvinceMappingProfile()
    {
        CreateMap<Province, ShortProvinceDto>().ReverseMap();
        CreateMap<Province, ProvinceDto>();
    }
}

