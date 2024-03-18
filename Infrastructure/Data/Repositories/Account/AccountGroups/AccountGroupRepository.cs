using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class AccountGroupRepository : GenericRepository<AccountGroup>, IAccountGroupRepository
{
    public AccountGroupRepository(PowerBiContext context) : base(context)
    {

    }
}


