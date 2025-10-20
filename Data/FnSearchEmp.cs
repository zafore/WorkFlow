namespace WorkFlow.Data
{
        //modelBuilder.Entity<FnSearchEmp>().HasKey(x => x.EmployeeID);
    public class FnSearchEmp
    {
        public int EmployeeID { get; set; }
        public string? Employee_Name { get; set; }
        public string? IDNumber { get; set; }
        public string? NT_User { get; set; }
        public int? MilitaryRank_ID { get; set; }
        public string? Military_Rank_Name { get; set; }
        public int? CivilRankID { get; set; }
        public string? CivilRankName { get; set; }
        public string? FullName { get; set; }
        public string? IP { get; set; }
        public string? SwitchIP { get; set; }
        public string? Department { get; set; }
        public string? Section { get; set; }
        public string? Building { get; set; }
        public string? Floor { get; set; }
        public string? Room { get; set; }
        public string? LastRequestID { get; set; }
        public string? MobileNo { get; set; }
        public string? ExtensionNo { get; set; }

    }
}
