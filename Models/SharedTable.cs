using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class SharedTable
{
    public int SharedTableId { get; set; }

    public string SharedTableName { get; set; } = null!;

    public string SharedTableColumn { get; set; } = null!;

    public string SharedTableValue { get; set; } = null!;

    public string SharedTableNameDisplayed { get; set; } = null!;

    public bool? InUse { get; set; }

    public virtual ICollection<ApplicationRequirement> ApplicationRequirements { get; set; } = new List<ApplicationRequirement>();

    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();
}
