using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

internal class TicketCategoryRepository : GenericRepository<TicketCategory>, ITicketCategoryRepository
{
    public TicketCategoryRepository(PowerBiContext context) : base(context)
    {
        
    }
}
