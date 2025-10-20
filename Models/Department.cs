using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public int Divisionid { get; set; }

    public string? DepartmentNameEng { get; set; }

    public int? StatusId { get; set; }

    public int? DeptCreUid { get; set; }

    public DateTime? DeptCreDate { get; set; }

    public string? DepartmentNameAr { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<ArchivesMaster> ArchivesMasters { get; set; } = new List<ArchivesMaster>();

    public virtual ICollection<DepManager> DepManagers { get; set; } = new List<DepManager>();

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}
