using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(PowerBiContext context) : base(context)
    {

    }
}

