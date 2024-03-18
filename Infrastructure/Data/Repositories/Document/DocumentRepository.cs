using Infrastructure.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
{
    public DocumentRepository(PowerBiContext context) : base(context)
    {

    }
}
