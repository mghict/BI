using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(PowerBiContext context) : base(context)
    {

    }
}
