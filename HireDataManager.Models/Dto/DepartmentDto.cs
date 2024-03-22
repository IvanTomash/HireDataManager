using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Models.Dto;

public class DepartmentDto
{
    public int DepartmentId { get; set; }

    [Required]
    [MaxLength(30)]
    [DisplayName("Department Name")]
    public string DepartmentName { get; set; } = null!;

    [DisplayName("Location ID")]
    public int? LocationId { get; set; }

    public virtual LocationDto? Location { get; set; }
}
