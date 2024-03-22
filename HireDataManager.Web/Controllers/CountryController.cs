using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.Models.Entity;
using HireDataManager.Models;
using Microsoft.AspNetCore.Mvc;
using HireDataManager.Models.Dto;
using HireDataManager.BLL.Services;
using HireDataManager.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using HireDataManager.DAL.Repositories.Interfaces;


namespace HireDataManager.Web.Controllers;

public class CountryController : Controller
{
    private readonly ILogger<CountryController> _logger;
    private readonly ICountryService _countryService;
    private readonly IRegionService _regionService;
    private readonly string _controllerName;

    public CountryController(ILogger<CountryController> logger, ICountryService countryService, IRegionService regionService)
    {
        _logger = logger;
        _countryService = countryService;
        _regionService = regionService;
        _controllerName = "Country";
    }

    public async Task<IActionResult> Index(int pg = 1)
    {
        const int pageSize = 4;
        if (pg < 1)
        {
            pg = 1;
        }

        Pager pager = new Pager(_countryService.GetTotalCount(), pg, _controllerName, pageSize);
        var data = await _countryService.GetPaginated(pager.CurrentPage, pager.PageSize);

        ViewBag.Pager = pager;
        return View(data);
    }

    public async Task<IActionResult> Create(int pageIndex = 1)
    {
        ViewBag.PageIndex = pageIndex;
        var regions = await _regionService.GetAll();
        var countryVM = new CountryViewModel()
        {
            Country = new CountryDto(),
            RegionList = regions.Select(x => new SelectListItem()
            {
                Text = x.RegionName,
                Value =x.RegionId.ToString()
            })
        };

        return View(countryVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CountryViewModel obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _countryService.Create(obj.Country);
            if (result != null)
            {
                return RedirectToAction("Index", new { pg = pageIndex });
            }
        }        
        return BadRequest();
    }

    public async Task<IActionResult> Update(string id, int pageIndex = 1)
    {
        ViewBag.PageIndex = pageIndex;
        var result = await _countryService.GetById(id);
        var regions = await _regionService.GetAll();
        CountryViewModel countryVM = new CountryViewModel()
        {
            Country = result,
            RegionList = regions.Select(x => new SelectListItem()
            {
                Text = x.RegionName,
                Value = x.RegionId.ToString(),
            })
        };
        if (result != null)
        {
            return View(countryVM);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(CountryViewModel obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _countryService.Update(obj.Country);
            if (result != null)
            {
                return RedirectToAction("Index", new { pg = pageIndex });
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Delete(string id, int pageIndex = 1)
    {
        var result = await _countryService.Delete(id);
        if (result != null)
        {
            return RedirectToAction("Index", new { pg = pageIndex });
        }
        return BadRequest();
    }
}
