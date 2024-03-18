using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.PowerBi.API.Controllers.Base;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Moneyon.PowerBi.API.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : AppBaseController
{
    private readonly AccountService _accountService;
    public AccountController(IHttpContextAccessor contextAccessor, AccountService accountService) : base(contextAccessor)
    {
        _accountService = accountService;
    }


    [HttpGet]
    [Route("sum-accounts")]
    [JWTAuthorization()]
    public async Task<AccountBalanceDto> GetSumAccounts()
    {
        if (User is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        return await _accountService.GetSumAccountsAsync(User.PersonId);
    }

    [HttpGet]
    [Route("accounts")]
    [JWTAuthorization()]
    public async Task<IEnumerable<AccountShortDto>> GetAccounts()
    {
        if (User is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        return await _accountService.GetAccountsAsync(User.PersonId);
    }

    [HttpGet]
    [Route("{accountId}/transactions")]
    [JWTAuthorization]
    public async Task<DataResult<TransactionDetailSummaryDto>> GetAccountTransaction([FromQuery] DataRequest request,long accountId, CancellationToken cancellationToken=default)
    {
        return await _accountService.GetWalletTransactionsAsync(accountId, request,cancellationToken);
    }


}


