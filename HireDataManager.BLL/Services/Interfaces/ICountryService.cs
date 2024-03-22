using HireDataManager.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services.Interfaces;

public interface ICountryService
{
    Task<string?> Create(CountryDto country);
    Task<IEnumerable<CountryDto>> GetAll();
    Task<CountryDto> GetById(string id);
    Task<string?> Update(CountryDto country);
    Task<string?> Delete(string job);
    Task<List<CountryDto>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
