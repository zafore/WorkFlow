using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class NotificationApp
{
    public int NotificationAppId { get; set; }

    public string NotificationAppName { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
