using System.ComponentModel;

namespace Moneyon.PowerBi.Domain.Modeling;

/// <summary>
/// کلید گروه حساب
/// </summary>
public enum AccountGroupKey
{
    /// <summary>
    /// دارآیی ها
    /// </summary>
    [Description("دارایی ها")]
    Assets = 1000,
    [Description("دارایی ها")]
    CurrentAssets = 1001,

    /// <summary>
    /// بدهی ها
    /// </summary>
    [Description("بدهی ها")]
    Liabilities = 2000,
    [Description("بدهی ها")]
    CurrentLiabilities = 2001,
    [Description("بدهی ها")]
    PeopleWallets = 2002,
    [Description("بدهی ها")]
    PlatformFundraising = 2003,

    /// <summary>
    /// حقوق صاحبان سهم
    /// </summary>
    [Description("حقوق صاحبان سهم")]
    Equity = 3000,

    /// <summary>
    /// درآمدها  
    /// </summary>
    [Description("درآمدها")]
    Incomes = 4001,
}
