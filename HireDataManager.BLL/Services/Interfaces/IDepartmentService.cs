
using HireDataManager.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services.Interfaces;

public interface IDepartmentService
{
    Task<int?> Create(DepartmentDto department);
    Task<IEnumerable<DepartmentDto>> GetAll();
    Task<DepartmentDto> GetById(int id);
    Task<int?> Update(DepartmentDto department);
    Task<int?> Delete(int id);
    Task<List<DepartmentDto>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
