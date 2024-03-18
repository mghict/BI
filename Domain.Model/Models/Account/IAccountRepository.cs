using Moneyon.PowerBi.Domain.Model.Modeling;


namespace Moneyon.PowerBi.Domain.Modeling;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<Account> GetAccountAsync(long accountId, CancellationToken cancellationToken = default);
    Task<Account> GetAccountAsync(Guid personId, AccountKey key, CancellationToken cancellationToken = default);
    Task<Account> GetAccountAsync(long personId, AccountKey key, CancellationToken cancellationToken = default);
    Task<AccountGroup> GetAccountGroupAsync(AccountGroupKey key, CancellationToken cancellationToken = default);
    Task<IEnumerable<TransactionDetail>> GetAccountTransactionDetailsAsync(long accountId, CancellationToken cancellationToken = default);

    Task<IEnumerable<TransactionDetail>> GetAccountTransactionDetailsAsync(Guid personId, AccountKey key, CancellationToken cancellationToken = default);
}
