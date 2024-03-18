using AutoMapper;
using Moneyon.PowerBi.Common.ObjectMapper;


namespace Moneyon.PowerBi.Domain.Modeling;

[ObjectMapper]
public class AccountGroupMapper : Profile
{
    public AccountGroupMapper()
    {
        CreateMap<AccountGroup, AccountGroupDto>();
    }
}