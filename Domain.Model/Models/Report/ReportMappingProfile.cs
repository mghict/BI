using Moneyon.PowerBi.Common.Extensions;
using System.Security.Cryptography;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class ReportMappingProfile : AutoMapper.Profile
{

    public ReportMappingProfile()
    {


        CreateMap<ReportCreateDto, Report>()
            .ForMember(des => des.Permission, src => src.MapFrom(src => new Permission()
            {
                Name = src.LatinName,
                Description = src.Title,
                Type = PermissionType.Report,
                PermisionId = GenerateRandomPermissionId(),
            }));

        CreateMap<ReportEditDto, Report>();

        CreateMap<Report, ReportDto>();
        CreateMap<Report, ReportShowDto>();
    }

    private int GenerateRandomPermissionId()
    {
        var id = Guid.NewGuid().ToByteArray();
        var minValue = BitConverter.ToInt32(id, 0);
        return Math.Abs( RandomNumberGenerator.GetInt32(minValue, int.MaxValue));
    }
}
