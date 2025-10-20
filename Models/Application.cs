using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public string ApplicationNameEng { get; set; } = null!;

    public string ApplicationNameAr { get; set; } = null!;

    public int DepartmentId { get; set; }

    public int? SectionId { get; set; }

    public DateTime CrDate { get; set; }

    public string CrEmpId { get; set; } = null!;

    public DateTime? UpDate { get; set; }

    public string? UpEmpId { get; set; }

    public bool ApplicationInUse { get; set; }

    public int? SystemInfoId { get; set; }

    public virtual ICollection<ApplicationLevel> ApplicationLevels { get; set; } = new List<ApplicationLevel>();

    public virtual ICollection<ApplicationRequirement> ApplicationRequirements { get; set; } = new List<ApplicationRequirement>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual Section? Section { get; set; }
}
