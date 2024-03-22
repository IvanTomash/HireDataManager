using HireDataManager.Models;
using HireDataManager.Models.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<int?> Create(Employee employee, IFormFile photo);
    Task<IEnumerable<Employee>> GetAll();
    Task<Employee> GetById(int employee);
    Task<int?> Update(Employee employee, IFormFile photo);
    Task<int?> Delete(int id);
    Task<List<Employee>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
    string GetPhoto(int employeeId);
}
