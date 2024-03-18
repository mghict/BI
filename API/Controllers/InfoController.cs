using Microsoft.AspNetCore.Mvc;
using Moneyon.PowerBi.Domain.Modeling;
using Moneyon.PowerBi.Domain.Service;

namespace Moneyon.PowerBi.API.Controllers;

[Route("api/info")]
[ApiController]
public class InfoController : ControllerBase
{
    private readonly InfoService _infoService;

    public InfoController(InfoService infoService)
    {
        _infoService = infoService;
    }

    [HttpGet]
    [Route("country")]
    public async Task<IEnumerable<CountryDto>> GetCountryList()
    {
        return await _infoService.GetListCountry();
    }

    [HttpGet]
    [Route("province")]
    public async Task<IEnumerable<ProvinceDto>> GetProvinceList()
    {
        return await _infoService.GetListProvince();
    }

    [HttpGet]
    [Route("{countryId}/province")]
    public async Task<IEnumerable<ProvinceDto>> GetProvinceList(long countryId)
    {
        return await _infoService.GetListProvince(countryId);
    }

    [HttpGet]
    [Route("city")]
    public async Task<IEnumerable<CityDto>> GetCityList()
    {
        return await _infoService.GetListCity();
    }

    [HttpGet]
    [Route("{provinceId}/city")]
    public async Task<IEnumerable<CityDto>> GetCityList(long provinceId)
    {
        return await _infoService.GetListCity(provinceId);
    }
}