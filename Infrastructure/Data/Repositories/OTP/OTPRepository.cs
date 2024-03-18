using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Infrastructure.Data.Repositories;

public class OTPRepository : GenericRepository<OTP> , IOTPRepository
{
    public OTPRepository(PowerBiContext context) : base(context)
    {
            
    }
}
