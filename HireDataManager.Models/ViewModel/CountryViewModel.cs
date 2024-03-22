using HireDataManager.Models.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HireDataManager.Models.ViewModel;

public class CountryViewModel
{
    public CountryDto Country { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> RegionList { get; set; }
}
