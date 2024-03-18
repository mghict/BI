using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(PowerBiContext context) : base(context)
    {

    }
}
