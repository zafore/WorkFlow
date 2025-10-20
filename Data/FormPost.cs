namespace WorkFlow.Data
{
    public class FormPost
    {





        public int RequestID { get; set; }
        public int Request_Level_Id { get; set; }
        public int EmployeId { get; set; }

        public string Application_Requirement_Id { get; set; }
        public string S_Value { get; set; }
        public int? S_Shared_Table_Id_Value { get; set; }
        public string S_Date { get; set; }
        public string NewDrop { get; set; }

        public List<string> Application_Req_Details_Id { get; set; }


        public List<IFormFile> files { get; set; }

    }
}
