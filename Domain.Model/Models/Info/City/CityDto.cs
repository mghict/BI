namespace Moneyon.PowerBi.Domain.Modeling;

public class CityDto
{
    public long Id { get; set; }
    public string CityName { get; set; }
    public long ProvinceId { get; set; }
    public ShortProvinceDto Province { get; set; }
}
