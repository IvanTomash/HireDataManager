using HireDataManager.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services.Interfaces;

public interface ILocationService
{
    Task<int?> Create(LocationDto location);
    Task<IEnumerable<LocationDto>> GetAll();
    Task<LocationDto> GetById(int id);
    Task<int?> Update(LocationDto location);
    Task<int?> Delete(int id);
    Task<List<LocationDto>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
