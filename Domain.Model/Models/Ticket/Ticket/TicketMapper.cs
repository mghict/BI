using Moneyon.Common.Extensions;
using Moneyon.PowerBi.Common.ObjectMapper;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

[ObjectMapper]
public class TicketMapper
{
    public TicketMapper(AutoMapper.Profile profile)
    {
        profile.CreateMap<Ticket, TicketDto>()
            .ForMember(des => des.Status, src => src.MapFrom(x => x.Status.GetDescription()))
            .ForMember(des => des.StatusCode, src => src.MapFrom(x => (int)x.Status))
            .ForMember(des => des.TicketCategory, src => src.MapFrom(x => x.TicketCategory.Name))
            .ForMember(des => des.LastUserModified, src => src.MapFrom(x => x.LastUserModified!.DisplayName))
            .ForMember(des => des.CreatedBy, src => src.MapFrom(x => x.CreatedBy.DisplayName))
            .ForMember(des => des.TicketDetails, opt => opt.Ignore());

        profile.CreateMap<IEnumerable<Ticket>, IEnumerable<TicketDto>>();

        profile.CreateMap<Ticket, TicketShortDto>()
            .ForMember(des => des.Status, src => src.MapFrom(x => x.Status.GetDescription()))
            .ForMember(des => des.StatusCode, src => src.MapFrom(x => (int)x.Status))
            .ForMember(des => des.TicketCategory, src => src.MapFrom(x => x.TicketCategory.Name))
            .ForMember(des => des.LastUserModified, src => src.MapFrom(x => x.LastUserModified!.DisplayName))
            .ForMember(des => des.CreatedBy, src => src.MapFrom(x => x.CreatedBy.DisplayName));

        profile.CreateMap<IEnumerable<Ticket>, IEnumerable<TicketShortDto>>();




        profile.CreateMap<CreateTicketDto, Ticket>()
            .ForMember(des => des.Status, src => src.MapFrom(x => TicketStatus.AwaitingForReview))
            .ForMember(des => des.CreateOn, src => src.MapFrom(x => System.DateTime.UtcNow));

        //profile.CreateMap<UpdateTicketDto, Ticket>();

        //profile.CreateMap<TicketChangeDto, TicketChangeStatusDto>();
    }
}
