using System.ComponentModel;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public enum TicketStatus
{

    All = 0,
    [Description("در انتظار بررسی")]
    AwaitingForReview = 1,
    [Description("پاسخ داده شده")]
    Answered = 2,
    [Description("بسته شده")]
    Closed = 3
}
