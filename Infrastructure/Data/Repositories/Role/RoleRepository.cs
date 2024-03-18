using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Infrastructure.Data.Repositories;

public class RoleRepository : GenericRepository<Role> , IRoleRepository
{
    public RoleRepository(PowerBiContext context) : base(context)
    {
        
    }
}
