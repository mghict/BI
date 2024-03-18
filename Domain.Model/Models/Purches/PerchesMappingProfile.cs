namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class PurchesMappingProfile  : AutoMapper.Profile
{
    public PurchesMappingProfile()
    {
        CreateMap<Purches, PurchesShortToSubscriptionDto>();
    }
}
