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

public class RegionService : IRegionService
{
    private readonly IMapper _mapper;
    private readonly IRegionRepository _regionRepository;

    public RegionService(IMapper mapper, IRegionRepository regionRepository)
    {
        _mapper = mapper;
        _regionRepository = regionRepository;
    }


    public async Task<int?> Create(RegionDto region)
    {
        Region newRegion = _mapper.Map<Region>(region); 

        int? result = await _regionRepository.Create(newRegion);
        return result;
    }

    public async Task<int?> Delete(int id)
    {
        var result = await _regionRepository.Delete(id);
        return result;
    }

    public async Task<IEnumerable<RegionDto>> GetAll()
    {
        var regions = await _regionRepository.GetAll();
        if (regions != null)
        {
            var regionsDto = regions.Select(region => _mapper.Map<RegionDto>(region));
            return regionsDto;
        }
        return null!;
    }

    public async Task<RegionDto> GetById(int id)
    {
        var region = await _regionRepository.GetById(id);
        if (region != null)
        {
            var regionDto = _mapper.Map<RegionDto>(region);
            return regionDto;
        }
        return null!;
    }

    public async Task<List<RegionDto>> GetPaginated(int page, int pageSize)
    {
        var result = _mapper.Map<List<RegionDto>>(await _regionRepository.GetPaginated(page, pageSize));
        return result;
    }

    public int GetTotalCount()
    {
        return _regionRepository.GetTotalCount();
    }

    public async Task<int?> Update(RegionDto region)
    {
        var updatedEntity = _mapper.Map<Region>(region);
        var result = await _regionRepository.Update(updatedEntity);
        return result;
    }
}
