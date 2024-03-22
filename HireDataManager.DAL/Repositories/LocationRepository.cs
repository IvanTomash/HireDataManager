using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LocationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int?> Create(Location location)
    {
        var result = await _dbContext.Locations.AddAsync(location);
        _dbContext.SaveChanges();
        return result.Entity.LocationId;
    }

    public async Task<int?> Delete(int id)
    {
        Location? deletedObject = await _dbContext.Locations.FindAsync(id);
        if (deletedObject != null)
        {
            var result = _dbContext.Locations.Remove(deletedObject);
            _dbContext.SaveChanges();
            return result.Entity.LocationId;
        }

        return null;
    }

    public async Task<IEnumerable<Location>> GetAll()
    {
        IEnumerable<Location> locations = _dbContext.Locations
            .Include(l => l.Country)
            .ToList();
        return locations;
    }

    public async Task<Location?> GetById(int id)
    {
        Location? location = await _dbContext.Locations.FindAsync(id);
        if (location != null)
        {
            return location;
        }
        return null;
    }

    public async Task<List<Location>> GetPaginated(int page, int pageSize)
    {
        int recSkip = (page - 1) * pageSize;
        var data = await _dbContext.Locations
            .Skip(recSkip)
            .Take(pageSize)
            .Include(e => e.Country)
            .ToListAsync();

        return data;
    }

    public int GetTotalCount()
    {
        return _dbContext.Locations.Count();
    }

    public async Task<int?> Update(Location location)
    {
        var result = _dbContext.Locations.Update(location);
        _dbContext.SaveChanges();
        return result.Entity.LocationId;
    }
}
