using HireDataManager.Models.Entity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Models.Dto;

public class CountryDto
{
    public string CountryId { get; set; } = string.Empty;

    [MaxLength(40)]
    [Display(Name = "Country Name")]
    public string? CountryName { get; set; }

    [Required]
    [DisplayName("Regiion ID")]
    public int? RegionId { get; set; }

    [ValidateNever]
    public virtual RegionDto Region { get; set; } = null!;
}
