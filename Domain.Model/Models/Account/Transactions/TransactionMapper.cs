using AutoMapper;
using Moneyon.PowerBi.Common.ObjectMapper;


namespace Moneyon.PowerBi.Domain.Modeling;

[ObjectMapper]
public class TransactionMapper : Profile
{
    public TransactionMapper()
    {
        CreateMap<Transaction, TransactionShortDto>();

        
    }
}