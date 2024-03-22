using HireDataManager.BLL.Services;
using HireDataManager.BLL.Services.Interfaces;
using HireDataManager.DAL.Repositories.Interfaces;
using HireDataManager.Models;
using HireDataManager.Models.Dto;
using HireDataManager.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HireDataManager.Web.Controllers;

public class JobController : Controller
{
    private readonly ILogger<JobController> _logger;
    private readonly IJobService _jobService;
    private readonly string _controllerName;

    public JobController(ILogger<JobController> logger, IJobService jobService)
    {
        _logger = logger;
        _jobService = jobService;
        _controllerName = "Job";
    }

    public async Task<IActionResult> Index(int pg = 1)
    {
        const int pageSize = 4;
        if (pg < 1)
        {
            pg = 1;
        }

        Pager pager = new Pager(_jobService.GetTotalCount(), pg, _controllerName, pageSize);
        var data = await _jobService.GetPaginated(pager.CurrentPage, pager.PageSize);

        ViewBag.Pager = pager;
        return View(data);
    }

    public IActionResult Create(int pageIndex = 1)
    {
        ViewBag.PageIndex = pageIndex;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(JobDto obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _jobService.Create(obj);
            if (result != null)
            {
                return RedirectToAction("Index", new {pg = pageIndex});
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Update(int id, int pageIndex = 1 )
    {
        ViewBag.PageIndex = pageIndex;
        var result = await _jobService.GetById(id);
        if (result != null)
        {
            return View(result);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(JobDto obj, int pageIndex)
    {
        if (ModelState.IsValid)
        {
            var result = await _jobService.Update(obj);
            if (result != null)
            {
                return RedirectToAction("Index", new { pg = pageIndex });
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Delete(int id, int pageIndex = 1)
    {
        var result = await _jobService.Delete(id);
        if (result != null)
        {
            return RedirectToAction("Index", new { pg = pageIndex });
        }
        return BadRequest();
    }
}
