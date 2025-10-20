using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class Request
{
    public int RequestId { get; set; }

    public string RequestTitle { get; set; } = null!;

    public string RequestDescription { get; set; } = null!;

    public int CrUserId { get; set; }

    public int? ApplicationId { get; set; }

    public int? AssignedFromEmpId { get; set; }

    public int? RequestStatusId { get; set; }

    public DateTime? RequestDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsEnd { get; set; }

    public bool? IsComplate { get; set; }

    public DateTime? ComplateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedUser { get; set; }

    public virtual Application? Application { get; set; }

    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();

    public virtual ICollection<RequestLevel> RequestLevels { get; set; } = new List<RequestLevel>();

    public virtual RequestStatus? RequestStatus { get; set; }
}
