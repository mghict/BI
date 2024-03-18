using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class IPGPaymentRepository : GenericRepository<IpgPayment>, IIpgPaymentRepository
{
    public IPGPaymentRepository(PowerBiContext context) : base(context)
    {

    }
}