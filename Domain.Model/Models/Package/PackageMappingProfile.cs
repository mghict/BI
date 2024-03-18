namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class PackageMappingProfile : AutoMapper.Profile
{
    public PackageMappingProfile()
    {
        CreateMap<PackageCreateReqDto, Package>().ReverseMap();
        
        CreateMap<PackageEditReqDto, Package>().ReverseMap();

        CreateMap<PackageDto, Package>();
        CreateMap< Package, PackageDto>()
            .ForMember(des=>des.Permissions,src=>src.MapFrom(src=>src.Reports!));

        CreateMap<Package, ShowPackagesDto>()
            .ForMember(des => des.Reports, src => src.MapFrom(src=>src.Reports!.Select(p=>p.Report!).ToList()));
    }
}
