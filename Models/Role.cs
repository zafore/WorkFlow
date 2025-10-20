using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string RoleNameAr { get; set; } = null!;

    public bool? RoleInUse { get; set; }

    public virtual ICollection<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();
}
