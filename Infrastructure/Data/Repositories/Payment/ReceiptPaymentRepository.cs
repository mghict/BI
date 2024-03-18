using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class ReceiptPaymentRepository : GenericRepository<ReceiptPayment>, IReceiptPaymentRepository
{
    public ReceiptPaymentRepository(PowerBiContext context) : base(context)
    {

    }
}
