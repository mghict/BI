namespace Moneyon.PowerBi.Domain.Modeling;

public class AddressDto
{
    public long? CityId { get; set; }
    public long? ProvinceId { get; set; }
    public long? CountryId { get; set; }
    public string? CityName { get; set; }
    public string? ProvinceName { get; set; }
    public string? CountryName { get; set; }
    public string? AddressValue { get; set; }
    public string? Tel { get; set; }
    public string? PostalCode { get; set; }
}

public class AddressCreateDto
{
    public long CityId { get; set; }
    public string AddressValue { get; set; }
    public string? Tel { get; set; }
    public string? PostalCode { get; set; }
}
