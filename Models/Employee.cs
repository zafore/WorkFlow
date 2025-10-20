using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public string? Idnumber { get; set; }

    public string? NtUser { get; set; }

    public int? MilitaryRankId { get; set; }

    public string? MilitaryRankName { get; set; }

    public int? CivilRankId { get; set; }

    public string? CivilRankName { get; set; }

    public virtual ICollection<ApplicationNotification> ApplicationNotifications { get; set; } = new List<ApplicationNotification>();
}
