using HireDataManager.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.BLL.Services.Interfaces;

public interface IRegionService
{
    Task<int?> Create(RegionDto region);
    Task<IEnumerable<RegionDto>> GetAll();
    Task<RegionDto> GetById(int id);
    Task<int?> Update(RegionDto region);
    Task<int?> Delete(int id);
    Task<List<RegionDto>> GetPaginated(int page, int pageSize);
    int GetTotalCount();
}
