using Moneyon.PowerBi.Domain.Model.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Modeling;

public class Address:AppEntity
{
    public long CityId { get; set; }

    [ForeignKey("CityId")]
    public City City { get; set; }

    [MaxLength(500)]
    public string AddressValue { get; set; }

    [MaxLength(20)]
    public string? Tel { get; set; }

    [MaxLength(20)]
    public string? PostalCode { get; set; }

    public long PersonId { get; set; }

    [ForeignKey("PersonId")]
    public Person Person { get; set; }

}
