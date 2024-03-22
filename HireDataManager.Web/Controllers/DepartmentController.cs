using HireDataManager.BLL.Services;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.Models;
using HireDataManager.Models.Dto;
using HireDataManager.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HireDataManager.Web.Controllers;

public class DepartmentController : Controller
{
    private readonly ILogger<DepartmentController> _logger;
    private readonly IDepartmentService _departmentService;
    private readonly ILocationService _locationService;
    private readonly string _controllerName;

    public DepartmentController(ILogger<DepartmentController> logger, IDepartmentService departmentService, ILocationService locationService)
    {
        _logger = logger;
        _departmentService = departmentService;
        _controllerName = "Department";
        _locationService = locationService;
    }

    public async Task<IActionResult> Index(int pg = 1)
    {
        const int pageSize = 4;
        if (pg < 1)
        {
            pg = 1;
        }

        Pager pager = new Pager(_departmentService.GetTotalCount(), pg, _controllerName, pageSize);
        var data = await _departmentService.GetPaginated(pager.CurrentPage, pager.PageSize);

        ViewBag.Pager = pager;
        return View(data);
    }

    public async Task<IActionResult> Create(int pageIndex = 1)
    {
        ViewBag.PageIndex = pageIndex;
        var locations = await _locationService.GetAll();
        var departmentVM = new DepartmentViewModel()
        {
            Department = new DepartmentDto(),
            LocationList = locations.Select(x => new SelectListItem()
            {
                Text = $"{x.City}, {x.StreetAddress}",
                Value = x.LocationId.ToString()
            })
        };

        return View(departmentVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentViewModel obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _departmentService.Create(obj.Department);
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
        var result = await _departmentService.GetById(id);
        var locations = await _locationService.GetAll();
        DepartmentViewModel departmentVM = new DepartmentViewModel()
        {
            Department = result,
            LocationList = locations.Select(x => new SelectListItem()
            {
                Text = $"{x.City}, {x.StreetAddress}",
                Value = x.LocationId.ToString()
            })
        };
        if (result != null)
        {
            return View(departmentVM);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(DepartmentViewModel obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _departmentService.Update(obj.Department);
            if (result != null)
            {
                return RedirectToAction("Index", new { pg = pageIndex });
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Delete(int id, int pageIndex = 1)
    {
        var result = await _departmentService.Delete(id);
        if (result != null)
        {
            return RedirectToAction("Index", new { pg = pageIndex });
        }
        return BadRequest();
    }
}
