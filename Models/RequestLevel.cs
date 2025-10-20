using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class RequestLevel
{
    public int RequestLevelId { get; set; }

    public int? ApplicationLevelId { get; set; }

    public int? RequestId { get; set; }

    public int? AssignedToEmpId { get; set; }

    public DateTime? AssignedDate { get; set; }

    public int? RequestDetailsStatusId { get; set; }

    public int? ConversionToEmpId { get; set; }

    public DateTime? ActionDate { get; set; }

    public bool? InUse { get; set; }

    public virtual ApplicationLevel? ApplicationLevel { get; set; }

    public virtual Request? Request { get; set; }

    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();

    public virtual ICollection<RequestDetailsAttach> RequestDetailsAttaches { get; set; } = new List<RequestDetailsAttach>();

    public virtual RequestDetailsStatus? RequestDetailsStatus { get; set; }

    public virtual ICollection<RequestLevelAssigned> RequestLevelAssigneds { get; set; } = new List<RequestLevelAssigned>();
}
