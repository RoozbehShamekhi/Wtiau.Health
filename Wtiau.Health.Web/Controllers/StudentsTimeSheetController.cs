using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Wtiau.Health.Web.Models.Domian;
using Wtiau.Health.Web.Models.ViewModels;
using Wtiau.Health.Web.Models.Plugins;
using System.IO;

namespace Wtiau.Health.Web.Controllers
{
    public class StudentsTimeSheetController : Controller
    {
        HealthEntities db = new HealthEntities();

        // GET: StudentsTimeSheet
        public ActionResult Index(int id)
        {
            var _TakeTurn = db.Tbl_TurnTimeSheet.Where(a => a.TTS_ID == id).SingleOrDefault().Tbl_TakeTurn;

            var _Student = _TakeTurn.Select(a => a.Tbl_Student).Select(a => new Model_StudentTakeTimeList
            {
                ID = a.Student_ID,
                Student_Code = a.Student_Code,
                Student_National = a.Student_NationalCode,
                Student_Name = a.Student_SIID.HasValue ? a.Tbl_StudentInfo.SI_Name : "نا معلوم",
                Student_Family = a.Student_SIID.HasValue ? a.Tbl_StudentInfo.SI_Family : "نا معلوم",
                per = false,

            }).ToList();

            ViewBag.ID = id;

            return View(_Student);
        }

        public FileResult Excel (int id)
        {
            var _TakeTurn = db.Tbl_TurnTimeSheet.Where(a => a.TTS_ID == id).SingleOrDefault().Tbl_TakeTurn;

            var _Student = _TakeTurn.Select(a => a.Tbl_Student).Select(a => new Model_StudentTakeTimeList
            {
                ID = a.Student_ID,
                Student_Code = a.Student_Code,
                Student_National = a.Student_NationalCode,
                Student_Name = a.Student_SIID.HasValue ? a.Tbl_StudentInfo.SI_Name : "نا معلوم",
                Student_Family = a.Student_SIID.HasValue ? a.Tbl_StudentInfo.SI_Family : "نا معلوم",
                per = false,

            }).ToList();


            string path = Path.Combine(Server.MapPath("~/App_Data/Excel/"), "TurnReportTemplate.xlsx");

            Microsoft_Excel _Excel = new Microsoft_Excel(path);
            _Excel.Open(1);

            int i = 1;

            foreach (var item in _Student)
            {
                _Excel.WriteToCell(i, 0, item.Student_Code);
                _Excel.WriteToCell(i, 1, item.Student_National);
                _Excel.WriteToCell(i, 2, item.Student_Name);
                _Excel.WriteToCell(i, 3, item.Student_Family);

                i++;
            }

            string SaveAsPath = Path.Combine(Server.MapPath("~/App_Data/Excel/"), string.Format("{0}.xlsx",Guid.NewGuid()));

            _Excel.SaveAs(SaveAsPath);

            _Excel.Close();

            string filename = _TakeTurn.FirstOrDefault().Tbl_TurnTimeSheet.TTS_Name;

            return File(SaveAsPath, "*", string.Format("{0}.xlsx", filename));
        }
    }
}