using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.File.SpreadSheet;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Moneyon.PowerBi.Domain.Modeling;

/// <summary>
/// حساب
/// </summary>
public class Account : AppEntity
{

    public long AccountGroupId { get; set; }
    public long OwnerId { get; set; }
    public AccountKey? Key { get; set; }

    [MaxLength(500)]
    public string? Name { get; set; }

    /// <summary>
    /// مانده حساب
    /// </summary>
    public long Balance { get; set; }

    /// <summary>
    /// موجودی مسدود شده
    /// </summary>
    public long Blocked { get; set; }

    /// <summary>
    /// دلیل مسدود شدن موجودی 
    /// </summary>
    public string? BlockReason { get; set; }

    /// <summary>
    /// ریز تراکنش ها 
    /// </summary>
    public ICollection<TransactionDetail> TransactionDetails { get; set; }
    
    public AccountGroup Group { get; set; }

    public Person Owner { get; set; }

    /// <summary>
    /// مانده در دسترس
    /// </summary>
    public long AvailableBalance { get => Balance - Blocked; }

    /// <summary>
    /// آیا ماهیت حساب «بدهکار» است؟
    /// </summary>
    public bool HasDebitNature { get => Group.HasDebitNature; }

    /// <summary>
    /// آیا ماهیت حساب «بستانکار» است؟
    /// </summary>
    public bool HasCreditNature { get => Group.HasCreditNature; }

    public Account()
    {
    }

    public Account(long accountGroupId, AccountKey key, string name)
    {
        AccountGroupId = accountGroupId;
        Key = key;
        Name = name;
    }

    public Account(AccountGroup accountGroup, AccountKey key, string name)
    {
        Group = accountGroup;
        Key = key;
        Name = name;
    }

    public void EnsureAvailableBalance(long amount)
    {
        if (AvailableBalance < amount)
        {
            throw new BizException(BizExceptionCode.InsufficientAccountBalance);
        }
    }

    public void EnsureBalance(long amount)
    {
        if (Balance < amount)
        {
            throw new BizException(BizExceptionCode.InsufficientAccountBalance);
        }
    }
}


public class AccountBalanceDto
{
    [WorkSheetCoulmnAttribute(Header = "موجودی حساب")]
    public long Balance { get; set; }
    [WorkSheetCoulmnAttribute(Header = "مانده بلوکه شده")]
    public long Blocked { get; set; }
    [WorkSheetCoulmnAttribute(Header = "مانده در دسترس")]
    public long AvailableBalance { get; set; }
}

public class AccountShortDto
{
    public int Id { get; set; }

    [WorkSheetCoulmnAttribute(Header = "نام")]
    public string Name { get; set; }

    [WorkSheetCoulmnAttribute(Header = "مانده حساب")]
    public long Balance { get; set; }

    [WorkSheetCoulmnAttribute(Header = "مقدار بلوکه شده")]
    public long Blocked { get; set; }
    public long AvailableBalance { get; set; }
}

public class AccountDetailsDto : AccountShortDto
{

    public PersonShortDto Owner { get; set; }
    public AccountGroupDto Group { get; set; }

    #region Extra Excel

    [WorkSheetCoulmnAttribute(Header = "مشخصات")]
    [JsonIgnore]
    public string DisplayName => Owner.DisplayName;

    [WorkSheetCoulmnAttribute(Header = "نام گروه")]
    [JsonIgnore]
    public string GroupName => Group.Name;
    #endregion
}
