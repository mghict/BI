using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using Domain.Model.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Modeling;
using System.Collections.Generic;


namespace Moneyon.PowerBi.Domain.Service.Services;

[AutoRegister]
public class AccountService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public AccountService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this._uw = unitOfWork;
        this._mp = mapper;
    }

    public async Task<IEnumerable<AccountShortDto>> GetAccountsAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        var accounts=await _uw.AccountRepository.ReadAsync(filter:p=>p.Owner.Code==personId, cancellationToken);
        return _mp.MapCollection<Account, AccountShortDto>(accounts);
    }

    public async Task<IEnumerable<AccountShortDto>> GetAccountsAsync(long personId, CancellationToken cancellationToken = default)
    {
        var accounts = await _uw.AccountRepository.ReadAsync(filter: p => p.OwnerId == personId,
                                                                          include: i=>i.Include(p=>p.Group) , 
                                                                          cancellationToken);
        return _mp.MapCollection<Account, AccountShortDto>(accounts);
    }

    public async Task<AccountBalanceDto> GetSumAccountsAsync(long personId, CancellationToken cancellationToken = default)
    {
        var accounts = await _uw.AccountRepository.ReadAsync(filter: p => p.OwnerId == personId, cancellationToken);

        return new AccountBalanceDto()
        {
            AvailableBalance = accounts!.Sum(a => a.AvailableBalance),
            Balance = accounts!.Sum(a => a.Balance),
            Blocked = accounts!.Sum(a => a.Blocked)
        };
    }

    public async Task<AccountBalanceDto> GetWalletBalanceAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        var account = await _uw.AccountRepository.GetAccountAsync(personId, AccountKey.Wallet, cancellationToken);
        return _mp.Map<AccountBalanceDto>(account);
    }

    public async Task<AccountBalanceDto> GetWalletBalanceAsync(long personId, CancellationToken cancellationToken = default)
    {
        var person = await _uw.PersonRepository.FirstOrDefaultAsync(p=>p.Id==personId);
        if (person is null) throw new BizException(BizExceptionCode.UserNotFound);

        var account = await _uw.AccountRepository.GetAccountAsync(person.Code, AccountKey.Wallet, cancellationToken);
        return _mp.Map<AccountBalanceDto>(account);
    }

    public async Task<IEnumerable<TransactionDetailShortDto>> GetWalletTransactionsAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        var txDetails = await _uw.AccountRepository.GetAccountTransactionDetailsAsync(personId, AccountKey.Wallet, cancellationToken);
        return _mp.MapCollection<TransactionDetail, TransactionDetailShortDto>(txDetails);
    }

    public async Task<IEnumerable<TransactionDetailShortDto>> GetWalletTransactionsAsync(long personId, CancellationToken cancellationToken = default)
    {
        var person = await _uw.PersonRepository.FirstOrDefaultAsync(p => p.Id == personId);
        if (person is null) throw new BizException(BizExceptionCode.UserNotFound);

        var txDetails = await _uw.AccountRepository.GetAccountTransactionDetailsAsync(person.Code, AccountKey.Wallet, cancellationToken);
        return _mp.MapCollection<TransactionDetail, TransactionDetailShortDto>(txDetails);
    }

    public async Task<DataResult<TransactionDetailSummaryDto>> GetWalletTransactionsAsync(long accountId,DataRequest request, CancellationToken cancellationToken = default)
    {
        var txDetails = await _uw.TransactionDetailRepository.ReadPagableAsync(request,
                                                                               filter: p=>p.AccountId== accountId,
                                                                               include: i=>i.Include(p=>p.Transaction),
                                                                               orderBy:o=>o.OrderByDescending(p=>p.Transaction.Date));

        return _mp.MapDataResult<TransactionDetail, TransactionDetailSummaryDto>(txDetails);
    }

    
}