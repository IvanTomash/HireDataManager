using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories;

public class RegionRepository : IRegionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RegionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int?> Create(Region region)
    {
        var createdEntity = await _dbContext.Regions.AddAsync(region);
        _dbContext.SaveChanges();

        return createdEntity.Entity.RegionId;
    }

    public async Task<int?> Delete(int id)
    {
        Region? deletedObject = await _dbContext.Regions.FindAsync(id);
        if (deletedObject != null)
        {
            var result = _dbContext.Regions.Remove(deletedObject);
            _dbContext.SaveChanges();
            return result.Entity.RegionId;
        }

        return null;
    }

    public async Task<IEnumerable<Region>> GetAll()
    {
        IEnumerable<Region> regions = _dbContext.Regions.ToList();
        return regions;
    }

    public async Task<Region?> GetById(int id)
    {
        Region? region = await _dbContext.Regions.FindAsync(id);
        if (region != null)
        {
            return region;
        }
        return null;
    }

    public async Task<List<Region>> GetPaginated(int page, int pageSize)
    {
        int recSkip = (page - 1) * pageSize;
        var data = await _dbContext.Regions
            .Skip(recSkip)
            .Take(pageSize)
            .ToListAsync();

        return data;
    }

    public int GetTotalCount()
    {
        return _dbContext.Regions.Count();
    }

    public async Task<int?> Update(Region region)
    {
        var entity = _dbContext.Regions.Update(region);
        _dbContext.SaveChanges();
        return entity.Entity.RegionId;
    }
}
