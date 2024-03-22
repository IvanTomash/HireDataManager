using HireDataManager.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Models.ViewModel;

public class LocationViewModel
{
    public LocationDto Location { get; set; } = new LocationDto();

    [ValidateNever]
    public IEnumerable<SelectListItem> CountryList { get; set; }
}
