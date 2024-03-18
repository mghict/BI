using Moneyon.Common.Extensions;
using Moneyon.PowerBi.Common.ObjectMapper;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

[ObjectMapper]
public class DocumentMapper: AutoMapper.Profile
{
    public DocumentMapper()
    {
        //profile.CreateMap<IFormFile, UserDocument>()
        //     .ForMember(model => model.OriginalFileName, opt => opt.MapFrom(x => x.FileName))
        //     .ForMember(model => model.ContentType, opt => opt.MapFrom(x => x.ContentType))
        //     .ForMember(model => model.Size, opt => opt.MapFrom(x => x.Length));

        CreateMap<DocumentCreateDto, Document>();
        CreateMap<Document, DocumentDto>()
            .ForMember(model => model.Description, opt => opt.MapFrom(x => x.Description ?? x.Type.GetDescription()));

        CreateMap<Document, DocumentWithContentDto>()
            .ForMember(model => model.Content, opt => opt.MapFrom(x => x.Content.Value));

        CreateMap<IEnumerable<Document>, IEnumerable<DocumentWithContentDto>>();

    }
}