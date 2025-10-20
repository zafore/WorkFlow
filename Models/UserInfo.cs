using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class UserInfo
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string UserIdentity { get; set; } = null!;

    public bool? UserActive { get; set; }

    public DateTime CrDate { get; set; }

    public int CrUserId { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();
}
