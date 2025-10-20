using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFlow.Data;
using WorkFlow.Models;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WorkFlow.Controllers
{
    [Authorize]
    public class Requests : Controller
    {
        private readonly Workflow2Context db;
     
        public Requests(Workflow2Context context)
        {
            db = context;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        private string GetCurrentUserName()
        {
            return User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";
        }

        private string GetCurrentUserFullName()
        {
            return User.FindFirst("FullName")?.Value ?? "Unknown";
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewRequest()
        {
            return View();
        }
        public IActionResult GetApplication() {

            var model = db.Applications.Include(x => x.Department).ToList();
            var Dep = model.Select(x => new { x.Department.DepartmentId, x.Department.DepartmentNameEng }).Distinct().ToList();
            var ReturnModel = Dep.Select(x => new
            {
                id = x.DepartmentId,
                label = x.DepartmentNameEng,
                options = model.Where(m => m.DepartmentId == x.DepartmentId && m.ApplicationInUse == true && m.SystemInfoId == 1).Select(d => new { value = d.ApplicationId, label = d.ApplicationNameEng }).ToList()


            });

            return Ok(ReturnModel);
        }
        public IActionResult FormEdit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult _LoadNewForm(int ApplicationId)
        {
            var FirstLevel=db.ApplicationLevels.Where(x=>x.ApplicationId == ApplicationId  && x.ApplicationLevelInUse==true && x.TemplateId=="start").FirstOrDefault();
            var model = db.ApplicationRequirements.Where(x => x.ApplicationLevelId == FirstLevel.ApplicationLevelId).Select(x => new LoadFormClass
            {
                Application_Requirement_Id = x.ApplicationRequirementId,
                ApplicationLevelId = x.ApplicationLevelId,
                ApplicationRequirementInUse = x.ApplicationRequirementInUse,
                DisplayIndex = x.DisplayIndex,
                DisplaylinkId = x.DisplaylinkId,
                FixId = x.FixId,
                UseMin = x.UseMin,
                IsRequired = x.IsRequired,
                ToolsId = x.ToolsId,
                Tools = x.Tools,
                ColumnNameValue = x.ColumnNameValue,
                FnRun = x.FnRun,
                InStart = x.InStart,
                SharedTableId = x.SharedTableId,
                SharedTable = x.SharedTable,
                ApplicationProcuderDetails = null,
                ApplicationRequirementDetail = null,
                ArchivesMasterId = x.ArchivesMasterId,
                ArchivesMasters = x.ArchivesMaster,
                DisplayLink = null,
                //Employe
                //RequestDetailsAttachs = x.RequestDetailsAttaches.ToList()

            }).ToList();
            ViewBag.ApplicationId = ApplicationId;
            ViewBag.ApplicationLevelId = FirstLevel.ApplicationLevelId;
            return PartialView(model);
        }



        [HttpPost]
        public ActionResult RequestPOst(List<FormPost> collection, int ApplicationId ,int ApplicationLevelId, int? AssignedFromEmpId, int? AssignedToEmp_Id)
        {
            // الحصول على معلومات المستخدم الحالي
            int currentUserId = GetCurrentUserId();
            string currentUserName = GetCurrentUserName();
            string currentUserFullName = GetCurrentUserFullName();

            if (currentUserId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            // Note: These variables need to be properly defined based on your authentication system
            int EmployeId = currentUserId; // استخدام معرف المستخدم الحالي
            int Application_Id = ApplicationId;
            int? Assigned_To_Emp_Id = AssignedToEmp_Id;
            var modelData = db.Applications.Where(x => x.ApplicationInUse == true && x.ApplicationId == ApplicationId);
            Request model = new Models.Request();
            model.ApplicationId = ApplicationId;
            model.IsComplate = false;
            model.IsEnd = false;
            model.RequestDate = System.DateTime.Now;
            model.RequestStatusId = 2;
            model.AssignedFromEmpId = AssignedFromEmpId;
            model.CrUserId = currentUserId; // استخدام معرف المستخدم الحالي
            db.Requests.Add(model);
            db.SaveChanges();

            int RequestID = model.RequestId;
            int Application_Requirement_Id;
            int applicationLevel;
  


 
            RequestLevel AddRequestLevel = new RequestLevel();
            AddRequestLevel.RequestId = RequestID;
            AddRequestLevel.AssignedDate = System.DateTime.Now;
            AddRequestLevel.RequestDetailsStatusId = 5;
            AddRequestLevel.ApplicationLevelId = ApplicationLevelId;
            AddRequestLevel.InUse = false;
            db.RequestLevels.Add(AddRequestLevel);
            db.SaveChanges();
            // Note: These database operations need to be implemented using proper Entity Framework models
            // The following code uses old database patterns that don't exist in the current EF context
            try
            {
                // TODO: Implement proper workflow level management using Entity Framework
                // var NextLevel = db.Fu_Next_Level(Application_Id, 0).First();
                // RequestLevel AddNextLevel = new RequestLevel();
                // AddNextLevel.RequestId = RequestID;
                // AddNextLevel.AssignedDate = System.DateTime.Now.AddHours(9);
                // AddNextLevel.AssignedToEmpId = Assigned_To_Emp_Id;
                // AddNextLevel.RequestDetailsStatusId = 8;
                // AddNextLevel.ApplicationLevelId = NextLevel.ApplicationLevelId;
                // AddNextLevel.InUse = true;
                // db.RequestLevels.Add(AddNextLevel);
                // db.SaveChanges();
                // var AssignToList = db.Pr_Add_Level_Assign(AddNextLevel.ApplicationLevelId, EmployeId, AddNextLevel.RequestLevelId).ToList();
            }
            catch (Exception ex)
            {
                // Log the error and continue with basic request creation
                // TODO: Add proper logging
            }



            // Note: These database operations need to be implemented using proper Entity Framework models
            // TODO: Implement notification system and proper level management
            // var LevelName = db.ApplicationLevels.Where(x => x.ApplicationLevelId == NextLevel.ApplicationLevelId).FirstOrDefault();
            // var EmpInfo = db.Employees.Where(x => x.EmployeeId == EmployeId).First();
            // var RequesterName = EmpInfo.EmployeeName + " - " + model.RequestId;
            // var body = "New Request waiting for you from " + RequesterName + " Request for " + LevelName.Application.ApplicationNameEng;
            // var subject = "New Request waiting for you from " + RequesterName;
            // foreach (var item in AssignToList)
            // {
            //     AddWorkFlowNotification(subject, body, EmployeId, item.AssignedToEmpId.Value);
            // }

            // body = "Your Request No. " + model.RequestId + " Request for " + LevelName.Application.ApplicationNameEng + " is Pending ";
            // subject = "Your Request No. " + model.RequestId + " Request for " + LevelName.Application.ApplicationNameEng + " is Pending ";
            // AddWorkFlowNotification(subject, body, null, EmployeId);
            int RequestLevelId = AddRequestLevel.RequestLevelId;
            // var Requestmodel = (from e in db.RequestLevels where e.RequestLevelId == RequestLevelId select e).First();
            // int ApplicationLevelIndex = Requestmodel.ApplicationLevel.ApplicationLevelIndex;
            foreach (var x in collection)
            {
                Application_Requirement_Id = int.Parse(x.Application_Requirement_Id);
                int? Application_Req_Details_Id = null;
                int? S_Shared_Table_Id_Value = null;
                int? sharedTableId = null;
                int? NewDrop = null;
                if (x.S_Shared_Table_Id_Value != null)
                {
                    S_Shared_Table_Id_Value = x.S_Shared_Table_Id_Value;
                    // Note: This needs to be implemented using proper Entity Framework models
                    // sharedTableId = db.ApplicationRequirements.Where(m => m.ApplicationRequirementId == Application_Requirement_Id).First().SharedTableId;
                }

                DateTime? S_Date = null;
                if (x.S_Date != null)
                    S_Date = DateTime.Parse(x.S_Date);
                if (x.NewDrop != null)
                    NewDrop = int.Parse(x.NewDrop);

                // Note: This method needs to be implemented using proper Entity Framework models
                // var Request_Details = db.Pr_Add_Request_Details(Application_Requirement_Id, RequestID, NewDrop, sharedTableId, x.S_Shared_Table_Id_Value.ToString(), x.S_Value, S_Date, RequestLevelId).First();

                if (x.Application_Req_Details_Id != null)
                {
                    foreach (var Details in x.Application_Req_Details_Id.Select((value, index) => new { value, index }))
                    {
                        // Note: These database operations need to be implemented using proper Entity Framework models
                        // Check_List_Details CkD = new Check_List_Details();
                        // CkD.Application_Requirement_Id = Application_Requirement_Id;
                        // CkD.Check_List_Value = int.Parse(Details.value);
                        // CkD.Request_ID = RequestID;
                        // db.Check_List_Details.Add(CkD);
                        // db.SaveChanges();
                    }


                }
                //if (x.files != null)
                //{
                //    foreach (var file in x.files)
                //    {
                //        if (file != null)
                //        {
                //            if (file.ContentLength > 0)
                //            {
                //                // Note: File handling needs to be implemented using proper Entity Framework models
                //                // var fileName = Path.GetFileName(file.FileName);
                //                // string time = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();

                //                // FileInfo fileInfo = new FileInfo(file.FileName);
                //                // var FileName = time + fileInfo;

                //                // var path = Path.Combine(Server.MapPath("~/Files/Request"), FileName);
                //                // file.SaveAs(path);
                //                // db.Pr_Add_Request_Details_Attach(Request_Details.Request_Details_Id, RequestID, Application_Requirement_Id, FileName, file.FileName, System.DateTime.Now, EmployeId, RequestLevelId);
                //            }
                //        }
                //    }
                //}

            }









            //    db.Pr_Request_Level_Process(Application_Id, RequestID, 0, applicationLevel, 5, Request_Level_Id, EmployeId);

            return RedirectToAction("My_Request");







        }



        [HttpGet]
        public JsonResult GetSharedTable(int id)
        {
            var shareid = (from e in db.ApplicationRequirements where e.ApplicationRequirementId == id select e).First().SharedTableId;
            //var Sring = $"execute Pr_Shared_Tabel_Data @SharedTableId= '{shareid}'";

			var result = db.SharedTableData.FromSqlRaw($"execute Pr_Shared_Tabel_Data @SharedTableId= '{shareid}'").ToList();
            return Json(result);

        }
        //public JsonResult SearchJson(string Name, string MINISTRYCARDNO, string Employe_NO, int? DEPT_ID, int? JOB_DESCRIPTION_ID, int? Nationality_Id, DateTime? FMINISTRYCARDNO_Expire, DateTime? TMINISTRYCARDNO_Expire, int? MINISTRYCARDTYPE_Id, string UserId, DateTime? FEMPLOYMENT_DATE, DateTime? TEMPLOYMENT_DATE, int? EMPID, int? GENDER_ID, int? COMPID, int? DIVISIONID, string EMPSTATUSID)
        //{

        //    //var model = db.Pr_Search_Emp(Name, MINISTRYCARDNO, Employe_NO, DEPT_ID, JOB_DESCRIPTION_ID, Nationality_Id, FMINISTRYCARDNO_Expire, TMINISTRYCARDNO_Expire, MINISTRYCARDTYPE_Id, UserId, FEMPLOYMENT_DATE, TEMPLOYMENT_DATE, EMPID, GENDER_ID, COMPID, DIVISIONID, EMPSTATUSID).OrderBy(x => x.FIRSTNAME).ToList();
        //    //return Json(model, JsonRequestBehavior.AllowGet);

        //}
    }
}
