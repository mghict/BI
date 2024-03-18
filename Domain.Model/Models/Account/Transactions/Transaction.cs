using Moneyon.Common.Collections;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.Extensions;
using Moneyon.Common.File.SpreadSheet;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Common.Extensions;
using Moneyon.PowerBi.Domain.Model.Modeling;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Moneyon.PowerBi.Domain.Modeling;

/// <summary>
/// تراکنش مالی
/// </summary>
public class Transaction : AppEntity
{
    public long SecurityCode { get; set; }

    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }

    [Range(1, long.MaxValue)]
    public long Amount { get; set; }

    [MaxLength(200)]
    public string Description { get; set; }

    public ICollection<TransactionDetail> Details { get; set; }

    public string TrackingCode { get; private set; }

    public Transaction() {
        TrackingCode =Guid.NewGuid().ToString("N");
    }

    public Transaction(TransactionType type, long amount, string description)
        :this()
    {
        SecurityCode = DateTime.UtcNow.GenerateDateTimeSecurityCode();
        Type = type;
        Date = DateTime.Now;
        Amount = amount;
        Description = description;
        Details = new List<TransactionDetail>();
    }

    public Transaction(long securityCode, TransactionType type, long amount, string description)
        : this()
    {
        SecurityCode = securityCode;
        Type = type;
        Date = DateTime.Now;
        Amount = amount;
        Description = description;
        Details = new List<TransactionDetail>();
    }

    public Transaction(TransactionType type, Account debitAccount, Account creditAccount, long amount, string description)
        : this(type, amount, description)
    {
        TransactionSet(debitAccount, creditAccount, amount, description);
    }

    public Transaction(long securityCode, TransactionType type, Account debitAccount, Account creditAccount, long amount, string description)
        : this(securityCode, type, amount, description)
    {
        TransactionSet(debitAccount, creditAccount, amount, description);
    }

    public Transaction(
        TransactionType type,
        IEnumerable<AccountAmountTuple> debitAccounts,
        IEnumerable<AccountAmountTuple> creditAccounts,
        long amount,
        string description) : this(type, amount, description)
    {
        TransactionSet(debitAccounts, creditAccounts, amount, description);
    }


    public Transaction(
        long securityCode,
        TransactionType type,
        IEnumerable<AccountAmountTuple> debitAccounts,
        IEnumerable<AccountAmountTuple> creditAccounts,
        long amount,
        string description) : this(securityCode, type, amount, description)
    {
        TransactionSet(debitAccounts, creditAccounts, amount, description);
    }

    private void TransactionSet(
        Account debitAccount,
        Account creditAccount,
        long amount,
        string description)
    {
        debitAccount.Balance = debitAccount.Balance + (debitAccount.HasDebitNature ? 1 : -1) * amount;
        creditAccount.Balance = creditAccount.Balance + (creditAccount.HasCreditNature ? 1 : -1) * amount;
        Details = new List<TransactionDetail>()
            {
                new TransactionDetail()
                {
                    Account = debitAccount,
                    Credit = 0,
                    Debit = amount,
                    AccountBalance = debitAccount.Balance,
                    Description= description
                },
                new TransactionDetail()
                {
                    Account = creditAccount,
                    Credit = amount,
                    Debit = 0,
                    AccountBalance = creditAccount.Balance,
                    Description= description
                }
            };
    }

    private void TransactionSet(
        IEnumerable<AccountAmountTuple> debitAccounts,
        IEnumerable<AccountAmountTuple> creditAccounts,
        long amount,
        string description)
    {
        if (debitAccounts.Sum(x => x.Amount) != creditAccounts.Sum(x => x.Amount))
        {
            throw new BizException(BizExceptionCode.InvalidTransaction);
        }

        debitAccounts.ForEach(tuple =>
        {
            tuple.Account.Balance = tuple.Account.Balance + (tuple.Account.HasDebitNature ? 1 : -1) * tuple.Amount;
            Details.Add(new TransactionDetail()
            {
                Account = tuple.Account,
                Credit = 0,
                Debit = tuple.Amount,
                AccountBalance = tuple.Account.Balance,
                Description = description
            });
        });

        creditAccounts.ForEach(tuple =>
        {
            tuple.Account.Balance = tuple.Account.Balance + (tuple.Account.HasCreditNature ? 1 : -1) * tuple.Amount;
            Details.Add(new TransactionDetail()
            {
                Account = tuple.Account,
                Credit = tuple.Amount,
                Debit = 0,
                AccountBalance = tuple.Account.Balance,
                Description = description
            });
        });
    }

}

