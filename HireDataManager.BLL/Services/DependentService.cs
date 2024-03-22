using AutoMapper;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.DAL.Repositories;
using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models.Dto;
using HireDataManager.Models.Entity;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services;

public class DependentService : IDependentService
{
    private readonly IMapper _mapper;
    private readonly IDependentRepository _dependentRepository;

    public DependentService(IMapper mapper, IDependentRepository dependentRepository)
    {
        _mapper = mapper;
        _dependentRepository = dependentRepository;
    }


    public async Task<int?> Create(DependentDto dependent)
    {
        Dependent newDependent = _mapper.Map<Dependent>(dependent);
        var result = await _dependentRepository.Create(newDependent);
        return result;
    }

    public async Task<int?> Delete(int id)
    {
        var result = await _dependentRepository.Delete(id);
        return result;
    }

    public async Task<IEnumerable<DependentDto>> GetAll()
    {
        var dependents = await _dependentRepository.GetAll();
        if (dependents != null)
        {
            var dependentsDto = dependents.Select(region => _mapper.Map<DependentDto>(region));
            return dependentsDto;
        }
        return null!;
    }

    public async Task<DependentDto> GetById(int id)
    {
        var result = _mapper.Map<DependentDto>(await _dependentRepository.GetById(id));
        return result;
    }

    public async Task<List<DependentDto>> GetPaginated(int page, int pageSize)
    {
        var result = _mapper.Map<List<DependentDto>>(await _dependentRepository.GetPaginated(page, pageSize));
        return result;
    }
    
    public int GetTotalCount()
    {
        return _dependentRepository.GetTotalCount();
    }

    public async Task<int?> Update(DependentDto dependent)
    {
        Dependent updatedDependent = _mapper.Map<Dependent>(dependent);
        var result = await _dependentRepository.Update(updatedDependent);
        return result;
    }
}
