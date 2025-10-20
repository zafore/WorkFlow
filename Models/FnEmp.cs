using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class FnEmp
{
    public int EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public string? Idnumber { get; set; }

    public string? NtUser { get; set; }

    public int? MilitaryRankId { get; set; }

    public string? MilitaryRankName { get; set; }

    public int? CivilRankId { get; set; }

    public string? CivilRankName { get; set; }

    public string? FullName { get; set; }

    public string? Ip { get; set; }

    public string? SwitchIp { get; set; }

    public string? Department { get; set; }

    public string? Section { get; set; }

    public string? Building { get; set; }

    public string? Floor { get; set; }

    public string? Room { get; set; }

    public string? LastRequestId { get; set; }

    public string? MobileNo { get; set; }

    public string? ExtensionNo { get; set; }
}
