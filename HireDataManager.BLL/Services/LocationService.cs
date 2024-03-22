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

public class LocationService : ILocationService
{
    private readonly IMapper _mapper;
    private readonly ILocationRepository _locationRepository;

    public LocationService(IMapper mapper, ILocationRepository locationRepository)
    {
        _mapper = mapper;
        _locationRepository = locationRepository;
    }


    public Task<int?> Create(LocationDto location)
    {
        var newLocation = _mapper.Map<Location>(location);
        var result = _locationRepository.Create(newLocation);
        return result;
    }

    public Task<int?> Delete(int id)
    {
        var result = _locationRepository.Delete(id);
        return result;
    }

    public async Task<IEnumerable<LocationDto>> GetAll()
    {
        var locations = await _locationRepository.GetAll();
        if (locations != null)
        {
            var locationDtos = locations.Select(loc => _mapper.Map<LocationDto>(loc));
            return locationDtos;
        }
        return null!;
    }

    public async Task<LocationDto> GetById(int id)
    {
        var location = await _locationRepository.GetById(id);
        LocationDto locationDto = _mapper.Map<LocationDto>(location);
        return locationDto;
    }

    public async Task<List<LocationDto>> GetPaginated(int page, int pageSize)
    {
        var result = _mapper.Map<List<LocationDto>>(await _locationRepository.GetPaginated(page, pageSize));
        return result;
    }

    public int GetTotalCount()
    {
        return _locationRepository.GetTotalCount();
    }

    public async Task<int?> Update(LocationDto location)
    {
        var updatedLocation = _mapper.Map<Location>(location);
        var result = await _locationRepository.Update(updatedLocation);
        return result;
    }
}
