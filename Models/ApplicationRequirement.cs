using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class ApplicationRequirement
{
    public int ApplicationRequirementId { get; set; }

    public int ApplicationLevelId { get; set; }

    public int? ApplicationId { get; set; }

    public int? ArchivesMasterId { get; set; }

    public string ColumnNameValue { get; set; } = null!;

    public int ToolsId { get; set; }

    public bool IsRequired { get; set; }

    public int? SharedTableId { get; set; }

    public bool ApplicationRequirementInUse { get; set; }

    public DateTime CrDate { get; set; }

    public int CrEmpId { get; set; }

    public DateTime? UpDate { get; set; }

    public int? UpEmpId { get; set; }

    public bool? InStart { get; set; }

    public int? DisplaylinkId { get; set; }

    public string? FixId { get; set; }

    public string? FnRun { get; set; }

    public int? DisplayIndex { get; set; }

    public bool? UseMin { get; set; }

    public virtual Application? Application { get; set; }

    public virtual ApplicationLevel ApplicationLevel { get; set; } = null!;

    public virtual ICollection<ApplicationProcuderDetail> ApplicationProcuderDetails { get; set; } = new List<ApplicationProcuderDetail>();

    public virtual ICollection<ApplicationRequirementDetail> ApplicationRequirementDetails { get; set; } = new List<ApplicationRequirementDetail>();

    public virtual ArchivesMaster? ArchivesMaster { get; set; }

    public virtual DisplayLink? Displaylink { get; set; }

    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();

    public virtual ICollection<RequestDetailsAttach> RequestDetailsAttaches { get; set; } = new List<RequestDetailsAttach>();

    public virtual SharedTable? SharedTable { get; set; }

    public virtual Tool Tools { get; set; } = null!;
}
