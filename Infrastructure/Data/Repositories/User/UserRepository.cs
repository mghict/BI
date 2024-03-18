using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Infrastructure.Data.Repositories;

public class UserRepository : GenericRepository<User>,  IUserRepository 
{
    public UserRepository(PowerBiContext context) : base (context)
    {
    }
}
