using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class ApplicationProcuderDetail
{
    public int ProcuderDetailId { get; set; }

    public int ApplicationLevelId { get; set; }

    public int ApplicationRequirementId { get; set; }

    public string ColumnName { get; set; } = null!;

    public virtual ApplicationLevel ApplicationLevel { get; set; } = null!;

    public virtual ApplicationRequirement ApplicationRequirement { get; set; } = null!;
}
