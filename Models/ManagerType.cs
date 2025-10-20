using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class ManagerType
{
    public int ManagerTypeId { get; set; }

    public string ManagerTypeName { get; set; } = null!;

    public bool InUse { get; set; }
}
