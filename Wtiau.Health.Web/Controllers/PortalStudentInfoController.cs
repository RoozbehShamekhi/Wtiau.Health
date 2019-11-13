using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Wtiau.Health.Web.Models.Domian;
using Wtiau.Health.Web.Models.ViewModels;

namespace Wtiau.Health.Web.Controllers
{
    public class PortalStudentInfoController : Controller
    {
        HealthEntities db = new HealthEntities();

        // GET: PortalStudentInfo
        public ActionResult Index()
        {
            var q = db.Tbl_Student.Where(a => a.Student_Code == User.Identity.Name).SingleOrDefault();
            if (q != null)
            {
                if (!q.Student_Info)
                {

                }
                else
                {

                }
            }
            else
            {

            }

            return View();
        }
    }
}