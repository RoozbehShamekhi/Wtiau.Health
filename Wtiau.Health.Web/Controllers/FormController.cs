using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Wtiau.Health.Web.Models.Domian;
using Wtiau.Health.Web.Models.Plugins;
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

            return View();
        }


    }
}