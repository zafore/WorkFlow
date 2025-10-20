using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using WorkFlow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop.Implementation;
using Newtonsoft.Json;
using WorkFlow.Data;
using WorkFlow.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static NRules.RuleModel.Builders.RuleTransformation;

namespace WorkFlow.Controllers
{     //[ServiceFilter(typeof(DecryptRequestFilter))]
      //[ServiceFilter(typeof(EncryptResponseFilter))]
    [Authorize]
    public class ApplicationsController : Controller
    {
        int UserID = 18;
        private readonly Workflow2Context _context;

        public ApplicationsController(Workflow2Context context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var workflow2Context = await _context.Applications.Include(a => a.Department).Include(a => a.Section).ToListAsync();
            return View(workflow2Context);
        }


        [HttpGet]

        // GET: Applications
        public async Task<IActionResult> GetSeactionByDep(int DepartmentId)
        {
            var workflow2Context = _context.Sections.Where(x => x.DeptId == DepartmentId).ToList();
            return Json(workflow2Context);
        }


        [HttpGet]
        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            //var decryptedAppId = EncryptionHelper.Decrypt(id);
            ViewBag.id = id;
            ViewBag.AppId = id;
            // Convert decryptedAppId back to the original data type if necessary
            int AppId = int.Parse(id);
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }
            ViewBag.Tools_Id = _context.Tools.Where(x => x.ToolInUse == true).ToList();
            ViewBag.Shared_Table_Id = _context.SharedTables.ToList();

