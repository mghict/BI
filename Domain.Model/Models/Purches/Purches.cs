
using Microsoft.AspNetCore.Http;
using Moneyon.PowerBi.Common.Attributes;
using Moneyon.PowerBi.Domain.Model.Common;
using Moneyon.PowerBi.Domain.Modeling;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class Purches : AppEntity,IAudit
{
    public long OwnerId { get; set; }
    public long? PackageId { get; set; }
    public long? SubscriptionId { get; set; }
    public string TrackingCode { get; set; }
    public long? TransactionId { get; set; }
    public Transaction? Transaction { get; set; }



    [ForeignKey("OwnerId")]
    public Person Owner { get; set; }

    [ForeignKey("PackageId")]
    public Package? Package { get; set; }

    [ForeignKey("SubscriptionId")]
    public Subscription? Subscription { get; set; }

    public DateTime CreateOn { get ; set ; }
    public DateTime? ModifiedOn { get ; set ; }

    public Purches()
    {
        TrackingCode = DateTime.UtcNow.ToString("yyyyMMddHHmmsszzz");
    }

    public Purches(string? trackingCode)
    {
        TrackingCode = trackingCode ?? DateTime.UtcNow.ToString("yyyyMMddHHmmsszzz");
    }

    public Purches(long ownerId, long? packageId,string? trackingCode=null)
        :this(trackingCode)
    {
        OwnerId = ownerId;
        PackageId = packageId;
    }

    public Purches(long ownerId, long packageId, Subscription subscription, string? trackingCode=null)
        :this(trackingCode)
    {
        OwnerId = ownerId;
        PackageId = packageId;
        Subscription = subscription;
    }
}


public class PurchesCreateDto
{

    [Required(ErrorMessage = " مبلغ الزامی است")]
    public long Amount { get; set; }
    public long AccountId { get; set; }
    public long PackageId { get; set; }

    
}


public class PurchesReceiptCreateDto
{

    public Guid ImageId { get; set; }

    [Required(ErrorMessage = " مبلغ الزامی است")]
    public long Amount { get; set; }
    public long PackageId { get; set; }


}

public class PurchesIpgCreateDto
{

    [Required(ErrorMessage = " مبلغ الزامی است")]
    public long Amount { get; set; }
    public long PackageId { get; set; }


}