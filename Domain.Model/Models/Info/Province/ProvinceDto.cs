namespace Moneyon.PowerBi.Domain.Modeling;

public class ProvinceDto
{
    public int Id { get; set; }
    public string ProvinceName { get; set; }
    public long CountryId { get; set; }
    public Country Country { get; set; }
    public IEnumerable<CityDto> Cities { get; set; }


}

