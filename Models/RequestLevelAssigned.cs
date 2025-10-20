using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class RequestLevelAssigned
{
    public int RequestLevelAssignedId { get; set; }

    public int AssignedToUserId { get; set; }

    public int RequestLevelId { get; set; }

    public int ApplicationLevelId { get; set; }

    public virtual RequestLevel RequestLevel { get; set; } = null!;
}
