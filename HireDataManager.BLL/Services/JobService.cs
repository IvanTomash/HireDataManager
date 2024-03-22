using AutoMapper;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models.Dto;
using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services;

public class JobService : IJobService
{
    private readonly IMapper _mapper;
    private readonly IJobRepository _jobRepository;

    public JobService(IMapper mapper, IJobRepository jobRepository)
    {
        _mapper = mapper;
        _jobRepository = jobRepository;
    }


    public async Task<int?> Create(JobDto job)
    {
        Job newJob = _mapper.Map<Job>(job);
        
        int? result = await _jobRepository.Create(newJob);

        return result;
    }

    public async Task<int?> Delete(int id)
    {
        var result = await _jobRepository.Delete(id);
        return result;
    }

    public async Task<IEnumerable<JobDto>> GetAll()
    {
        var jobs = await _jobRepository.GetAll();
        if (jobs != null)
        {
            var jobDtos = jobs.Select(j => _mapper.Map<JobDto>(j));
            return jobDtos;
        }
        return null!;
    }

    public async Task<JobDto> GetById(int id)
    {
        var job = await _jobRepository.GetById(id);
        if (job != null)
        {
            var jobDto = _mapper.Map<JobDto>(job);
            return jobDto;
        }
        return null!;
    }

    public async Task<List<JobDto>> GetPaginated(int page, int pageSize)
    {
        var result = _mapper.Map<List<JobDto>>(await _jobRepository.GetPaginated(page, pageSize));
        return result;
    }

    public int GetTotalCount()
    {
        return _jobRepository.GetTotalCount();
    }

    public async Task<int?> Update(JobDto job)
    {
        var updatedEntity = _mapper.Map<Job>(job);
        var result = await _jobRepository.Update(updatedEntity);
        return result;
    }
}
