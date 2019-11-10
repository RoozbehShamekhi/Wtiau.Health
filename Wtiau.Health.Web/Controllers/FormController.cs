using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wtiau.Health.Web.Models.Domian;
using Wtiau.Health.Web.Models.ViewModels;

namespace Wtiau.Health.Web.Controllers
{
    public class FormController : Controller
    {
        HealthEntities db = new HealthEntities();
        // GET: Form
        public ActionResult Index()
        {
            var _student = db.Tbl_From.Where(x => x.Form_IsDelete == false).Select(x => new Model_FormList
            {
                ID = x.Form_ID,
                Form_Name = x.Form_Name,
                Form_QuestionCount = x.Form_TotalQuestion,
                Form_CreateDate = x.Form_CreateDate.ToString(),
                Form_StepCount = x.Form_StepCount
            }).ToList();


            return View(_student);
        }

        [HttpGet]
        public ActionResult CreateForm()
        {


            return PartialView();
        }


        [HttpPost]
        public ActionResult CreateForm(Model_FormCreate model)
        {
            Tbl_From _From = new Tbl_From();
            _From.Form_Name = model.Form_Name;
            _From.Form_Display = model.Form_Display;
            _From.Form_Active = true;
            _From.Form_Guid = Guid.NewGuid();
            _From.Form_IsDelete = false;
            _From.Form_Modify = DateTime.Now;
            _From.Form_CreateDate = DateTime.Now;
            _From.Form_StepCount = 0;
            _From.Form_TotalQuestion = 0;
            _From.Tbl_Course = db.Tbl_Course.Where(a => a.Course_Guid.ToString() == model.Course.ToString()).SingleOrDefault();

            db.Tbl_From.Add(_From);

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                TempData["TosterState"] = "success";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "عملیات با موفقیت انجام شده";

                return RedirectToAction("Index", "Form");
            }
            else
            {
                TempData["TosterState"] = "error";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "خطا";

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult AddStep(int id)
        {
            Model_FormStepAdd model = new Model_FormStepAdd();

            model.ID = id;

            return PartialView();
        }


        [HttpPost]
        public ActionResult AddStep(Model_FormStepAdd model)
        {
            Tbl_FormStep _step = new Tbl_FormStep()
            {
                FS_Display = model.Display,
                FS_Name = model.Name,
                FS_Order = model.Order,
                FS_FormID = model.ID,
                FS_Guid = Guid.NewGuid(),
                
            };

            Tbl_From _From = db.Tbl_From.Where(a => a.Form_ID == model.ID).SingleOrDefault();

            _From.Form_StepCount++;


            db.Entry(_From).State = EntityState.Modified;

            db.Tbl_FormStep.Add(_step);

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                TempData["TosterState"] = "success";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "عملیات با موفقیت انجام شده";

                return RedirectToAction("Index", "Form");
            }
            else
            {
                TempData["TosterState"] = "error";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "خطا";

                return RedirectToAction("Index");
            }
        }


        public JsonResult Get_CourseList(string searchTerm)
        {
            var q = db.Tbl_Course.ToList();

            if (searchTerm != null)
            {
                q = db.Tbl_Course.Where(a => a.Course_Years.Contains(searchTerm)).ToList();
            }

            var md = q.Select(a => new { id = a.Course_Guid, text = a.Course_Years });

            return Json(md, JsonRequestBehavior.AllowGet);
        }



    }
}