using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class Section
{
    public int SectionId { get; set; }

    public string SectionNameEng { get; set; } = null!;

    public int DeptId { get; set; }

    public bool? SectionInUse { get; set; }

    public string? CrUserId { get; set; }

    public DateTime? CrDate { get; set; }

    public string? UpUserId { get; set; }

    public DateTime? UpDate { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Department Dept { get; set; } = null!;
}
