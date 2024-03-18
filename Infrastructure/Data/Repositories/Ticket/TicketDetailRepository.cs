using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

internal class TicketDetailRepository : GenericRepository<TicketDetail>, ITicketDetailRepository
{

    public TicketDetailRepository(PowerBiContext context) : base(context)
    {
    }
}
