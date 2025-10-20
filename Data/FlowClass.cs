using WorkFlow.Models;

namespace WorkFlow.Data
{
    public class Label
    {
        public int? From { get; set; }
        public int? to { get; set; }
        public string? fromPort { get; set; }
        public string? toPort { get; set; }
        public string? position { get; set; }
        public string? content { get; set; }
        public string? templateId { get; set; }
        public int? movex { get; set; }
        public int? movey { get; set; }
        public string? text { get; set; }
    }

    public class Link
    {

        public int? From { get; set; }
        public string? fromPort { get; set; }
        public int?  to { get; set; }
        public string? toPort { get; set; }
        public string? templateId { get; set; }
        public double? length { get; set; }
        public int? ApplictionLinkId { get; set; }
        public List<LinkCondation> LinkCondations { get; set; } = new List<LinkCondation>();
        public string? FromName { get; set; }
        public string? ToName { get; set; }

        public string? TotemplateId { get; set; }
        
    }

    public class Node
    {
        public string? templateId { get; set; }
        public double? x { get; set; }
        public double? y { get; set; }
        public int id { get; set; }
        public string? text { get; set; }
        public string? fill { get; set; }
		public int ApplicationId { get; set; }
		public int? Application_Level_Id { get; set; }
        public AppRequirement AppRequirements { get; set; }

	}

    public class FlowData
    {
        public List<Node> nodes { get; set; }

        public List<Label> labels { get; set; }
        public List<Link> links { get; set; }
        public List<double> viewBox { get; set; }
        public List<object> selectedShapes { get; set; }
        //public List<Label> labels { get; set; } = new List<Label>();
        //public List<Link> links { get; set; } = new List<Link>();
        //public List<string> viewBox { get; set; } = new List<string>();
        //public List<string> selectedShapes { get; set; } = new List<string>();
    }
	public class AppRequirement
	{
		public int? ApplicationId { get; set; }
		public int? Application_Level_Id { get; set; }
		public string NodeID { get; set; }
		public string Application_Level_Name { get; set; }
		public string Column_Name_Value { get; set; }
		public string Tools_Id { get; set; }
		public string Is_Required { get; set; }
		public string Shared_Table_Id { get; set; }
		public string DisplaylinkId { get; set; }
		public string fill { get; set; }
		public string FnRun { get; set; }

	}



}
