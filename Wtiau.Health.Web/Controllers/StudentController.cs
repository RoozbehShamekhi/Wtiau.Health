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
    public class StudentController : Controller
    {
        HealthEntities db = new HealthEntities();
        // GET: Student

        [HttpGet]
        public ActionResult Index()
        {

            var _student = db.Tbl_Student.Where(x => x.Student_IsDelete == false).Select(x => new Model_StudentList
            {
                ID = x.Student_ID,
                Student_Code = x.Student_Code,
                Student_National = x.Student_NationalCode,
                Student_Form1 = x.Student_Form1,
                Student_Form2 = x.Student_Form1,
                Student_Turn = x.Student_TakeTurn,
                Student_Name = x.Student_SIID.HasValue ? "نا معلوم" : x.Tbl_StudentInfo.SI_Name,
                Student_Family = x.Student_SIID.HasValue ? "نا معلوم" : x.Tbl_StudentInfo.SI_Family,
                Student_Info = x.Student_SIID.HasValue

            }).ToList();

            return View(_student);
        }


        public ActionResult ImportStudentFromExcel()
        {

            Microsoft_Excel excel = new Microsoft_Excel(@"C:\2.xlsx");



            excel.Open(1);
            int row_count = excel.Get_RowCount();
            string[,] ex = excel.Get_Range(1, 1, row_count, 2);
            excel.Close();

            for (int i = 0; i < row_count; i++)
            {

                Tbl_Student _Student = new Tbl_Student()
                {
                    Student_Code = ex[i, 0],
                    Student_NationalCode = ex[i, 1],
                    Student_Guid = Guid.NewGuid()
                };

                db.Tbl_Student.Add(_Student);

            }


            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                TempData["TosterState"] = "success";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "ثبت نام با موفقیت انجام شده";
                return View();
            }
            else
            {
                TempData["TosterState"] = "error";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "خطا";
                return View();
            }
        }

    }
}