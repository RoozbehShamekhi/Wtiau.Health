using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web.Mvc;
using Wtiau.Health.Web.Models.Domian;
using Wtiau.Health.Web.Models.Plugins;
using Wtiau.Health.Web.Models.ViewModels;

namespace Wtiau.Health.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,DataEntry")]
    public class StudentController : Controller
    {
        HealthEntities db = new HealthEntities();

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
                Student_Name = x.Student_SIID.HasValue ? x.Tbl_StudentInfo.SI_Name : "نا معلوم",
                Student_Family = x.Student_SIID.HasValue ? x.Tbl_StudentInfo.SI_Family : "نا معلوم",
                Student_Info = x.Student_SIID.HasValue

            }).ToList();

            return View(_student);
        }

        [HttpPost]
        public ActionResult GetStudents()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            var _student = db.Tbl_Student.Where(x => x.Student_IsDelete == false).Select(x => new Model_StudentList
            {
                ID = x.Student_ID,
                Student_Code = x.Student_Code,
                Student_National = x.Student_NationalCode,
                Student_Form1 = x.Student_Form1,
                Student_Form2 = x.Student_Form1,
                Student_Turn = x.Student_TakeTurn,
                Student_Name = x.Student_SIID.HasValue ? x.Tbl_StudentInfo.SI_Name : "نا معلوم",
                Student_Family = x.Student_SIID.HasValue ? x.Tbl_StudentInfo.SI_Family : "نا معلوم",
                Student_Info = x.Student_SIID.HasValue

            }).ToList();

            int totalRows = _student.Count;

            if (!string.IsNullOrEmpty(searchValue))
            {
                _student = _student.Where(x => x.ID.ToString().ToLower().Contains(searchValue.ToLower()) ||
                                               x.Student_Code.ToLower().Contains(searchValue.ToLower()) ||
                                               x.Student_National.ToLower().Contains(searchValue.ToLower()) ||
                                               x.Student_Form1.ToString().ToLower().Contains(searchValue.ToLower()) ||
                                               x.Student_Form2.ToString().ToLower().Contains(searchValue.ToLower()) ||
                                               x.Student_Turn.ToString().ToLower().Contains(searchValue.ToLower()) ||
                                               (string.IsNullOrEmpty(x.Student_Name) ? false : x.Student_Name.ToLower().Contains(searchValue.ToLower())) ||
                                               (string.IsNullOrEmpty(x.Student_Family) ? false : x.Student_Family.ToLower().Contains(searchValue.ToLower())) ||
                                               x.Student_Info.ToString().ToLower().Contains(searchValue.ToLower())).ToList();
            }

            int totalRowsAfterFiltering = _student.Count;

            _student = _student.OrderBy(sortColumnName + " " + sortDirection).ToList();

            _student = _student.Skip(start).Take(length).ToList();

            return Json(new { data = _student, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalRowsAfterFiltering }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailStudent(int? id)
        {
            if (id != null)
            {
                Model_MessageModal model = new Model_MessageModal();

                var q = db.Tbl_Form.Where(x => x.Form_ID == id).SingleOrDefault();

                if (q != null)
                {
                    model.ID = id.Value;
                    model.Name = q.Form_Name;
                    model.Description = "آیا از حذف پرسش نامه مورد نظر اطمینان دارید ؟";

                    return PartialView(model);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsStudent(Model_MessageModal model)
        {
            if (ModelState.IsValid)
            {

            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditStudent(int? id)
        {
            if (id != null)
            {
                Model_MessageModal model = new Model_MessageModal();

                var q = db.Tbl_Form.Where(x => x.Form_ID == id).SingleOrDefault();

                if (q != null)
                {
                    model.ID = id.Value;
                    model.Name = q.Form_Name;
                    model.Description = "آیا از حذف پرسش نامه مورد نظر اطمینان دارید ؟";

                    return PartialView(model);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(Model_MessageModal model)
        {
            if (ModelState.IsValid)
            {

            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult DeleteStudent(int? id)
        {
            if (id != null)
            {
                Model_MessageModal model = new Model_MessageModal();

                var q = db.Tbl_Form.Where(x => x.Form_ID == id).SingleOrDefault();

                if (q != null)
                {
                    model.ID = id.Value;
                    model.Name = q.Form_Name;
                    model.Description = "آیا از حذف پرسش نامه مورد نظر اطمینان دارید ؟";

                    return PartialView(model);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudent(Model_MessageModal model)
        {
            if (ModelState.IsValid)
            {

            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        #region Data

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

        public FileResult Excel_AllStudents()
        {
            string path = Path.Combine(Server.MapPath("~/Content/Reports/Excel/"), "StudentTemplate.xlsx");

            Microsoft_Excel _Excel = new Microsoft_Excel(path);
            _Excel.Open(1);

            int i = 1;

            foreach (var item in db.Tbl_Student.OrderBy(a => a.Student_Code).ToList())
            {
                _Excel.WriteToCell(i, 0, item.Student_Code);
                _Excel.WriteToCell(i, 1, item.Student_NationalCode);
                _Excel.WriteToCell(i, 2, item.Student_SIID.HasValue ? item.Tbl_StudentInfo.SI_Name : "نا معلوم");
                _Excel.WriteToCell(i, 3, item.Student_SIID.HasValue ? item.Tbl_StudentInfo.SI_Family : "نا معلوم");
                _Excel.WriteToCell(i, 4, item.Student_Info ? "تکمیل شده" : "تکمیل نشده");
                _Excel.WriteToCell(i, 5, item.Student_Form1 ? "تکمیل شده" : "تکمیل نشده");
                _Excel.WriteToCell(i, 6, item.Student_Form2 ? "تکمیل شده" : "تکمیل نشده");
                _Excel.WriteToCell(i, 7, item.Student_TakeTurn ? "تکمیل شده" : "تکمیل نشده");

                i++;
            }

            string SaveAsPath = Path.Combine(Server.MapPath("~/App_Data/Excel/"), string.Format("{0}.xlsx", Guid.NewGuid()));

            _Excel.SaveAs(SaveAsPath);

            _Excel.Close();

            return File(SaveAsPath, "*", string.Format("{0}.xlsx", "تمامی دانشجویان"));
        }

        public FileResult Excel_NotRegisterStudents()
        {
            string path = Path.Combine(Server.MapPath("~/Content/Reports/Excel/"), "StudentTemplate.xlsx");

            Microsoft_Excel _Excel = new Microsoft_Excel(path);
            _Excel.Open(1);

            int i = 1;

            foreach (var item in db.Tbl_Student.Where(a => a.Student_Info == false || a.Student_Form1 == false || a.Student_Form2 == false || a.Student_TakeTurn == false).OrderBy(a => a.Student_Code).ToList())
            {
                _Excel.WriteToCell(i, 0, item.Student_Code);
                _Excel.WriteToCell(i, 1, item.Student_NationalCode);
                _Excel.WriteToCell(i, 2, item.Student_SIID.HasValue ? item.Tbl_StudentInfo.SI_Name : "نا معلوم");
                _Excel.WriteToCell(i, 3, item.Student_SIID.HasValue ? item.Tbl_StudentInfo.SI_Family : "نا معلوم");
                _Excel.WriteToCell(i, 4, item.Student_Info ? "تکمیل شده" : "تکمیل نشده");
                _Excel.WriteToCell(i, 5, item.Student_Form1 ? "تکمیل شده" : "تکمیل نشده");
                _Excel.WriteToCell(i, 6, item.Student_Form2 ? "تکمیل شده" : "تکمیل نشده");
                _Excel.WriteToCell(i, 7, item.Student_TakeTurn ? "تکمیل شده" : "تکمیل نشده");

                i++;
            }

            string SaveAsPath = Path.Combine(Server.MapPath("~/App_Data/Excel/"), string.Format("{0}.xlsx", Guid.NewGuid()));

            _Excel.SaveAs(SaveAsPath);

            _Excel.Close();

            return File(SaveAsPath, "*", string.Format("{0}.xlsx", "دانشجویان ثبت نام نشده"));
        }

        #endregion
    }
}