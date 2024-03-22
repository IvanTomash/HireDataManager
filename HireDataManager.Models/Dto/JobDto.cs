using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Models.Dto;

public class JobDto
{
    public int JobId { get; set; }

    [Required(ErrorMessage = "Job title is required!")]
    [DisplayName("Job Title")]
    [MaxLength(35)]
    public string JobTitle { get; set; } = null!;

    [DisplayName("Min Salary")]
    public decimal? MinSalary { get; set; }

    [DisplayName("Max Salary")]
    public decimal? MaxSalary { get; set; }
}
