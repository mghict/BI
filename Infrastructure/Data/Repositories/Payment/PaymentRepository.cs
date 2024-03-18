using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(PowerBiContext context) : base(context)
    {

    }
}
