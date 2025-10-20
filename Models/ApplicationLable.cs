using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class ApplicationLable
{
    public int ApplicationLableId { get; set; }

    public int FromApplicationLevelId { get; set; }

    public int ToApplicationLevelId { get; set; }

    public string? FromPort { get; set; }

    public string? ToPort { get; set; }

    public double? Position { get; set; }

    public double? Content { get; set; }

    public string? TemplateId { get; set; }

    public int? Movex { get; set; }

    public int? Movey { get; set; }

    public string? Text { get; set; }

    public bool? InUse { get; set; }

    public virtual ApplicationLevel FromApplicationLevel { get; set; } = null!;

    public virtual ApplicationLevel ToApplicationLevel { get; set; } = null!;
}
