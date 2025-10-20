using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class Notifier
{
    public int NotifierId { get; set; }

    public int NotificationId { get; set; }

    public int EmpId { get; set; }

    public bool IsRead { get; set; }

    public DateTime? ReadDate { get; set; }

    public bool? IsSend { get; set; }

    public virtual Notification Notification { get; set; } = null!;
}
