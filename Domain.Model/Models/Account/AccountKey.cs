using System.ComponentModel;

namespace Moneyon.PowerBi.Domain.Modeling;

/// <summary>
/// کلید حساب
/// </summary>
public enum AccountKey
{
    /// <summary>
    /// کیف پول 
    /// </summary>      
    [Description("کیف پول")]
    Wallet = 1,

    #region دارایی ها - شروع از 1000
    /// <summary>
    /// موجودی بانک
    /// </summary>
    [Description("موجودی بانک")]
    Bank = 1001,
    #endregion

    #region بدهی ها - شروع از 2000
    /// <summary>
    /// جمع‌آوری سرمایه پروژه‌ها
    /// </summary>
    [Description("جمع‌آوری سرمایه پروژه‌ها")]
    PlatformFundraising = 2001,
    #endregion

    #region سرمایه - شروع از 3000
    [Description("سرمایه")]
    Capital = 3000,
    #endregion

    #region درآمد 
    [Description("درآمد")]
    PlatformIncomes = 4000,
    #endregion
}
