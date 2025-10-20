using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class ApplicationLink
{
    public int ApplictionLinkId { get; set; }

    public string LinkName { get; set; } = null!;

    public string LinkNameAr { get; set; } = null!;

    public string LinkNameEng { get; set; } = null!;

    public string DisplayFunction { get; set; } = null!;

    public int FromTapplicationLevelId { get; set; }

    public int ToApplicationLevelId { get; set; }

    public string TemplateId { get; set; } = null!;

    public double Length { get; set; }

    public bool? InUse { get; set; }

    public virtual ICollection<ApplicationNotification> ApplicationNotifications { get; set; } = new List<ApplicationNotification>();

    public virtual ApplicationLevel FromTapplicationLevel { get; set; } = null!;

    public virtual ICollection<LinkCondation> LinkCondations { get; set; } = new List<LinkCondation>();

    public virtual ApplicationLevel ToApplicationLevel { get; set; } = null!;
}
