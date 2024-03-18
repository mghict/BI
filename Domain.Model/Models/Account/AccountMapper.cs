using AutoMapper;
using Moneyon.PowerBi.Common.ObjectMapper;



namespace Moneyon.PowerBi.Domain.Modeling;


[ObjectMapper]
public class AccountMapper:Profile
{
    public AccountMapper()
    {
        
        CreateMap<Account, AccountShortDto>();
        CreateMap<Account, AccountBalanceDto>();
        CreateMap<Account, AccountDetailsDto>();
    }
}