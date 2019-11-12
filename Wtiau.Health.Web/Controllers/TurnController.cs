using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wtiau.Health.Web.Controllers
{
    public class TurnController : Controller
    {
        // GET: Turn
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TurnAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TurnAdd(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult TurnEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TurnEdit(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult TurnDelete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TurnDelete(int id)
        {
            return View();
        }
    }
}