using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class CheckListDetail
{
    public int CheckListDetailsId { get; set; }

    public int ApplicationRequirementId { get; set; }

    public int RequestId { get; set; }

    public int CheckListValue { get; set; }
}
