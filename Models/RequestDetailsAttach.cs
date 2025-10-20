using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class RequestDetailsAttach
{
    public int RequestDetailsAttachId { get; set; }

    public int RequestId { get; set; }

    public int ApplicationRequirementId { get; set; }

    public string RequestDetailsAttachPath { get; set; } = null!;

    public string RequestDetailsAttachName { get; set; } = null!;

    public DateTime CrDate { get; set; }

    public int CrEmpId { get; set; }

    public int? RequestLevelId { get; set; }

    public int? RequestDetailsId { get; set; }

    public virtual ApplicationRequirement ApplicationRequirement { get; set; } = null!;

    public virtual RequestLevel? RequestLevel { get; set; }
}
