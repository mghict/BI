using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(PowerBiContext context) : base(context)
    {

    }
}
