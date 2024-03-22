using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories.Interfaces;

public interface ICountryRepository
{
    Task<string?> Create(Country country);
    Task<IEnumerable<Country>> GetAll();
    Task<Country?> GetById(string id);
    Task<string?> Update(Country country);
    Task<string?> Delete(string id);
    Task<List<Country>> GetPaginated(int page, int pageSize); 
    int GetTotalCount();
}
