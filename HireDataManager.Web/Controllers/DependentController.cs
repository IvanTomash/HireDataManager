using HireDataManager.BLL.Services;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.Models;
using HireDataManager.Models.Dto;
using HireDataManager.Models.Entity;
using HireDataManager.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HireDataManager.Web.Controllers;

public class DependentController : Controller
{
    private readonly ILogger<DependentController> _logger;
    private readonly IDependentService _dependentService;
    private readonly IEmployeeService _employeeService;
    private readonly string _controllerName;

    public DependentController(ILogger<DependentController> logger, IDependentService dependentService, IEmployeeService employeeService)
    {
        _logger = logger;
        _dependentService = dependentService;
        _controllerName = "Dependent";
        _employeeService = employeeService;
    }

    public async Task<IActionResult> Index(int pg = 1)
    {
        const int pageSize = 4;
        if (pg < 1)
        {
            pg = 1;
        }

        Pager pager = new Pager(_dependentService.GetTotalCount(), pg, _controllerName, pageSize);
        var data = await _dependentService.GetPaginated(pager.CurrentPage, pager.PageSize);

        ViewBag.Pager = pager;
        return View(data);
    }

    public async Task<IActionResult> Create(int pageIndex = 1)
    {
        ViewBag.PageIndex = pageIndex;
        var employees = await _employeeService.GetAll();
        var dependentVM = new DependentViewModel()
        {
            Dependent = new DependentDto(),
            EmployeeList = employees.Select(x => new SelectListItem()
            {
                Text = $"{x.FirstName} {x.LastName}",
                Value = x.EmployeeId.ToString()
            })
        };

        return View(dependentVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DependentViewModel obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _dependentService.Create(obj.Dependent);
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
        var result = await _dependentService.GetById(id);
        var employees = await _employeeService.GetAll();
        var dependentVM = new DependentViewModel()
        {
            Dependent = result,
            EmployeeList = employees.Select(x => new SelectListItem()
            {
                Text = $"{x.FirstName} {x.LastName}",
                Value = x.EmployeeId.ToString()
            })
        };
        if (result != null)
        {
            return View(dependentVM);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(DependentViewModel obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _dependentService.Update(obj.Dependent);
            if (result != null)
            {
                return RedirectToAction("Index", new { pg = pageIndex });
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Delete(int id, int pageIndex = 1)
    {
        var result = await _dependentService.Delete(id);
        if (result != null)
        {
            return RedirectToAction("Index", new { pg = pageIndex });
        }
        return BadRequest();
    }
}
