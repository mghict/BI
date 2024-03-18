using System.ComponentModel;

namespace Moneyon.PowerBi.Domain.Modeling;

public enum TransactionType
{
    [Description("شارژ حساب")]
    AccountCharge = 1,
    [Description("خرید اشتراک")]
    PurchesSubscription = 2,
}
