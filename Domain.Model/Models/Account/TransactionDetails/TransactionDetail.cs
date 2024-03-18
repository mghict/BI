using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Domain.Modeling;

/// <summary>
/// یک قلم از جزئیات تراکنش مالی
/// </summary>
public class TransactionDetail : AppEntity
{
    //public int Id { get; set; }
    public long TransactionId { get; set; }
    public long AccountId { get; set; }
    public long Credit { get; set; }
    public long Debit { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// Account balance after transaction
    /// </summary>
    public long AccountBalance { get; set; }

    public Account Account { get; set; }
    public Transaction Transaction { get; set; }

    public TransactionDetail()
    {
    }

    public TransactionDetail(int accountId, long credit, long debit, long accountBalance)
    {
        AccountId = accountId;
        Credit = credit;
        Debit = debit;
        AccountBalance = accountBalance;
    }
}
