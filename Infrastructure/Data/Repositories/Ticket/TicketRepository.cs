using Infrastructure.Data;
using Moneyon.Common.Data;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Domain.Model.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(PowerBiContext context) : base(context)
    {

    }
}
