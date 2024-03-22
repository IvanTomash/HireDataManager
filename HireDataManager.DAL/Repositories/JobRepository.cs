using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories;

public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _dbContext;

    public JobRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int?> Create(Job job)
    {
        var createdEntity = await _dbContext.Jobs.AddAsync(job); 
        _dbContext.SaveChanges();

        return createdEntity.Entity.JobId;
    }

    public async Task<int?> Delete(int id)
    {
        Job? deletedObject = await _dbContext.Jobs.FindAsync(id);
        if (deletedObject != null)
        {
            var result = _dbContext.Jobs.Remove(deletedObject);
            _dbContext.SaveChanges();
            return result.Entity.JobId;
        }
        return null;
    }

    public async Task<IEnumerable<Job>> GetAll()
    {
        IEnumerable<Job> jobs = _dbContext.Jobs.ToList();
        return jobs;
    }

    public async Task<Job?> GetById(int id)
    {
        Job? job = await _dbContext.Jobs.FindAsync(id);
        if (job != null)
        {
            return job;
        }
        return null;
    }

    public async Task<List<Job>> GetPaginated(int page, int pageSize)
    {
        int recSkip = (page - 1) * pageSize;
        var data = await _dbContext.Jobs
            .Skip(recSkip)
            .Take(pageSize)
            .ToListAsync();

        return data;
    }

    public int GetTotalCount()
    {
        return _dbContext.Jobs.Count();
    }

    public async Task<int?> Update(Job job)
    {
        var entity = _dbContext.Jobs.Update(job);
        _dbContext.SaveChanges();
        return entity.Entity.JobId;
    }
}
