using Moneyon.PowerBi.Common.ObjectMapper;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

[ObjectMapper]
public class TicketDetailMapper: AutoMapper.Profile
{
    public TicketDetailMapper()
    {
        CreateMap<TicketDetail, TicketDetailDto>()
            .ForMember(des => des.Person, src => src.MapFrom(x => x.Person!.DisplayName ?? ""));

        CreateMap<IEnumerable<TicketDetail>, IEnumerable<TicketDetailDto>>();
        //profile.CreateMap<ResponseDto<IEnumerable<TicketDetail>>, ResponseDto<IEnumerable<TicketDetailDto>>>();

        CreateMap<TicketDetail, TicketDetailShortDto>()
            .ForMember(des => des.Person, src => src.MapFrom(x => x.Person.DisplayName));

        CreateMap<IEnumerable<TicketDetail>, IEnumerable<TicketDetailShortDto>>();

        CreateMap<TicketDetailCreateDto, TicketDetail>()
            .ForMember(des => des.CreateOn, src => src.MapFrom(x => System.DateTime.UtcNow));


    }
}