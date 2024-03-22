using HireDataManager.BLL.Services;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.Models;
using HireDataManager.Models.Dto;
using HireDataManager.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HireDataManager.Web.Controllers;

public class LocationController : Controller
{
    private readonly ILogger<LocationController> _logger;
    private readonly ILocationService _locationService;
    private readonly ICountryService _countryService;
    private readonly string _controllerName;
    
    public LocationController(ILogger<LocationController> logger, ILocationService locationService, ICountryService countryService)
    {
        _logger = logger;
        _locationService = locationService;
        _countryService = countryService;
        _controllerName = "Location";
    }

    public async Task<IActionResult> Index(int pg = 1)
    {
        const int pageSize = 4;
        if (pg < 1)
        {
            pg = 1;
        }

        Pager pager = new Pager(_locationService.GetTotalCount(), pg, _controllerName, pageSize);
        var data = await _locationService.GetPaginated(pager.CurrentPage, pager.PageSize);

        ViewBag.Pager = pager;
        return View(data);
    }

    public async Task<IActionResult> Create(int pageIndex = 1)
    {
        ViewBag.PageIndex = pageIndex;
        var countries = await _countryService.GetAll();
        var locationVM = new LocationViewModel()
        {
            Location = new LocationDto(),
            CountryList = countries.Select(x => new SelectListItem()
            {
                Text = x.CountryName,
                Value = x.CountryId.ToString()
            })
        };

        return View(locationVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LocationViewModel obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _locationService.Create(obj.Location);
            if (result != null)
            {
                return RedirectToAction("Index", new { pg = pageIndex });
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Update(int id, int pageIndex = 1)
    {
        ViewBag.PageIndex = pageIndex;
        var location = await _locationService.GetById(id);
        var countries = await _countryService.GetAll();
        LocationViewModel countryVM = new LocationViewModel()
        {
            Location = location,
            CountryList = countries.Select(x => new SelectListItem()
            {
                Text = x.CountryName,
                Value = x.CountryId,
            })
        };
        if (location != null)
        {
            return View(countryVM);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(LocationViewModel obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _locationService.Update(obj.Location);
            if (result != null)
            {
                return RedirectToAction("Index", new { pg = pageIndex });
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Delete(int id, int pageIndex = 1)
    {
        var result = await _locationService.Delete(id);
        if (result != null)
        {
            return RedirectToAction("Index", new { pg = pageIndex });
        }
        return BadRequest();
    }
}
