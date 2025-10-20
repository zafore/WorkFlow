using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class Action
{
    public int ActionId { get; set; }

    public string ActionName { get; set; } = null!;

    public bool ActionInUse { get; set; }

    public string ActionNameArabic { get; set; } = null!;

    public bool MoveNext { get; set; }

    public bool SuspendIn { get; set; }

    public bool? IsSpecial { get; set; }

    public int? ForApplicationId { get; set; }

    public virtual ICollection<ApplicationNotification> ApplicationNotifications { get; set; } = new List<ApplicationNotification>();

    public virtual ICollection<LinkCondation> LinkCondationActions { get; set; } = new List<LinkCondation>();

    public virtual ICollection<LinkCondation> LinkCondationChangeActions { get; set; } = new List<LinkCondation>();
}
