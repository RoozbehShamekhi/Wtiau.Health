using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wtiau.Health.Web.Models.Domian;
using Wtiau.Health.Web.Models.ViewModels;
using Wtiau.Health.Web.Models.Repository;
using System.Data.Entity;

namespace Wtiau.Health.Web.Controllers
{
    public class QuestionController : Controller
    {
        HealthEntities db = new HealthEntities();

        public ActionResult Index(int id)
        {
            var _Qusetion = db.Tbl_Question.Select(x => new Model_QusetionList
            {
                ID  = x.Question_ID,
                Name = x.Question_Titel,
                
                Lie = x.Question_Lie,
                step = x.Tbl_FormStep.FS_Display,
                ResponseCount = 0,
            }).ToList();



            return View(_Qusetion);
        }

        [HttpGet]
        public ActionResult QuestionAdd()
        {


            return PartialView();
        }

        [HttpPost]
        public ActionResult QuestionAdd(Modal_QuestionAdd model)
        {
            Tbl_Question _Question = new Tbl_Question()
            {
                Question_Titel = model.Name,
                Question_Guid = Guid.NewGuid(),
                Question_TypeCodeID = Rep_CodeGroup.Get_CodeIDWithGUID(Guid.Parse(model.type)),
                Question_Lie = model.Lie,
                Question_FSID = db.Tbl_FormStep.Where( a=> a.FS_Guid.ToString() == model.step).SingleOrDefault().FS_ID,
            };

            Tbl_From _From = _Question.Tbl_FormStep.Tbl_From;
            _From.Form_TotalQuestion++;


            db.Entry(_From).State = EntityState.Modified;

            db.Tbl_Question.Add(_Question);

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                TempData["TosterState"] = "success";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "عملیات با موفقیت انجام شده";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["TosterState"] = "error";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "خطا";

                return RedirectToAction("Index");
            }
        }

        public JsonResult Get_StepList(string searchTerm)
        {
            var q = db.Tbl_FormStep.ToList();

            if (searchTerm != null)
            {
                q = db.Tbl_FormStep.Where(a => a.FS_Display.Contains(searchTerm)).ToList();
            }

            var md = q.Select(a => new { id = a.FS_Guid.ToString(), text = a.FS_Display });

            return Json(md, JsonRequestBehavior.AllowGet);
        }
    }
}