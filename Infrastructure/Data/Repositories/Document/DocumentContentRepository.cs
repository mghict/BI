using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class DocumentContentRepository : GenericRepository<DocumentContent>, IDocumentContentRepository
{
    public DocumentContentRepository(PowerBiContext context) : base(context)
    {

    }
}