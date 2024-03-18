using Moneyon.Common.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Modeling;

[Table("Countries")]
public class Country : AppEntity
{ 
    public string CountryName { get; set; }
}
