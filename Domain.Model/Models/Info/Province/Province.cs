using DocumentFormat.OpenXml.Bibliography;
using Moneyon.Common.Data;
using Moneyon.PowerBi.Domain.Model.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Modeling;

[Table("Provinces")]
public class Province : AppEntity
{

    [MaxLength(150)]
    public string ProvinceName { get; set; }
    public long CountryId { get; set; }

    [ForeignKey("CountryId")]
    public Country Country { get; set; }
    public IEnumerable<City> Cities { get; set; }


}

