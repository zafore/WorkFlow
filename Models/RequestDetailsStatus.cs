using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class RequestDetailsStatus
{
    public int RequestDetailsStatusId { get; set; }

    public string RequestDetailsStatusEng { get; set; } = null!;

    public string RequestDetailsStatusAr { get; set; } = null!;

    public bool Retrun { get; set; }

    public bool Reject { get; set; }

    public bool Complete { get; set; }

    public bool Conversion { get; set; }

    public virtual ICollection<RequestLevel> RequestLevels { get; set; } = new List<RequestLevel>();
}
