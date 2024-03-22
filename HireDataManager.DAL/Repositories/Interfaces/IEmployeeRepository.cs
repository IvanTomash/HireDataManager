using HireDataManager.Models;
using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<int?> Create(Employee employee);
    Task<IEnumerable<Employee>> GetAll();
    Task<Employee> GetById(int employee);
    Task<int?> Update(Employee employee);
    Task<int?> Delete(int id);
    Task<List<Employee>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
