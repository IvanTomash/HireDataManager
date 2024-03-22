using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models;
using HireDataManager.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.DAL.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int GetTotalCount()
    {
        return _dbContext.Employees.Count();
    }

    public async Task<int?> Create(Employee employee)
    {
        var result = await _dbContext.Employees.AddAsync(employee);
        _dbContext.SaveChanges();
        return result.Entity.EmployeeId;
    }

    public async Task<int?> Delete(int id)
    {
        Employee? deletedObject = await _dbContext.Employees.FindAsync(id);
        if (deletedObject != null)
        {
            var result = _dbContext.Employees.Remove(deletedObject);
            _dbContext.SaveChanges();
            return result.Entity.EmployeeId;
        }

        return null;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        IEnumerable<Employee> employees = await _dbContext.Employees
            .Include(e => e.Department)
            .Include(e => e.Manager)
            .Include(e => e.Job)
            .ToListAsync();
        return employees;
    }

    public async Task<Employee?> GetById(int id)
    {
        Employee? employee = await _dbContext.Employees.Include(e => e.Department).Include(e => e.Job).Where(e => e.EmployeeId == id).FirstAsync();
        if (employee != null)
        {
            return employee;
        }
        return null;
    }

    public async Task<List<Employee>> GetPaginated(int page, int pageSize)
    {
        int recSkip = (page - 1) * pageSize;
        var data =  await _dbContext.Employees
            .Skip(recSkip)
            .Take(pageSize)
            .Include(e=> e.Department)
            .Include(e => e.Manager)
            .Include(e => e.Job)
            .ToListAsync();
        
        return data;
    }

    public async Task<int?> Update(Employee employee)
    {
        var result = _dbContext.Employees.Update(employee);
        _dbContext.SaveChanges();
        return result.Entity.EmployeeId;
    }
}
