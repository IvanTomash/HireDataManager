using HireDataManager.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services.Interfaces;

public interface IDependentService
{
    Task<int?> Create(DependentDto dependent);
    Task<IEnumerable<DependentDto>> GetAll();
    Task<DependentDto> GetById(int id);
    Task<int?> Update(DependentDto dependent);
    Task<int?> Delete(int id);
    Task<List<DependentDto>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
