using HireDataManager.Models;
using HireDataManager.Models.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services.Interfaces;

public interface IEmployeeService
{
    int GetTotalCount();
    Task<int?> Create(EmployeeDto employee, IFormFile photo);
    Task<IEnumerable<EmployeeDto>> GetAll();
    Task<EmployeeDto> GetById(int id);
    Task<int?> Update(EmployeeDto employee, IFormFile photo);
    Task<int?> Delete(int id);
    Task<List<EmployeeDto>> GetPaginated(int page, int pageSize);
}
