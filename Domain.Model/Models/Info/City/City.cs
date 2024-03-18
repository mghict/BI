using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Modeling;

[Table("City")]
public class City : AppEntity
{

    [MaxLength(150)]
    public string CityName { get; set; }
    public long ProvinceId { get; set; }

    [ForeignKey("ProvinceId")]
    public Province Province { get; set; }
}
