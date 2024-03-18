using Moneyon.Common.File.SpreadSheet;
using Moneyon.PowerBi.Domain.Model.Common;
using Moneyon.PowerBi.Domain.Modeling;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class PurchesShortToSubscriptionDto
{
    public string TrackingCode { get; set; }
    public TransactionDetailSummaryDto Transaction { get; set; }
    public DateTime CreateOn { get; set; }
    public DateTime? ModifiedOn { get; set; }

    [WorkSheetCoulmnAttribute(Header = "بدهکار")]
    public long Debit { get; set; }

    [WorkSheetCoulmnAttribute(Header = "بستانکار")]
    public long Credit { get; set; }

    [WorkSheetCoulmnAttribute(Header = "موجودی")]
    public long AccountBalance { get; set; }
}


public class PurchesShortDto
{
    public string TrackingCode { get; set; }
    public DateTime CreateOn { get; set; }
    public long Debit { get; set; }
    public long Credit { get; set; }
    public long AccountBalance { get; set; }
    public string PackageName { get; set; }
}