public class TransactionDetailShortDto
{
    [WorkSheetCoulmnAttribute(Header = "بدهکار")]
    public long Debit { get; set; }

    [WorkSheetCoulmnAttribute(Header = "بستانکار")]
    public long Credit { get; set; }

    [WorkSheetCoulmnAttribute(Header = "موجودی")]
    public long AccountBalance { get; set; }


    public TransactionShortDto Transaction { get; set; }
    public AccountDetailsDto Account { get; set; }


    #region  Extra Excle

    [JsonIgnore]
    [WorkSheetCoulmnAttribute(Header = "شرح تراکنش")]
    public string? Description => Transaction?.Description;

    [JsonIgnore]
    [WorkSheetCoulmnAttribute(Header = "تاریخ تراکنش")]
    public string? Date => Transaction?.Date.ToJalaliDateTime();

    [JsonIgnore]
    [WorkSheetCoulmnAttribute(Header = "کد پیگیری")]
    public string? TrackingCode => Transaction?.TrackingCode ?? "";

    [JsonIgnore]
    [WorkSheetCoulmnAttribute(Header = "مبلغ تراکنش")]
    public long? Amount => Transaction?.Amount;


    [WorkSheetCoulmnAttribute(Header = "مشخصات")]
    [JsonIgnore]
    public string? DisplayName => Account.Owner.DisplayName;

    [WorkSheetCoulmnAttribute(Header = "نام")]
    [JsonIgnore]
    public string? AccountName => Account is null ? "" : Account.Name;

    [WorkSheetCoulmnAttribute(Header = "مانده حساب")]
    [JsonIgnore]
    public long? Balance => Account is null ? null : Account.Balance;

    [WorkSheetCoulmnAttribute(Header = "مقدار بلوکه شده")]
    [JsonIgnore]
    public long? Blocked => Account is null ? null : Account.Blocked;

    [WorkSheetCoulmnAttribute(Header = "نام گروه حساب")]
    [JsonIgnore]
    public string? AccountGroupName => Account is null ? null : Account?.Group?.Name;

    #endregion
}

public class TransactionShortDto
{
    public int Id { get; set; }
    [WorkSheetCoulmnAttribute(Header = "شرح تراکنش")]
    public string Description { get; set; }

    [WorkSheetCoulmnAttribute(Header = "تاریخ تراکنش")]
    [JsonIgnore]
    public string DatePesian => Date.ToJalaliDateTime();
    public DateTime Date { get; set; }

    [WorkSheetCoulmnAttribute(Header = "کد پیگیری")]
    public string TrackingCode { get; set; }

    [WorkSheetCoulmnAttribute(Header = "مبلغ")]
    public long Amount { get; set; }

}

public class TransactionDetailSummaryDto
{
    [WorkSheetCoulmnAttribute(Header = "بدهکار")]
    public long Debit { get; set; }

    [WorkSheetCoulmnAttribute(Header = "بستانکار")]
    public long Credit { get; set; }

    [WorkSheetCoulmnAttribute(Header = "موجودی")]
    public long AccountBalance { get; set; }


    //public TransactionShortDto Transaction { get; set; }
    //public AccountDetailsDto Account { get; set; }


    #region  Extra Excle

    [WorkSheetCoulmnAttribute(Header = "شرح تراکنش")]
    public string? TransactionDescription { get; set; }

    [WorkSheetCoulmnAttribute(Header = "تاریخ تراکنش")]
    public string? TransactionPersianDate { get; set; } //=> Transaction?.Date.ToJalaliDateTime();

    public DateTime TransactionDate { get; set; }

    [WorkSheetCoulmnAttribute(Header = "کد پیگیری")]
    public string? TrackingCode { get; set; }

    [WorkSheetCoulmnAttribute(Header = "مبلغ تراکنش")]
    public long? TransactionAmount { get; set; }


    [WorkSheetCoulmnAttribute(Header = "مشخصات")]
    public string? OwnerDisplayName { get; set; }



    #endregion
}
