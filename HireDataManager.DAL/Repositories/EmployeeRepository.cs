using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models;
using HireDataManager.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace HireDataManager.DAL.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly string _destinationFolder;
    private readonly DirectoryInfo _directoryInfo;

    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _destinationFolder = @"D:\my projects\HireDataManager\HireDataManager.Web\wwwroot\lib\images";
        _directoryInfo = new DirectoryInfo(_destinationFolder);
    }

    public int GetTotalCount()
    {
        return _dbContext.Employees.Count();
    }

    public async Task<int?> Create(Employee employee, IFormFile photo)
    {
        var result = await _dbContext.Employees.AddAsync(employee);
        _dbContext.SaveChanges();
        await AddPhoto(photo, result.Entity.EmployeeId);
        return result.Entity.EmployeeId;
    }

    public async Task<int?> Delete(int id)
    {
        Employee? deletedObject = await _dbContext.Employees.FindAsync(id);
        if (deletedObject != null)
        {
            var result = _dbContext.Employees.Remove(deletedObject);
            string photoPath = GetPhoto(result.Entity.EmployeeId);
            if (photoPath != null)
            {
                System.IO.File.Delete(photoPath);
            }
            FileInfo[] files = _directoryInfo.GetFiles();
           
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

    public async Task<int?> Update(Employee employee, IFormFile photo)
    {
        var result = _dbContext.Employees.Update(employee);
        string photoPath = GetPhoto(employee.EmployeeId);
        if (photoPath != null)
        {
            System.IO.File.Delete(photoPath);
        }
        await AddPhoto(photo, result.Entity.EmployeeId);
        _dbContext.SaveChanges();
        return result.Entity.EmployeeId;
    }

    public string GetPhoto(int employeeId)
    {
        FileInfo[] files = _directoryInfo.GetFiles();
        try
        {
            FileInfo photo = files.Where(x => Convert.ToInt32(Path.GetFileNameWithoutExtension(x.Name)) == employeeId).FirstOrDefault()!;
            return photo.FullName;
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }

        return null!;
    }

    private async Task AddPhoto(IFormFile photo, int? id)
    {
        var filePath = Path.Combine(_destinationFolder, id+Path.GetExtension(photo.FileName));
        using(var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(fileStream);
        }
    }
}
