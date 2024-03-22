using HireDataManager.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Models.ViewModel;

public class EmployeeViewModel
{
    public EmployeeDto Employee { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> JobList { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> EmployeeList { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> DepartmentList { get; set; }
}
