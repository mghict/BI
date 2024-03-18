namespace Moneyon.PowerBi.Domain.Modeling;

public class AccountAmountTuple
{
    public Account Account { get; }
    public long Amount { get; }

    public AccountAmountTuple(Account account, long amount)
    {
        Account = account;
        Amount = amount;
    }
}