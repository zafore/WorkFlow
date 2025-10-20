using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class SystemInfo
{
    public int SystemInfoId { get; set; }

    public string SystemInfoName { get; set; } = null!;

    public bool? IsActive { get; set; }
}
