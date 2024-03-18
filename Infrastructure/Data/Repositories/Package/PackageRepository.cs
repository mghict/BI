using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class PackageRepository : GenericRepository<Package> , IPackageRepository
{
    public PackageRepository(PowerBiContext context) : base(context)
    {
            
    }
}
