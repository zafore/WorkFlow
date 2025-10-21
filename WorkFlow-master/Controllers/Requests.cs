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
        public ActionResult RequestPostNoCSRF(IFormCollection form)
        {
            try
            {
                // تسجيل مفصل للبيانات المرسلة
                System.Diagnostics.Debug.WriteLine("=== Form Data Debug ===");
                System.Diagnostics.Debug.WriteLine("Form Keys Count: " + form.Keys.Count);
                foreach (var key in form.Keys)
                {
                    System.Diagnostics.Debug.WriteLine($"Key: {key}, Value: {form[key]}");
                }
                
                // الحصول على معلومات المستخدم الحالي
                int currentUserId = GetCurrentUserId();
                if (currentUserId == 0)
                {
                    return Json(new { success = false, message = "يجب تسجيل الدخول أولاً" });
                }

                // الحصول على ApplicationId من النموذج
                int applicationId;
                if (!int.TryParse(form["ApplicationId"], out applicationId))
                {
                    return Json(new { success = false, message = "معرف التطبيق غير صحيح" });
                }

                // الحصول على المستوى الأول للتطبيق
                var firstLevel = db.ApplicationLevels
                    .Where(x => x.ApplicationId == applicationId && x.ApplicationLevelInUse == true && x.TemplateId == "start")
                    .FirstOrDefault();
                
                if (firstLevel == null)
                {
                    return Json(new { success = false, message = "لم يتم العثور على المستوى الأول للتطبيق" });
                }

                int applicationLevelId = firstLevel.ApplicationLevelId;

                // إنشاء طلب جديد
                Request newRequest = new Request();
                newRequest.ApplicationId = applicationId;
                newRequest.IsComplate = false;
                newRequest.IsEnd = false;
                newRequest.RequestDate = DateTime.Now;
                newRequest.RequestStatusId = 2; // حالة جديدة
                newRequest.AssignedFromEmpId = currentUserId;
                
                db.Requests.Add(newRequest);
                db.SaveChanges();

                int requestId = newRequest.RequestId;

                // إنشاء مستوى الطلب
                RequestLevel requestLevel = new RequestLevel();
                requestLevel.RequestId = requestId;
                requestLevel.AssignedDate = DateTime.Now;
                requestLevel.RequestDetailsStatusId = 5; // حالة جديدة
                requestLevel.ApplicationLevelId = applicationLevelId;
                requestLevel.InUse = false;
                
                db.RequestLevels.Add(requestLevel);
                db.SaveChanges();

                // معالجة بيانات النموذج
                foreach (var key in form.Keys)
                {
                    var value = form[key].ToString();
                    
                    // تجاهل الحقول الفارغة أو غير المطلوبة
                    if (string.IsNullOrEmpty(value) || 
                        key == "ApplicationId" || 
                        key == "ApplicationLevelId" || 
                        key == "__RequestVerificationToken")
                        continue;
                    
                    // معالجة البيانات المرسلة من النموذج
                    if (key.Contains(".S_Value") || key.Contains(".S_Date") || key.Contains(".S_Shared_Table_Id_Value") || key.Contains(".Application_Req_Details_Id"))
                    {
                        // استخراج معرف المتطلب من اسم الحقل
                        if (key.Contains("[") && key.Contains("]"))
                        {
                            var requirementIdStr = key.Split('[')[1].Split(']')[0];
                            if (int.TryParse(requirementIdStr, out int reqId))
                            {
                                RequestDetail detail = new RequestDetail();
                                detail.RequestId = requestId;
                                detail.RequestLevelId = requestLevel.RequestLevelId;
                                detail.ApplicationRequirementId = reqId;
                                detail.SValue = value;
                                detail.SDate = DateTime.Now;
                                
                                db.RequestDetails.Add(detail);
                            }
                        }
                    }
                }
                
                db.SaveChanges();
                
                return Json(new { success = true, message = "تم إرسال الطلب بنجاح", requestId = requestId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ أثناء إرسال الطلب: " + ex.Message });
            }
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
