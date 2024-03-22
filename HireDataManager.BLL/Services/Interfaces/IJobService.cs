using HireDataManager.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services.Interfaces;

public interface IJobService
{
    Task<int?> Create(JobDto job);
    Task<IEnumerable<JobDto>> GetAll();
    Task<JobDto> GetById(int id);
    Task<int?> Update(JobDto job);
    Task<int?> Delete(int job);
    Task<List<JobDto>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
