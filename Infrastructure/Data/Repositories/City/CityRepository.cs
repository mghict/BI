using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class CityRepository : GenericRepository<City>, ICityRepository
{
    public CityRepository(PowerBiContext context) : base(context)
    {

    }
}
