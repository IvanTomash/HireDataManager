using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories.Interfaces;

public interface IDepartmentRepository
{
    Task<int?> Create(Department department);
    Task<IEnumerable<Department>> GetAll();
    Task<Department> GetById(int id);
    Task<int?> Update(Department department);
    Task<int?> Delete(int id);
    Task<List<Department>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
