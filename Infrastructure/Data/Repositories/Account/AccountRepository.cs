using DocumentFormat.OpenXml.InkML;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.PowerBi.Domain.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Infrastructure.Data.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    protected PowerBiContext context;
    public AccountRepository(PowerBiContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<Account> GetAccountAsync(Guid personId, AccountKey key, CancellationToken cancellationToken = default)
    {
        return await dbSet
            .Include(x => x.Group)
            .SingleAsync(x => x.Owner.Code == personId && x.Key == key, cancellationToken);
    }

    public async Task<Account> GetAccountAsync(long personId, AccountKey key, CancellationToken cancellationToken = default)
    {
        return await dbSet
            .Include(x => x.Group)
            .FirstOrDefaultAsync(x => x.OwnerId == personId && x.Key == key, cancellationToken);
    }

    public async Task<Account> GetAccountAsync(long accountId, CancellationToken cancellationToken = default)
    {
        return await dbSet
            .Include(x => x.Group)
            .SingleAsync(x => x.Id == accountId, cancellationToken);
    }

    public async Task<AccountGroup> GetAccountGroupAsync(AccountGroupKey key, CancellationToken cancellationToken = default)
    {
        //TODO: needs caching
        return await context.AccountGroups.SingleAsync(x => x.Key == key, cancellationToken);
    }

    public async Task<IEnumerable<TransactionDetail>> GetAccountTransactionDetailsAsync(Guid personId, AccountKey key, CancellationToken cancellationToken = default)
    {
        return await context.TransactionDetails
            .Include(x => x.Transaction)
            .Where(x => x.Account.Owner.Code == personId && x.Account.Key == key)
            .OrderByDescending(x => x.Transaction.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TransactionDetail>> GetAccountTransactionDetailsAsync(long accountId, CancellationToken cancellationToken = default)
    {
        return await context.TransactionDetails
        .Include(x => x.Transaction)
        .Where(x => x.AccountId == accountId)
        .OrderByDescending(x => x.Transaction.Date)
        .ToListAsync(cancellationToken);
    }
}


