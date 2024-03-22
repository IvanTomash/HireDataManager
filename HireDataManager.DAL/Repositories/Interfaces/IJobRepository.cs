using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories.Interfaces;

public interface IJobRepository
{
    Task<int?> Create(Job job);
    Task<IEnumerable<Job>> GetAll();
    Task<Job> GetById(int id);
    Task<int?> Update(Job job);
    Task<int?> Delete(int id);
    Task<List<Job>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
