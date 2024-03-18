using Microsoft.AspNetCore.Http;
using Moneyon.PowerBi.Common.Attributes;
using Moneyon.PowerBi.Common.Extensions;
using Moneyon.PowerBi.Domain.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

/// <summary>
/// پرداخت از طریق فیش بانکی
/// </summary>
/// 
[Table("Payments")]
public class ReceiptPayment : Payment
{
    public long ImageId { get; set; }

    /// <summary>
    /// نام شخص پرداخت کننده - قید شده در فیش بانکی
    /// </summary>
	[MaxLength(200)]
    public string ReceiptPayerName { get; set; }

    public ReceiptPayment()
    {

    }

    public ReceiptPayment(long ownerId, long accountId, long amount,long packageId,Document image, string? description) :
        base(ownerId,accountId, amount, packageId, description)
    {
        Image=image;
    }
    /// <summary>
    /// تصویر فیش بانکی
    /// </summary>
    public Document Image { get; set; }

}


public class ReceiptPaymentCreateDto
{
    public Guid ImageId { get; set; }
    public long Amount { get; set; }
    public long AccountId { get; set; }
    public long PackageId { get; set; }

    public ReceiptPaymentCreateDto(Guid image, long amount, long accountId, long packageId)
    {
        ImageId = image;
        Amount = amount;
        AccountId = accountId;
        PackageId = packageId;
    }
}

public class ReceiptPaymentChangeStatus
{
    public string TrackingCode { get; set; }
    public PaymentStatus Status { get; set; }
    public string? Description { get; set; } = string.Empty;
}
