using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(PowerBiContext context) : base(context)
    {

    }
}


