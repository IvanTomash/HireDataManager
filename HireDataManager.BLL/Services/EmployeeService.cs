using AutoMapper;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models;
using HireDataManager.Models.Dto;
using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public int GetTotalCount()
    {
        return _employeeRepository.GetTotalCount();
    }

    public Task<int?> Create(EmployeeDto employee)
    {
        Employee newEmployee = _mapper.Map<Employee>(employee);
        var result = _employeeRepository.Create(newEmployee);
        return result;
    }

    public async Task<int?> Delete(int job)
    {
       var result = await _employeeRepository.Delete(job);
       return result;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAll()
    {
        var employees = await _employeeRepository.GetAll();
        if (employees != null)
        {
            var employeeDtos = employees.Select(emp => _mapper.Map<EmployeeDto>(emp));
            return employeeDtos;
        }

        return null!;
    }

    public async Task<EmployeeDto> GetById(int id)
    {
        var result = _mapper.Map<EmployeeDto>(await _employeeRepository.GetById(id));
        return result;
    }

    public async Task<List<EmployeeDto>> GetPaginated(int page, int pageSize)
    {
        var result = _mapper.Map<List<EmployeeDto>>(await _employeeRepository.GetPaginated(page, pageSize));
        return result;
    }

    public Task<int?> Update(EmployeeDto employee)
    {
        Employee updatedEmployee = _mapper.Map<Employee>(employee);
        var result = _employeeRepository.Update(updatedEmployee);
        return result;
    }
}
