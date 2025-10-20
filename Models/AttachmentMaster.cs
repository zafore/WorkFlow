using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class AttachmentMaster
{
    public int AttachmentId { get; set; }

    public int CrEmpId { get; set; }

    public DateTime CrDate { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<AttachmentDetail> AttachmentDetails { get; set; } = new List<AttachmentDetail>();
}
