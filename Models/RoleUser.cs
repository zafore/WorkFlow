using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class RoleUser
{
    public int RoleUserId { get; set; }

    public int RoleId { get; set; }

    public int UserId { get; set; }

    public DateTime CrDate { get; set; }

    public int CrUserId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
