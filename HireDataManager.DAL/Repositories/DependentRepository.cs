using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories;

public class DependentRepository : IDependentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DependentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int?> Create(Dependent dependent)
    {
        var result = await _dbContext.Dependents.AddAsync(dependent);
        _dbContext.SaveChanges();
        return result.Entity.DependentId;
    }

    public async Task<int?> Delete(int id)
    {
        Dependent? deletedObject = await _dbContext.Dependents.FindAsync(id);
        if (deletedObject != null)
        {
            var result = _dbContext.Dependents.Remove(deletedObject);
            _dbContext.SaveChanges();
            return result.Entity.DependentId;
        }
        return null;
    }

    public async Task<IEnumerable<Dependent>> GetAll()
    {
        IEnumerable<Dependent> dependents = _dbContext.Dependents
            .Include(d => d.Employee)
            .ToList();
        return dependents;
    }

    public async Task<Dependent?> GetById(int id)
    {
        Dependent? dependent = await _dbContext.Dependents.Where(d => d.DependentId == id).FirstAsync();
        if (dependent != null)
        {
            return dependent;
        }
        return null;
    }

    public async Task<List<Dependent>> GetPaginated(int page, int pageSize)
    {
        int recSkip = (page - 1) * pageSize;
        var data = await _dbContext.Dependents
            .Skip(recSkip)
            .Take(pageSize)
            .Include(e => e.Employee)
            .ToListAsync();

        return data;
    }

    public int GetTotalCount()
    {
        return _dbContext.Dependents.Count();
    }

    public async Task<int?> Update(Dependent dependent)
    {
        var result = _dbContext.Dependents.Update(dependent);
        await _dbContext.SaveChangesAsync();
        return result.Entity.DependentId;
    }
}
