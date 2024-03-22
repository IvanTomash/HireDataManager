using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Models.Dto;

public partial class DependentDto
{
    public int DependentId { get; set; }

    [Required]
    [MaxLength(50)]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(25)]
    public string Relationship { get; set; } = null!;

    [Required]
    [DisplayName("Employee ID")]
    public int EmployeeId { get; set; }

    [ValidateNever]
    public virtual EmployeeDto Employee { get; set; } = null!;
}

