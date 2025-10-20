using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class AttachmentDetail
{
    public int AttachmentDetailId { get; set; }

    public int AttachmentId { get; set; }

    public string AttachPath { get; set; } = null!;

    public string? Size { get; set; }

    public string? Type { get; set; }

    public DateTime? CrDate { get; set; }

    public int? CrEmpId { get; set; }

    public virtual AttachmentMaster Attachment { get; set; } = null!;
}
