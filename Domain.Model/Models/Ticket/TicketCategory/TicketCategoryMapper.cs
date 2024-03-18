using Moneyon.PowerBi.Common.ObjectMapper;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

[ObjectMapper]
public class TicketCategoryMapper
{
    public TicketCategoryMapper(AutoMapper.Profile profile)
    {

        profile.CreateMap<TicketCategory, TicketCategoryDto>()
            .ForMember(des => des.ParentName, src => src.MapFrom(x => x.Parent!.Name ?? string.Empty));

        profile.CreateMap<CreateTicketCategoryDto, TicketCategory>();

        profile.CreateMap<UpdateTicketCategoryDto, TicketCategory>();

    }
}
