using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DepartmentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int?> Create(Department department)
    {
        var result = await _dbContext.Departments.AddAsync(department);
        _dbContext.SaveChanges();
        return result.Entity.DepartmentId;
    }

    public async Task<int?> Delete(int id)
    {
        Department? deletedObject = await _dbContext.Departments.FindAsync(id);
        if (deletedObject != null)
        {
            var result = _dbContext.Departments.Remove(deletedObject);
            _dbContext.SaveChanges();
            return result.Entity.DepartmentId;
        }

        return null;
    }

    public async Task<IEnumerable<Department>> GetAll()
    {
        IEnumerable<Department> departments = _dbContext.Departments
            .Include(d => d.Location)
            .ToList();
        return departments;
    }

    public async Task<Department?> GetById(int id)
    {
        Department? department = await _dbContext.Departments.Include(d => d.Location).Where(d => d.DepartmentId == id).FirstAsync();
        if (department != null)
        {
            return department;
        }
        return null;
    }

    public async Task<List<Department>> GetPaginated(int page, int pageSize)
    {
        int recSkip = (page - 1) * pageSize;
        var data = await _dbContext.Departments
            .Skip(recSkip)
            .Take(pageSize)
            .Include(e => e.Location)
            .ToListAsync();

        return data;
    }

    public int GetTotalCount()
    {
        return _dbContext.Departments.Count();
    }

    public async Task<int?> Update(Department department)
    {
        var result = _dbContext.Departments.Update(department);
        _dbContext.SaveChanges();

        return result.Entity.DepartmentId;
    }
}
