using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class ApplicationLevelAssigned
{
    public int ApplicationLevelAssignedId { get; set; }

    public int ApplicationLevelId { get; set; }

    public int? AssginTypeId { get; set; }

    public int? AssignedToEmpId { get; set; }

    public int? ManagerTypeId { get; set; }

    public bool? AssignedInUse { get; set; }

    public DateTime? CrDate { get; set; }

    public string? CrEmpId { get; set; }

    public DateTime? UpDate { get; set; }

    public string? UpEmpId { get; set; }

    public virtual ApplicationLevel ApplicationLevel { get; set; } = null!;
}
