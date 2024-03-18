namespace Moneyon.PowerBi.Domain.Model.Modeling;

public class PackageDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Descreption { get; set; }
    public int TimePeriod { get; set; }
    public long Price { get; set; }
    public int Discount { get; set; }
    public long PackageAmount { get; set; }
    public bool ShowInHomePage { get; set; } 
    public bool IsSuperiorDiscount { get; set; } 
    public AllowdUserCount AllowdUserCount { get; set; }
    public bool Active { get; set; }
    public ICollection<PermissionDto>? Permissions { get; set; }
}

public class PackageCreateReqDto
{
    public string Name { get; set; }
    public string? Descreption { get; set; }
    public int TimePeriod { get; set; }
    public long Price { get; set; }
    public int Discount { get; set; }
    public bool ShowInHomePage { get; set; }
    public bool IsSuperiorDiscount { get; set; }
    public AllowdUserCount AllowdUserCount { get; set; }
    public bool Active { get; set; }
    public long[]? PermissionId { get; set; }


}

public class PackageEditReqDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Descreption { get; set; }
    public int TimePeriod { get; set; }
    public long Price { get; set; }
    public int Discount { get; set; }
    public bool ShowInHomePage { get; set; }
    public bool IsSuperiorDiscount { get; set; }
    public AllowdUserCount AllowdUserCount { get; set; }
    public bool Active { get; set; }
    public long[]? PermissionId { get; set; }


}

public class ShowPackagesDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Descreption { get; set; }
    public int TimePeriod { get; set; }
    public long Price { get; set; }
    public int Discount { get; set; }
    public long PackageAmount { get; set; }
    public bool ShowInHomePage { get; set; }
    public bool IsSuperiorDiscount { get; set; }
    public AllowdUserCount AllowdUserCount { get; set; }
    public ICollection<ReportShowDto>? Reports { get; set; }
}