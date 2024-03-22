using HireDataManager.BLL.Services;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.Models;
using HireDataManager.Models.Dto;
using HireDataManager.Models.Entity;
using HireDataManager.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HireDataManager.Web.Controllers;

public class EmployeeController : Controller
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IEmployeeService _employeeService;
    private readonly IJobService _jobService;
    private readonly IDepartmentService _departmentService;
    private readonly string _controllerName;

    public EmployeeController(
        ILogger<EmployeeController> logger,
        IEmployeeService employeeService,
        IJobService jobService, 
        IDepartmentService departmentService)
    {
        _logger = logger;
        _employeeService = employeeService;
        _controllerName = "Employee";
        _jobService = jobService;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index(int pg = 1)
    {
        const int pageSize = 8;
        if (pg < 1)
        {
            pg = 1;
        }

        Pager pager = new Pager(_employeeService.GetTotalCount(), pg, _controllerName, pageSize);
        var data = await _employeeService.GetPaginated(pager.CurrentPage, pager.PageSize);

        ViewBag.Pager = pager;
        return View(data);
    }

    public async Task<IActionResult> Create(int pageIndex = 1)
    {
        ViewBag.PageIndex = pageIndex;
        var jobs = await _jobService.GetAll();
        var departments = await _departmentService.GetAll();
        var employees = await _employeeService.GetAll();
        var employeeVM = new EmployeeViewModel()
        {
            Employee = new EmployeeDto(),
            JobList = jobs.Select(x => new SelectListItem()
            {
                Text = x.JobTitle,
                Value = x.JobId.ToString(),
            }),
            EmployeeList = employees.Select(x => new SelectListItem()
            { 
                Text = $"{x.FirstName} {x.LastName}",
            }),
            DepartmentList = departments.Select(x => new SelectListItem()
            {
                Text = x.DepartmentName,
                Value = x.DepartmentId.ToString()
            })
        };

        ViewBag.PageIndex = pageIndex;
        return View(employeeVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeViewModel obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _employeeService.Create(obj.Employee);
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
        var result = await _employeeService.GetById(id);
        var jobs = await _jobService.GetAll();
        var employees = await _employeeService.GetAll();
        var departments = await _departmentService.GetAll();
        var employeeVM = new EmployeeViewModel()
        {
            Employee = result,
            JobList = jobs.Select(x => new SelectListItem()
            {
                Text = x.JobTitle,
                Value = x.JobId.ToString(),
            }),
            EmployeeList = employees.Select(x => new SelectListItem()
            {
                Text = $"{x.FirstName} {x.LastName}",
                Value = x.EmployeeId.ToString(),
            }),
            DepartmentList = departments.Select(x => new SelectListItem()
            {
                Text = x.DepartmentName,
                Value = x.DepartmentId.ToString()
            })
        };
        if (result != null)
        {
            return View(employeeVM);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(EmployeeViewModel obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _employeeService.Update(obj.Employee);
            if (result != null)
            {
                return RedirectToAction("Index", new { pg = pageIndex });
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Delete(int id, int pageIndex = 1)
    {
        var result = await _employeeService.Delete(id);
        if (result != null)
        {
            return RedirectToAction("Index", new { pg = pageIndex });
        }
        return BadRequest();
    }
}
