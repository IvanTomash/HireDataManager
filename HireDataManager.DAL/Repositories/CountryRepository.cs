using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CountryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string?> Create(Country country)
    {
        var result = await _dbContext.Countries.AddAsync(country);
        _dbContext.SaveChanges();
        return result.Entity.CountryId;
    }

    public async Task<string?> Delete(string id)
    {
        Country? deletedObject = await _dbContext.Countries.FindAsync(id);
        if (deletedObject != null)
        {
            var result = _dbContext.Countries.Remove(deletedObject);
            _dbContext.SaveChanges();
            return result.Entity.CountryId;
        }

        return null;
    }

    public async Task<IEnumerable<Country>> GetAll()
    {
        IEnumerable<Country> countries = await _dbContext.Countries
            .Include(c => c.Region)
            .ToListAsync();
        return countries;
    }

    public async Task<Country?> GetById(string id)
    {
        Country? country =  await _dbContext.Countries.Include(c => c.Region).Where(c => c.CountryId == id).FirstAsync();
        if (country != null)
        {
            return country;
        }
        return null;
    }

    public async Task<List<Country>> GetPaginated(int page, int pageSize)
    {
        int recSkip = (page - 1) * pageSize;
        var data = await _dbContext.Countries
            .Skip(recSkip)
            .Take(pageSize)
            .Include(e => e.Region)
            .ToListAsync();

        return data;
    }

    public int GetTotalCount()
    {
        return _dbContext.Countries.Count();
    }

    public async Task<string?> Update(Country job)
    {
        var result = _dbContext.Countries.Update(job);
        _dbContext.SaveChanges();
        return result.Entity.CountryId;
    }
}
