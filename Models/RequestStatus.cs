using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class RequestStatus
{
    public int RequestStatusId { get; set; }

    public string RequestStatusEng { get; set; } = null!;

    public string RequestStatusAr { get; set; } = null!;

    public bool InStart { get; set; }

    public bool InEnd { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
