using HireDataManager.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Models.ViewModel;

public class DependentViewModel
{
    public DependentDto Dependent { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> EmployeeList { get; set; }
}
