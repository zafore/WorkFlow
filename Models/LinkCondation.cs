using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class LinkCondation
{
    public int LinkCondationId { get; set; }

    public int ApplictionLinkId { get; set; }

    public int ActionId { get; set; }

    public bool AnyOne { get; set; }

    public bool MustAll { get; set; }

    public int? ChangeActionId { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual ApplicationLink ApplictionLink { get; set; } = null!;

    public virtual Action? ChangeAction { get; set; }
}
