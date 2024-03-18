using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class PurchesRepository : GenericRepository<Purches> , IPurchesRepository
{
    public PurchesRepository(PowerBiContext context) : base(context)
    {
        
    }
}
