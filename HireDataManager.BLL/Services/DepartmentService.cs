using AutoMapper;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.DAL.Repositories;
using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models.Dto;
using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IMapper _mapper;
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IMapper mapper, IDepartmentRepository departmentRepository)
    {
        _mapper = mapper;
        _departmentRepository = departmentRepository;
    }

    public Task<int?> Create(DepartmentDto department)
    {
        Department newDepartment = _mapper.Map<Department>(department);
        var result = _departmentRepository.Create(newDepartment);
        return result;
    }

    public async Task<int?> Delete(int id)
    {
        var result = await _departmentRepository.Delete(id);
        return result;
    }

    public async Task<IEnumerable<DepartmentDto>> GetAll()
    {
        var departments = await _departmentRepository.GetAll();
        if (departments != null)
        {
            var departmentsDto = departments.Select(c => _mapper.Map<DepartmentDto>(c));
            return departmentsDto;
        }

        return null!;
    }

    public async Task<DepartmentDto> GetById(int id)
    {
        var result = _mapper.Map<DepartmentDto>(await _departmentRepository.GetById(id));
        return result;
    }

    public async Task<List<DepartmentDto>> GetPaginated(int page, int pageSize)
    {
        var result = _mapper.Map<List<DepartmentDto>>(await _departmentRepository.GetPaginated(page, pageSize));
        return result;
    }

    public int GetTotalCount()
    {
        return _departmentRepository.GetTotalCount();
    }

    public async Task<int?> Update(DepartmentDto department)
    {
        Department updatedDepartment = _mapper.Map<Department>(department);
        var result = await _departmentRepository.Update(updatedDepartment);
        return result;
    }
}
