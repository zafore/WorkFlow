using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class DisplayLink
{
    public int DisplaylinkId { get; set; }

    public string DisplaylinkName { get; set; } = null!;

    public bool DisplaylinkInUse { get; set; }

    public string? DisplayFunction { get; set; }

    public virtual ICollection<ApplicationRequirement> ApplicationRequirements { get; set; } = new List<ApplicationRequirement>();
}
