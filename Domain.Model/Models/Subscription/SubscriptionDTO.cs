using Moneyon.PowerBi.Domain.Model.Common;
using Moneyon.PowerBi.Domain.Modeling;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class SubscriptionShortDto
{
    public long Id { get; set; }
    public SubscriptionType SubscriptionType { get; set; }
    public bool Active { get; set; }
    public string PackageName { get; set; }
    public DateTime CreateOn { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? ExpireDate { get; set; }
    public PersonShortDto Owner { get; set; }

}


public class SubscriptionDto
{
    public SubscriptionType SubscriptionType { get; set; }
    public bool? Active { get; set; }
    public long? PackageId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? ExpireDate { get; set; }
    public long? PurchesId { get; set; }


    public ShowPackagesDto Package { get; set; }
    public PersonShortDto Owner { get; set; }

    public PurchesShortToSubscriptionDto Purches { get; set; }
    public DateTime CreateOn { get; set; }
    public DateTime? ModifiedOn { get; set; }

    public ICollection<ReportShowDto>? Reports { get; set; }

    
}