using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class Tool
{
    public int ToolsId { get; set; }

    public string ToolName { get; set; } = null!;

    public bool ToolInUse { get; set; }

    public bool FromDataBase { get; set; }

    public string? ImgUrl { get; set; }

    public string? ToolValue { get; set; }

    public virtual ICollection<ApplicationRequirement> ApplicationRequirements { get; set; } = new List<ApplicationRequirement>();
}
