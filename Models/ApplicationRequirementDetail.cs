using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class ApplicationRequirementDetail
{
    public int ApplicationReqDetailsId { get; set; }

    public int ApplicationRequirementId { get; set; }

    public string AppReqDValue { get; set; } = null!;

    public bool ApplicationReqDetailsInUse { get; set; }

    public DateTime CrDate { get; set; }

    public int CrEmpId { get; set; }

    public DateTime? UpDate { get; set; }

    public int? UpEmpId { get; set; }

    public virtual ApplicationRequirement ApplicationRequirement { get; set; } = null!;

    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();
}
