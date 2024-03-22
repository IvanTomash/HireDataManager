using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories.Interfaces;

public interface IRegionRepository
{
    Task<int?> Create(Region region);
    Task<IEnumerable<Region>> GetAll();
    Task<Region?> GetById(int id);
    Task<int?> Update(Region region);
    Task<int?> Delete(int id);
    Task<List<Region>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
