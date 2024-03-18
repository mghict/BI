using Moneyon.PowerBi.Domain.Modeling;

namespace Infrastructure.Data.Repositories;

public class UserTokenRepository : GenericRepository<UsersToken>, IUsersTokenRepository
{
    public UserTokenRepository(PowerBiContext context) : base(context)
    {
    }
}