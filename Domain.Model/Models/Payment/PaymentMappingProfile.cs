using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.Domain.Modeling;

[ObjectMapper]
public class PaymentMappingProfile : AutoMapper.Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, PaymentDto>();
        CreateMap<ReceiptPayment, PaymentDto>();
        CreateMap<IpgPayment, PaymentDto>();
        CreateMap<ReceiptPayment, PaymentWithImageDto>()
            .ForMember(des=> des.Document,src=>src.MapFrom(src=>src.Image));
    }
}