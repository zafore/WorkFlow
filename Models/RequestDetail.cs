using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class RequestDetail
{
    public int RequestDetailsId { get; set; }

    public int ApplicationRequirementId { get; set; }

    public int? RequestId { get; set; }

    public int? SApplicationReqDetailsId { get; set; }

    public int? SSharedTableId { get; set; }

    public int? SSharedTableIdValue { get; set; }

    public string? SValue { get; set; }

    public DateTime? SDate { get; set; }

    public int? RequestLevelId { get; set; }

    public virtual ApplicationRequirement ApplicationRequirement { get; set; } = null!;

    public virtual Request? Request { get; set; }

    public virtual RequestLevel? RequestLevel { get; set; }

    public virtual ApplicationRequirementDetail? SApplicationReqDetails { get; set; }

    public virtual SharedTable? SSharedTable { get; set; }
}
