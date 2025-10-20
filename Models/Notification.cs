using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int NotificationAppId { get; set; }

    public string NotificationSubject { get; set; } = null!;

    public string? NotificationBody { get; set; }

    public bool? SendEmail { get; set; }

    public DateTime? CrDate { get; set; }

    public int? CrEmpId { get; set; }

    public string? ControllerName { get; set; }

    public string? PageName { get; set; }

    public bool? InUse { get; set; }

    public virtual NotificationApp NotificationApp { get; set; } = null!;

    public virtual ICollection<Notifier> Notifiers { get; set; } = new List<Notifier>();
}
