using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Models.Dto;

public class EmployeeDto
{
    public int EmployeeId { get; set; }

    [DisplayName("First Name")]
    [MaxLength(20, ErrorMessage = "The length can't be more than 25")]
    public string? FirstName { get; set; }

    [DisplayName("Last Name")]
    [Required]
    [MaxLength(25, ErrorMessage ="The length can't be more than 25")]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(100, ErrorMessage = "The length can't be more than 100")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [DisplayName("Phone Number")]
    [MaxLength(20, ErrorMessage = "The length can't be more than 25")]
    [Phone(ErrorMessage ="You have to enter a correct phone number")]
    public string? PhoneNumber { get; set; }

    [Required]
    [DisplayName("Hire Date")]
    [DataType(DataType.Date)]
    public DateOnly HireDate { get; set; }

    [Required]
    [DisplayName("Job")]
    public int JobId { get; set; }

    [ValidateNever]
    public virtual JobDto Job { get; set; } = null!;

    [Required]
    [Range(0, 99999)]
    public decimal Salary { get; set; }

    [DisplayName("Manager")]
    public int? ManagerId { get; set; }

    [ValidateNever]
    public virtual EmployeeDto? Manager { get; set; }

    [DisplayName("Department")]
    public int? DepartmentId { get; set; }

    [ValidateNever]
    public virtual DepartmentDto? Department { get; set; }
}
