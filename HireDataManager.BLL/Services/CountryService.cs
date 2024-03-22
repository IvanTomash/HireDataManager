using AutoMapper;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.DAL.Repositories;
using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models.Dto;
using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services;

public class CountryService : ICountryService
{
    private readonly IMapper _mapper;
    private readonly ICountryRepository _countryRepository;

    public CountryService(IMapper mapper, ICountryRepository countryRepository)
    {
        _mapper = mapper;
        _countryRepository = countryRepository;
    }

    public async Task<string?> Create(CountryDto country)
    {
        Country newCountry = _mapper.Map<Country>(country);
       
        string? result = await _countryRepository.Create(newCountry);
        return result;
    }

    public async Task<string?> Delete(string id)
    {
        var result = await _countryRepository.Delete(id);
        return result;
    }

    public async Task<IEnumerable<CountryDto>> GetAll()
    {
        var countries = await _countryRepository.GetAll();
        if (countries != null)
        {
            var countriesDto = countries.Select(c => _mapper.Map<CountryDto>(c));
            return countriesDto;
        }

        return null!;
    }

    public async Task<CountryDto> GetById(string id)
    {
        var country = await _countryRepository.GetById(id);
        if (country != null)
        {
            var countryDto = _mapper.Map<CountryDto>(country);
            return countryDto;
        }
        return null!;
    }

    public async Task<List<CountryDto>> GetPaginated(int page, int pageSize)
    {
       var result = _mapper.Map<List<CountryDto>>(await _countryRepository.GetPaginated(page, pageSize));
        return result;
    }

    public int GetTotalCount()
    {
        return _countryRepository.GetTotalCount();
    }

    public async Task<string?> Update(CountryDto country)
    {
        var updatedEntity = _mapper.Map<Country>(country);
        var result = await _countryRepository.Update(updatedEntity);
        return result;
    }
}
