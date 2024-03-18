using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class ProvinceRepository : GenericRepository<Province>, IProvinceRepository
{
    public ProvinceRepository(PowerBiContext context) : base(context)
    {

    }
}
