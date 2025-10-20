using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class ApplicationLevel
{
    public int ApplicationLevelId { get; set; }

    public int? ApplicationId { get; set; }

    public int? ArchivesMasterId { get; set; }

    public string ApplicationLevelName { get; set; } = null!;

    public int ApplicationLevelIndex { get; set; }

    public bool CanShowAttach { get; set; }

    public bool CanEndRequest { get; set; }

    public bool CanReturn { get; set; }

    public bool CanReject { get; set; }

    public bool CanConversion { get; set; }

    public bool? ApplicationLevelInUse { get; set; }

    public DateTime? ApplicationLevelUpDate { get; set; }

    public int? ApplicationLevelUpEmpId { get; set; }

    public string? ProcuderName { get; set; }

    public string? Tbname { get; set; }

    public string? Notification { get; set; }

    public bool? HiddenLevel { get; set; }

    public double? X { get; set; }

    public double? Y { get; set; }

    public string? Fill { get; set; }

    public string? TemplateId { get; set; }

    public virtual Application? Application { get; set; }

    public virtual ICollection<ApplicationLable> ApplicationLableFromApplicationLevels { get; set; } = new List<ApplicationLable>();

    public virtual ICollection<ApplicationLable> ApplicationLableToApplicationLevels { get; set; } = new List<ApplicationLable>();

    public virtual ICollection<ApplicationLevelAssigned> ApplicationLevelAssigneds { get; set; } = new List<ApplicationLevelAssigned>();

    public virtual ICollection<ApplicationLink> ApplicationLinkFromTapplicationLevels { get; set; } = new List<ApplicationLink>();

    public virtual ICollection<ApplicationLink> ApplicationLinkToApplicationLevels { get; set; } = new List<ApplicationLink>();

    public virtual ICollection<ApplicationProcuderDetail> ApplicationProcuderDetails { get; set; } = new List<ApplicationProcuderDetail>();

    public virtual ICollection<ApplicationRequirement> ApplicationRequirements { get; set; } = new List<ApplicationRequirement>();

    public virtual ArchivesMaster? ArchivesMaster { get; set; }

    public virtual ICollection<RequestLevel> RequestLevels { get; set; } = new List<RequestLevel>();
}
