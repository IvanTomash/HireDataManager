using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HireDataManager.Models.Entity;

public partial class Region
{
    public int RegionId { get; set; }

    public string? RegionName { get; set; }

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
