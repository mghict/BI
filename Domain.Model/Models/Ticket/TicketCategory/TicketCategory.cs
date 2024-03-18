using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

[Table("TicketCategories")]
public class TicketCategory : AppEntity
{

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    public long? ParentId { get; set; }
    [ForeignKey("ParentId")]
    public TicketCategory? Parent { get; set; }
}
