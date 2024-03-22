using HireDataManager.BLL.Services;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.Models;
using HireDataManager.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HireDataManager.Web.Controllers;

public class RegionController : Controller
{
    private readonly ILogger<RegionController> _logger;
    private readonly IRegionService _regionService;
    private readonly string _controllerName;
   
    public RegionController(ILogger<RegionController> logger, IRegionService regionService)
    {
        _logger = logger;
        _regionService = regionService;
        _controllerName = "Region";

    }

    public async Task<IActionResult> Index(int pg = 1)
    {
        const int pageSize = 4;
        if (pg < 1)
        {
            pg = 1;
        }

        Pager pager = new Pager(_regionService.GetTotalCount(), pg, _controllerName, pageSize);
        var data = await _regionService.GetPaginated(pager.CurrentPage, pager.PageSize);

        ViewBag.Pager = pager;
        return View(data);
    }

    public IActionResult Create(int pageIndex = 1)
    {
        ViewBag.PageIndex = pageIndex;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RegionDto obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _regionService.Create(obj);
            if(result != null)
            {
                return RedirectToAction("Index", new {pg = pageIndex});
            }
        }
        return BadRequest();
    }


    public async Task<IActionResult> Update(int id, int pageIndex = 1)
    {
        ViewBag.PageIndex = pageIndex;
        var result = await _regionService.GetById(id);
        if (result != null)
        {
            return View(result);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(RegionDto obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _regionService.Update(obj);
            if (result != null)
            {
                return RedirectToAction("Index", new { pg = pageIndex });
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Delete(int id, int pageIndex=1)
    {
        var result = await _regionService.Delete(id);
        if (result != null)
        {
            return RedirectToAction("Index", new { pg = pageIndex});
        }
        return BadRequest();
    }
}
