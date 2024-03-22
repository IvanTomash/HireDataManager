using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Models.Dto;

public class RegionDto
{
    public int RegionId { get; set; }

    [Required]
    [MaxLength(25)]
    [DisplayName("Region Name")]
    public string? RegionName { get; set; }
}
