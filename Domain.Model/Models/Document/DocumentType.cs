using System.ComponentModel;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

/// <summary>
/// نوع سند 
/// </summary>
public enum DocumentType
{

    [Description("تصویر فیش واریز")]
    ReceiptPicture = 0,

    [Description("تصویر تیکت")]
    TicketPicture = 1,

}