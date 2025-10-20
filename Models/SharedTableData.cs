using System;
using System.Collections.Generic;

namespace WorkFlow.Models;

public partial class SharedTableData
{
    public int SharedTableDataId { get; set; }
    public int SharedTableId { get; set; }
    public string DataValue { get; set; } = null!;
    public string DataValueAr { get; set; } = null!;
    public string DataValueEng { get; set; } = null!;
    public bool SharedTableDataActive { get; set; }
    public bool SharedTableDataInUse { get; set; }
    public DateTime CrDate { get; set; }
    public int CrUserID { get; set; }
    public int CrEmpId { get; set; }
    public DateTime? UpDate { get; set; }
    public int? UpEmpId { get; set; }

    // Navigation properties
    public virtual SharedTable SharedTable { get; set; } = null!;
}
