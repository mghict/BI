namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class SubscriptionMappingProfile : AutoMapper.Profile
{
    public SubscriptionMappingProfile()
    {
        CreateMap<Subscription, SubscriptionShortDto>()
            .ForMember(des => des.PackageName, src => src.MapFrom(src => src.Package!.Name))
        ;

        CreateMap<Subscription, SubscriptionDto>();
    }
}
