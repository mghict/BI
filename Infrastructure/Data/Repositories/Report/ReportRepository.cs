using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class ReportRepository : GenericRepository<Report> , IReportRepository
{
    public ReportRepository(PowerBiContext context) : base(context)
    {
        
    }
}
