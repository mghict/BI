using AutoMapper;
using Domain.Model.Common.Data;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Service;

[AutoRegister()]
public class InfoService
{
    private readonly IMapper _mp;
    private readonly IUnitOfWork _uw;

    public InfoService(IMapper mp, IUnitOfWork unitOfWork)
    {
        _mp = mp;
        _uw = unitOfWork;
    }

    public async Task<IEnumerable<CountryDto>> GetListCountry()
    {
        var countries= await _uw.CountryRepository.ReadAsync();

        return _mp.MapCollection<Country,CountryDto>(countries);
    }

    public async Task<IEnumerable<ProvinceDto>> GetListProvince()
    {
        var provinces = await _uw.ProvinceRepository.ReadAsync();

        return _mp.MapCollection<Province, ProvinceDto>(provinces);
    }

    public async Task<IEnumerable<ProvinceDto>> GetListProvince(long countryId)
    {
        var provinces = await _uw.ProvinceRepository.ReadAsync(filter: p=>p.CountryId==countryId);

        return _mp.MapCollection<Province, ProvinceDto>(provinces);
    }

    public async Task<IEnumerable<CityDto>> GetListCity()
    {
        var cities = await _uw.CityRepository.ReadAsync();

        return _mp.MapCollection<City, CityDto>(cities);
    }

    public async Task<IEnumerable<CityDto>> GetListCity(long provinceId)
    {
        var cities = await _uw.CityRepository.ReadAsync(filter: p=>p.ProvinceId==provinceId);

        return _mp.MapCollection<City, CityDto>(cities);
    }
}
