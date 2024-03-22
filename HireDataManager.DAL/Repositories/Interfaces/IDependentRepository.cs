using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories.Interfaces;

public interface IDependentRepository
{
    Task<int?> Create(Dependent dependent);
    Task<IEnumerable<Dependent>> GetAll();
    Task<Dependent> GetById(int id);
    Task<int?> Update(Dependent dependent);
    Task<int?> Delete(int id);
    Task<List<Dependent>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
