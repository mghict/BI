using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class SubscriptionRepositorty : GenericRepository<Subscription> , ISubscriptionRepository
{
    public SubscriptionRepositorty(PowerBiContext context) : base(context)
    {
            
    }
}
