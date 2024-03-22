using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories.Interfaces;

public interface ILocationRepository
{
    Task<int?> Create(Location location);
    Task<IEnumerable<Location>> GetAll();
    Task<Location> GetById(int id);
    Task<int?> Update(Location location);
    Task<int?> Delete(int id);
    Task<List<Location>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
