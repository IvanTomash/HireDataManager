using System;
using System.Collections.Generic;

namespace HireDataManager.Models.Entity;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int? LocationId { get; set; }
    
    public virtual Location? Location { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
