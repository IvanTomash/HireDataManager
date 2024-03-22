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

public class LocationDto
{
    [ValidateNever]
    public int LocationId { get; set; }

    [MaxLength(40)]
    [DisplayName("Street Address")]
    public string? StreetAddress { get; set; }

    [MaxLength(12)]
    [DisplayName("Postal Code")]
    public string? PostalCode { get; set; }

    [Required]
    [MaxLength(30)]
    public string City { get; set; } = null!;

    [MaxLength(25)]
    [DisplayName("State Province")]
    public string? StateProvince { get; set; }

    [Required]
    [DisplayName("Country ID")]
    public string CountryId { get; set; } = null!;

    [ValidateNever]
    public virtual CountryDto Country { get; set; } = null!;
}
