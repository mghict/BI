using System.ComponentModel;

namespace Moneyon.PowerBi.Domain.Modeling;

/// <summary>
/// وضعیت پرداخت
/// </summary>
public enum PaymentStatus
{
    [Description("در انتظار")]
    Pending = 0,
    [Description("موفق")]
    Success = 1,
    [Description("دارای خطا")]
    Failed = 2,
    [Description("تایید نشده")]
    Rejected = 3
}
