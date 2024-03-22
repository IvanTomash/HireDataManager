using HireDataManager.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Models.ViewModel;

public class DepartmentViewModel
{
    public DepartmentDto Department { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> LocationList { get; set; }
}
