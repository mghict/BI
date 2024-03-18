using Moneyon.Common.ExceptionHandling;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Model.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Modeling;

[Table("Payments")]
public class Payment : AppEntity, IAudit
{
    public string TrackingCode { get; set; }
    public DateTime CreateOn { get; set; }
    public DateTime? ModifiedOn { get; set; }

    public long OwnerId { get; set; }
    public Person Owner { get; set; }
    public PaymentStatus Status { get; set; }
    public long Amount { get; set; }
    public string? Description { get; set; }

    public long? TransactionId { get; set; }
    public Transaction? Transaction { get; set; }

    public long AccountId { get; set; }
    //public Account Account { get; set; }
    public long? PackageId { get; set; }

    //[ForeignKey("PackageId")]
    //public Package? Package { get; set; }

    public Payment()
    {
        TrackingCode=DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
    }

    public Payment(long ownerId,long accountId, long amount,long packageId, string? description):
        this()
    {
        OwnerId = ownerId;
        Amount = amount;
        Description = description;
        AccountId = accountId;
        Status= PaymentStatus.Pending;
        PackageId = packageId;
    }

    public void EnsureStatus(PaymentStatus status)
    {
        if (Status != status)
            throw new BizException(BizExceptionCode.InvalidStatus);
    }
}


public class PaymentCreateDto
{
    public long Amount { get; set; }
    public long PackageId { get; set; }
    public long AccountId { get; set; }

    public PaymentCreateDto(long amount, long packageId, long accountId)
    {
        Amount = amount;
        PackageId = packageId;
        AccountId = accountId;
    }
}

public class PaymentDto
{
    public string TrackingCode { get; set; }
    public DateTime CreateOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public PersonShortDto? Owner { get; set; }
    public PaymentStatus Status { get; set; }
    public long Amount { get; set; }
    public string? Description { get; set; }
    public ShowPackagesDto? Package { get; set; }

}

public class PaymentWithImageDto
{
    public string TrackingCode { get; set; }
    public DateTime CreateOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public PersonShortDto? Owner { get; set; }
    public PaymentStatus Status { get; set; }
    public long Amount { get; set; }
    public string? Description { get; set; }
    public ShowPackagesDto? Package { get; set; }
    public DocumentDto? Document { get; set; }

}
