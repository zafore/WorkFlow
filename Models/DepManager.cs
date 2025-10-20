using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class DepManager
{
    public int DepManagerId { get; set; }

    public int DepartmentId { get; set; }

    public int EmployeId { get; set; }

    public bool? InUse { get; set; }

    public virtual Department Department { get; set; } = null!;
}