            var application = await _context.Applications
                .Include(a => a.Department)
                .Include(a => a.Section)
                .FirstOrDefaultAsync(m => m.ApplicationId == AppId);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }
        [HttpGet]
        public IActionResult ApplicationLevelDetails(string id)
        {
           // var decryptedAppId = EncryptionHelper.Decrypt(id);

            // Convert decryptedAppId back to the original data type if necessary
            int AppId = int.Parse(id);
            var ApplicationLevels = _context.ApplicationLevels.Include(a => a.ApplicationLinkFromTapplicationLevels).Where(x => x.ApplicationId == AppId && x.ApplicationLevelInUse == true).ToList();
            var RFlowData = new FlowData
            {

                nodes = ApplicationLevels.Select(x => new Node
                {
                    ApplicationId = AppId,
                    Application_Level_Id = x.ApplicationLevelId,
                    x = x.X,
                    y = x.Y,
                    id = x.ApplicationLevelId,
                    templateId = x.TemplateId,
                    text = x.ApplicationLevelName,
                    fill = x.Fill,
                    AppRequirements = null

                }).ToList(),
                links = (
          from x in ApplicationLevels
          join b in _context.ApplicationLinks.Where(X => X.InUse == true) on x.ApplicationLevelId equals b.FromTapplicationLevelId

          select new Link
          {
              to = b.ToApplicationLevelId,
              length = b.Length,
              templateId = b.TemplateId,
              From = b.FromTapplicationLevelId,
              FromName = b.FromTapplicationLevel.ApplicationLevelName,
              ToName = b.ToApplicationLevel.ApplicationLevelName,
              ApplictionLinkId= b.ApplictionLinkId,
              TotemplateId=b.ToApplicationLevel.TemplateId

          }).ToList(),


                labels = (
          from x in ApplicationLevels
          join b in _context.ApplicationLables on x.ApplicationLevelId equals b.FromApplicationLevelId
          select new Label
          {
              to = b.ToApplicationLevelId,
              templateId = b.TemplateId,
              From = b.FromApplicationLevelId,
              content = b.Content.ToString(),
              fromPort = b.FromPort,
              movex = (int?)(b.Movex ?? 0),
              movey = (int?)(b.Movey ?? 0),
              position = b.Position.ToString(),
              text = b.Text,
              toPort = b.ToPort

          }).ToList(),


            };
            return Json(RFlowData);
        }



        [HttpGet]
        public IActionResult LoadLinkCondition(int ApplicationLevelId)
        {
            var model = _context.LinkCondations.Where(x => x.ApplictionLink.FromTapplicationLevelId == ApplicationLevelId).ToList();

            return Json(model);
        }


        [HttpGet]
        public IActionResult _LoadForm(int ApplicationLevelId)
        {
            var model = _context.ApplicationRequirements.Where(x => x.ApplicationLevelId == ApplicationLevelId).Select(x => new LoadFormClass
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
                ApplicationProcuderDetails = x.ApplicationProcuderDetails,
                ApplicationRequirementDetail = x.ApplicationRequirementDetails,
                ArchivesMasterId = x.ArchivesMasterId,
                ArchivesMasters = x.ArchivesMaster,
                DisplayLink = x.Displaylink,
                //Employe
                //RequestDetailsAttachs = x.RequestDetailsAttaches.ToList()

            }).ToList();
            return PartialView(model);
        }
        [HttpGet]
        public IActionResult ApplicationLevelRequerment(int ApplicationLevelId)
        {
            var model = _context.ApplicationRequirements.Where(x => x.ApplicationLevelId == ApplicationLevelId).Select(x => new
            {

                x.ApplicationLevelId,
                x.ApplicationRequirementInUse,
                x.DisplayIndex,
                x.DisplaylinkId,
                x.FixId,
                x.UseMin,
                x.IsRequired,
                x.ToolsId,
                x.Tools.ToolName,
                x.Tools.ToolValue,
                x.ColumnNameValue,
                x.FnRun,
                x.InStart,
                x.SharedTableId,
                x.SharedTable,
                x.ApplicationProcuderDetails,
                x.ApplicationRequirementDetails,
                x.ArchivesMasterId,
                x.ArchivesMaster


            }).ToList();
            return Json(model);

        }
        [HttpGet("ActionList")]
        public IActionResult ActionList()
        {
            var model = _context.Actions.Where(x => x.ActionInUse == true).ToList();
            return Ok(model);

        }

        [HttpGet("ManagerTypeList")]
        public IActionResult ManagerTypeList()
        {
            var model = _context.ManagerTypes.Where(x => x.InUse == true).ToList();
            return Ok(model);

        }
        public class SearchPerson
        {
            public string? SearchP { get; set; }
            public string? SearchEmp { get; set; }
        }
        [HttpGet("SearchEmp")]
        public async Task<IActionResult> SearchEmp(string SearchP)
        {
            // استخدام Parameterized Query لمنع SQL Injection
            var result = await _context.FnEmps
                .FromSqlRaw("SELECT * FROM FnSearchEmp({0})", SearchP)
                .ToListAsync();
            return Ok(result);
        }
        [HttpGet]
        // GET: Applications/Create
        public IActionResult Create()
        {
            //_context.Sections.First().SectionNameEng

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentNameEng");
            ViewData["SystemInfoId"] = new SelectList(_context.SystemInfos.Where(x => x.IsActive == true), "SystemInfoId", "SystemInfoName");
            return View();
        }

 
        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // GET: Applications/Create
        public IActionResult Create(Application Application)
        {
            Application Model = new Application();
            Model.ApplicationNameAr = Application.ApplicationNameAr;
            Model.ApplicationNameEng = Application.ApplicationNameEng;
            Model.ApplicationInUse = true;
            Model.CrDate = DateTime.Now;
            Model.CrEmpId = 12;
            Model.DepartmentId = Application.DepartmentId;
            Model.SectionId = (Application.SectionId == null ? Application.SectionId : null);
            Model.SystemInfoId = Application.SystemInfoId;
            _context.Applications.Add(Model);
            _context.SaveChanges();


            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentNameEng");
            ViewData["SystemInfoId"] = new SelectList(_context.SystemInfos.Where(x => x.IsActive == true), "SystemInfoId", "SystemInfoName");

            return Redirect("Index");
        }
        public class ADDAssign
        {
            public int ApplicationLevelId { get; set; }
            public int AssginTypeId { get; set; }
            public int? AssignedToEmpId { get; set; }
            public int? ManagerTypeId { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> AddLevelAssign([FromBody]  ADDAssign ADDAssigns)
        {
            ApplicationLevelAssigned model = new ApplicationLevelAssigned();
            model.ApplicationLevelId = ADDAssigns.ApplicationLevelId;
            model.AssginTypeId = ADDAssigns.AssginTypeId;
            model.AssignedToEmpId = ADDAssigns.AssignedToEmpId;
            model.ManagerTypeId = ADDAssigns.ManagerTypeId; ;
            model.CrDate = DateTime.Now;

            _context.ApplicationLevelAssigneds.Add(model);
            _context.SaveChanges();

            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetLevelAssign(int ApplicationLevelId)
        {
            //var model = _context.FnApplicationLevelAssigneds.Where(x => x.ApplicationLevelId == ApplicationLevelId).ToList();
            //return Json(model);
            var result = _context.FnApplicationLevelAssigneds.FromSqlRaw($"select * from FnApplicationLevelAssigned('{ApplicationLevelId}') ").ToList();
            return Ok(result);

        }

          [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLevelRequirement([FromBody] FlowData flowchartData)
        {
            var ApplicationId = flowchartData.nodes[0].ApplicationId;
            ///ADD Nodes
            foreach (var Node in flowchartData.nodes)
            {

                var CheckLevel = _context.ApplicationLevels.Where(x => x.ApplicationLevelId == Node.Application_Level_Id && x.ApplicationId == Node.ApplicationId);
                if (CheckLevel.Any())
                {
                    ///Levele Exist 

                    ///Update 
                    ///
                    UpdateAppLevel(Node);

                    ///If there Requirement Need 
                    if (Node.AppRequirements != null)
                    {
                       var ApplicationRequirementId = AddAppRequirement(Node.AppRequirements);
                    }

                    //Check Reletion




                }
                else
                {
                    ///Add Level First 
                    var ApplicationLevelId = AddAppLevel(Node);
                    var NodeID = Node.id;

                    ///Update ALL Linke And Node For new AppLevelID
                    UpdateNodeIdForNewLevel(flowchartData, ApplicationLevelId, NodeID);


                    if (Node.AppRequirements != null)
                    {
                        //Add Level
                        Node.AppRequirements.Application_Level_Id = ApplicationLevelId;

                        //Insert Requerment
                        var ApplicationRequirementId = AddAppRequirement(Node.AppRequirements);
                    }




                }

            }


            ///End Add Nodes

            ///NotActive Nodes or Level

            //var AllLevels = _context.ApplicationLevels.Where(x => x.ApplicationId == ApplicationId && x.ApplicationLevelInUse==true).ToList();
            var AllLevels = _context.ApplicationLevels.FromSqlRaw($"select * from Application_Levels where Application_Level_InUse=1 and Application_Id='{ApplicationId}'").ToList();
            foreach (var Level in AllLevels)
            {


                var IsNodeIn = flowchartData.nodes.Where(x => x.Application_Level_Id == Level.ApplicationLevelId).Any();
                if (!IsNodeIn)
                {

                    Level.ApplicationLevelInUse = false;
                    _context.SaveChanges();

                }



            }


            /// end








            ///Add Relation Or link 
            foreach (var Link in flowchartData.links)
            {
                var CheckLinkExist = _context.ApplicationLinks.Where(x => x.FromTapplicationLevelId == Link.From.Value && x.ToApplicationLevelId == Link.to.Value);
                if (!CheckLinkExist.Any())
                {
                    ///New Link
                    ApplicationLink model = new ApplicationLink();
                    model.FromTapplicationLevelId = Link.From.Value;
                    model.ToApplicationLevelId = Link.to.Value;

                    model.InUse = true;
                    model.Length = Link.length.Value;
                    model.TemplateId = Link.templateId;
                    _context.ApplicationLinks.Add(model);
                    _context.SaveChanges();
                }
                else
                {
                    var UpdateLink = CheckLinkExist.First();
                    UpdateLink.InUse = true;
                    UpdateLink.Length = Link.length.Value;
                    UpdateLink.TemplateId = Link.templateId;
                    _context.SaveChanges();


                }







            }


            ///End Add  Relation or Links
            ///

            ////Ckeck Deleted Links 



            var AllLinkFrom = _context.ApplicationLinks.Where(x => x.ToApplicationLevel.ApplicationId == ApplicationId).ToList();

            foreach (var Link in AllLinkFrom)
            {
                var Check = flowchartData.links.Where(x => x.From == Link.FromTapplicationLevelId && x.to == Link.ToApplicationLevelId).Any();
                if (!Check)
                {//If Not exist update It Not Use

                    Link.InUse = false;
                    _context.SaveChanges();

                }


            }

















            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", application.DepartmentId);
            //ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", application.SectionId);
            var ApID =ApplicationId.ToString();
            var LoadNew = ApplicationLevelDetails(ApID);

            return Json(LoadNew);
        }

        public int AddAppRequirement(AppRequirement AppRequirement)
        {


            ApplicationRequirement model = new ApplicationRequirement();
            model.ApplicationId = AppRequirement.ApplicationId ?? 0;
            model.ApplicationLevelId = AppRequirement.Application_Level_Id ?? 0;
            model.ApplicationRequirementInUse = true;
            model.ColumnNameValue = AppRequirement.Column_Name_Value;
            model.ToolsId = int.Parse(AppRequirement.Tools_Id ?? "0");
            if (AppRequirement.Shared_Table_Id !=null)
                model.SharedTableId = int.Parse(AppRequirement.Shared_Table_Id);
            if (AppRequirement.FnRun != null)
                model.FnRun = AppRequirement.FnRun;
            model.IsRequired = bool.Parse( AppRequirement.Is_Required);
            model.CrDate = DateTime.Now;
            model.CrEmpId = UserID;

            _context.ApplicationRequirements.Add(model);
            _context.SaveChanges();

            return model.ApplicationRequirementId;

        }


        public int AddAppLevel(Node Node)
        {
            ApplicationLevel Model = new ApplicationLevel();
            Model.ApplicationId = Node.ApplicationId;
            if (Node.text != null)
                Model.ApplicationLevelName = Node.text;
            else
                Model.ApplicationLevelName = Node.templateId;
            Model.ApplicationLevelInUse = true;
            Model.ApplicationLevelIndex = Node.id;
            Model.Fill = Node.fill;
            Model.X = Node.x;
            Model.Y = Node.y;
            Model.TemplateId = Node.templateId;
            if (Node.templateId == "End")
                Model.CanEndRequest = true;
            else
                Model.CanEndRequest = false;
            _context.ApplicationLevels.Add(Model);
            _context.SaveChanges();





            return Model.ApplicationLevelId;



        }
        public void UpdateAppLevel(Node Node)
        {
            var Model = _context.ApplicationLevels.Where(x => x.ApplicationLevelId == Node.Application_Level_Id).First();
            Model.ApplicationId = Node.ApplicationId;
            if (Node.text != null)
                Model.ApplicationLevelName = Node.text;
            else
                Model.ApplicationLevelName = Node.templateId;
            Model.ApplicationLevelInUse = true;
            Model.ApplicationLevelIndex = Node.id;
            Model.Fill = Node.fill;
            Model.X = Node.x;
            Model.Y = Node.y;
            Model.TemplateId = Node.templateId;
            if (Node.templateId == "End")
                Model.CanEndRequest = true;
            else
                Model.CanEndRequest = false;

            _context.SaveChanges();








        }

        public void UpdateNodeIdForNewLevel(FlowData FlowData, int ApplicationLevelId, int NodeID)
        {

            foreach (var Level in FlowData.nodes.Where(x => x.id == NodeID))
            {

                Level.Application_Level_Id = ApplicationLevelId;


            }
            foreach (var link in FlowData.links.Where(x => x.From == NodeID || x.to == NodeID))
            {
                if (link.From == NodeID)
                    link.From = ApplicationLevelId;

                if (link.to == NodeID)
                    link.to = ApplicationLevelId;


            }

            foreach (var labl in FlowData.labels.Where(x => x.From == NodeID || x.to == NodeID))
            {
                if (labl.From == NodeID)
                    labl.From = ApplicationLevelId;

                if (labl.to == NodeID)
                    labl.to = ApplicationLevelId;


            }

        }

        public class Condation
        {
            public int ActionId { get; set; }
            public int ApplictionLinkId { get; set; }
            public int MustAll { get; set; }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLinkCondation([FromBody] Condation Condations)
        {
            var model = _context.LinkCondations.Where(x => x.ActionId == Condations.ActionId && x.ApplictionLinkId == Condations.ApplictionLinkId);
            if(model.Any())
            {
                return Json(model);

            }
            else
            {
                LinkCondation AddModel = new LinkCondation();
                AddModel.ActionId=Condations.ActionId;
                AddModel.ApplictionLinkId=Condations.ApplictionLinkId;

                if (Condations.MustAll == 0)
                {
                    AddModel.MustAll = false;
                    AddModel.AnyOne = true;
                }
                else
                {
                    AddModel.MustAll = true;
                    AddModel.AnyOne = false;

                }

                _context.LinkCondations.Add(AddModel);
                _context.SaveChanges();

                return Json(AddModel);


            }
            

        }
        [HttpGet]
        public IActionResult LoadLinkConditionList(int ApplicationLevelId)
        {
            var model = _context.LinkCondations.Where(x => x.ApplictionLink.FromTapplicationLevel.ApplicationLevelId == ApplicationLevelId).Select(x => new {

                FromName = x.ApplictionLink.FromTapplicationLevel.ApplicationLevelName,
                ToName = x.ApplictionLink.ToApplicationLevel.ApplicationLevelName,
                x.ApplictionLink.ToApplicationLevelId,
                x.ApplictionLink.ApplictionLinkId,
                x.LinkCondationId,
                x.AnyOne, x.MustAll,
                x.Action.ActionName,
                x.ActionId,

                ActionTake = (x.MustAll == true ? "موافقة الكل" : "موافة فردية")





            }).ToList();
            return Json(model);
        }


        [HttpPost]
        public IActionResult AddAppRequirementDirect(AppRequirement AppRequirement)
        {
            try
            {

                ApplicationRequirement model = new ApplicationRequirement();
                model.ApplicationId = AppRequirement.ApplicationId ?? 0;
                model.ApplicationLevelId = AppRequirement.Application_Level_Id ?? 0;
                model.ApplicationRequirementInUse = true;
                model.ColumnNameValue = AppRequirement.Column_Name_Value;
                model.ToolsId = int.Parse(AppRequirement.Tools_Id ?? "0");
                if (AppRequirement.Shared_Table_Id != null)
                    model.SharedTableId = int.Parse(AppRequirement.Shared_Table_Id);
                if (AppRequirement.FnRun != null)
                    model.FnRun = AppRequirement.FnRun;
                model.CrDate = DateTime.Now;
                model.CrEmpId = UserID;
                model.IsRequired = bool.Parse(AppRequirement.Is_Required);
                _context.ApplicationRequirements.Add(model);
                _context.SaveChanges();

                return Json(new { success = true, Data = model, message = "Form submitted successfully!" });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult DeleteRequement(int   ApplicationRequirementId)
        {
            var model = _context.ApplicationRequirements.Where(x => x.ApplicationRequirementId == ApplicationRequirementId).FirstOrDefault();
            var ApplicationLevelId = model.ApplicationLevelId;
            _context.ApplicationRequirements.Remove(model);
            _context.SaveChanges();
            return Json(new { success = true, Data = model, message = "Form submitted successfully!" });
        }

        /*
        [HttpPost]
        public IActionResult ApplicationNotification(ApplicationNotification ApplicationNotifications)
        {
            _context.ApplicationNotifications.Add(ApplicationNotifications);
            _context.SaveChanges();
            var model = _context.ApplicationNotifications.Where(x => x.ApplictionLink.ApplictionLinkId == ApplicationNotifications.ApplictionLinkId).Select(x => new {
                x.ApplictionLink.FromTapplicationLevel.ApplicationLevelId,
                x.ActionId,
                x.Action.ActionName,
                x.Action.ActionNameArabic,
                x.ApplicationNotificationId,
                x.ApplictionLink.FromTapplicationLevelId,
                FromApplicationLevelName = x.ApplictionLink.FromTapplicationLevel.ApplicationLevelName,
                x.ToEmp.EmployeeName,
                x.ToEmp.EmployeeId,
                ToApplicationLevelName = x.ApplictionLink.ToApplicationLevel.ApplicationLevelName,
                x.ApplictionLink.ApplictionLinkId
            }).ToList();
            return Json(new { success = true, Data = model, message = "Form submitted successfully!" });
        }
        */
        /*
        [HttpGet]
        public IActionResult GetApplicationNotification(int ApplicationLevelId)
        {
            var model = _context.ApplicationNotifications.Where(x => x.ApplictionLink.FromTapplicationLevel.ApplicationLevelId == ApplicationLevelId).Select(x=> new {
                x.ApplictionLink.FromTapplicationLevel.ApplicationLevelId,
                x.ActionId,x.Action.ActionName,x.Action.ActionNameArabic,x.ApplicationNotificationId,x.ApplictionLink.FromTapplicationLevelId,
                FromApplicationLevelName=x.ApplictionLink.FromTapplicationLevel.ApplicationLevelName,
                x.ToEmp.EmployeeName,x.ToEmp.EmployeeId,
                ToApplicationLevelName= x.ApplictionLink.ToApplicationLevel.ApplicationLevelName,
                 x.ApplictionLink.ApplictionLinkId
            }).ToList();
            return Json(new { success = true,Data= model, message = "Form submitted successfully!" });
        }

        [HttpGet]
        public IActionResult DeleteApplicationNotification(int ApplicationNotificationId)
        {
            var model = _context.ApplicationNotifications.Where(x => x.ApplicationNotificationId == ApplicationNotificationId).ToList();
            return Json(new { success = true, message = "Form submitted successfully!" });
        }
        */

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            // Load ViewBag data for dropdowns
            ViewBag.SystemInfoId = new SelectList(_context.SystemInfos, "SystemInfoId", "SystemInfoName", application.SystemInfoId);
            ViewBag.DepartmentId = new SelectList(_context.Departments, "DepartmentId", "DepartmentNameEng", application.DepartmentId);
            ViewBag.SectionId = new SelectList(_context.Sections.Where(s => s.DeptId == application.DepartmentId), "SectionId", "SectionNameEng", application.SectionId);

            return View(application);
        }

        // GET: Applications/EditSimple/5
        public async Task<IActionResult> EditSimple(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            // Load ViewBag data for dropdowns
            ViewBag.SystemInfoId = new SelectList(_context.SystemInfos, "SystemInfoId", "SystemInfoName", application.SystemInfoId);
            ViewBag.DepartmentId = new SelectList(_context.Departments, "DepartmentId", "DepartmentNameEng", application.DepartmentId);
            ViewBag.SectionId = new SelectList(_context.Sections.Where(s => s.DeptId == application.DepartmentId), "SectionId", "SectionNameEng", application.SectionId);

            return View("EditSimple", application);
        }

        // POST: Applications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,ApplicationNameEng,ApplicationNameAr,DepartmentId,SectionId,CrDate,CrEmpId,UpDate,UpEmpId,ApplicationInUse,SystemInfoId")] Application application)
        {
            if (id != application.ApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", application.DepartmentId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", application.SectionId);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Department)
                .Include(a => a.Section)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'Workflow2Context.Applications'  is null.");
            }
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return (_context.Applications?.Any(e => e.ApplicationId == id)).GetValueOrDefault();
        }
    }
}
