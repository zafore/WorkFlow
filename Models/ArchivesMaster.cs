using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class ArchivesMaster
{
    public int ArchivesMasterId { get; set; }

    public string ArchivesMasterName { get; set; } = null!;

    public string ArchivesMasterNote { get; set; } = null!;

    public int DepartmentId { get; set; }

    public string CrUserId { get; set; } = null!;

    public DateTime CrDate { get; set; }

    public bool? ArchivesMasterActive { get; set; }

    public string? UpUserId { get; set; }

    public DateTime? UpDate { get; set; }

    public virtual ICollection<ApplicationLevel> ApplicationLevels { get; set; } = new List<ApplicationLevel>();

    public virtual ICollection<ApplicationRequirement> ApplicationRequirements { get; set; } = new List<ApplicationRequirement>();

    public virtual Department Department { get; set; } = null!;
}
