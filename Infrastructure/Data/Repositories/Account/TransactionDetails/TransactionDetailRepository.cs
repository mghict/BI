using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class TransactionDetailRepository : GenericRepository<TransactionDetail>, ITransactionDetailRepository
{
    public TransactionDetailRepository(PowerBiContext context) : base(context)
    {

    }
}


