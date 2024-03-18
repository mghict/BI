using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Model.Common
{
    public interface IAudit
    {
        [Column(TypeName ="datatime")]
        DateTime CreateOn { get; set; }

        [Column(TypeName = "datatime")]
        DateTime? ModifiedOn { get; set; }
    }
}
