using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class AttachmentsType
{
    public int AttachTypeId { get; set; }

    public string AttachTypeName { get; set; } = null!;

    public bool InUse { get; set; }
}
