using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class ApplicationNotification
{
    public int ApplicationNotificationId { get; set; }

    public int ActionId { get; set; }

    public int ApplictionLinkId { get; set; }

    public int ToEmpId { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual ApplicationLink ApplictionLink { get; set; } = null!;

    public virtual Employee ToEmp { get; set; } = null!;
}
