using AutoMapper;
using Moneyon.Common.Extensions;
using Moneyon.PowerBi.Common.ObjectMapper;

namespace Moneyon.PowerBi.Domain.Modeling;

[ObjectMapper]
public class TransactionDetailMapper : Profile
{
    public TransactionDetailMapper()
    {
        CreateMap<TransactionDetail, TransactionDetailSummaryDto>()
            .ForMember(des=>des.TransactionAmount,src=>src.MapFrom(src=>src.Transaction.Amount))
            .ForMember(des => des.TransactionDate, src => src.MapFrom(src => src.Transaction.Date))
            .ForMember(des => des.TransactionPersianDate, src => src.MapFrom(src => src.Transaction.Date.ToJalaliDateTime()))
            .ForMember(des => des.TransactionDescription, src => src.MapFrom(src => src.Description))
            .ForMember(des => des.TrackingCode, src => src.MapFrom(src => src.Transaction.TrackingCode))
            .ForMember(des => des.OwnerDisplayName, src => src.MapFrom(src => src.Account.Owner.DisplayName))
            ;

        CreateMap<TransactionDetail, TransactionDetailShortDto>();
    }
}