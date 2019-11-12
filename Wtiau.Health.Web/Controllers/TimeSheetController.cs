using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wtiau.Health.Web.Controllers
{
    public class TimeSheetController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TimeSheetAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TimeSheetAdd(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult TimeSheetDelete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TimeSheetDelete(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult TimeSheetEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TimeSheetEdit(int id)
        {
            return View();
        }
    }
}