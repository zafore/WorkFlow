using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class SharedTableDatum
{
    public int DropIdTable { get; set; }

    public string? DropName { get; set; }

    public string? DropId { get; set; }

    public int? SharedTableId { get; set; }
}
