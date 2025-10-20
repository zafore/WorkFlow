
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WorkFlow.Models;

namespace WorkFlow.Data
{
    public class LoadFormClass
    {
        public string ColumnNameValue { get; set; } = null!;
        public string? FnRun { get; set; }
        
        public int Application_Requirement_Id { get; set; }
        public int ApplicationLevelId { get; set; }
        public int? DisplaylinkIdId { get; set; }
        public string? FixId { get; set; }
        public int? ToolsId { get; set; }
        public int? SharedTableId { get; set; }
        public int? ArchivesMasterId { get; set; }
        public int? DisplayIndex { get; set; }
        public int? DisplaylinkId { get; set; }

        public bool? ApplicationRequirementInUse { get; set; }
        public bool? UseMin { get; set; }
        public bool? IsRequired { get; set; }
        public bool? InStart { get; set; }
        
        // Navigation properties
        public virtual Tool? Tools { get; set; }
        public virtual SharedTable? SharedTable { get; set; }
        public virtual ApplicationLink? DisplayLink { get; set; }
        [NotMapped]
        public virtual object? ArchivesMasters { get; set; }
        public virtual ApplicationLevel? ApplicationProcuderDetails { get; set; }
        public virtual ApplicationRequirement? ApplicationRequirementDetail { get; set; }
        public virtual UserInfo? Employe { get; set; }
    }
}
