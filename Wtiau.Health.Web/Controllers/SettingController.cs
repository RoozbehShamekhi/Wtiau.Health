using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wtiau.Health.Web.Models.Domian;
using Wtiau.Health.Web.Models.ViewModels;

namespace Wtiau.Health.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SettingController : Controller
    {
        HealthEntities db = new HealthEntities();
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        #region Student



        #endregion

        #region Role

        public ActionResult Role_List()
        {
            var model = db.Tbl_Role.Where(x => x.Role_IsDelete == false).Select(x => new Model_RoleList
            {
                ID = x.Role_ID,
                Display = x.Role_Display,
                Name = x.Role_Name,
                Level = x.Role_Level

            }).ToList();


            return View(model);
        }
        #endregion

        #region User

        public ActionResult User_List()
        {
            var model = db.Tbl_Login.Where(x => x.Login_IsDelete == false).Select(x => new Model_UserList
            {
                ID = x.Login_ID,
                Family = x.Login_Family,
                Name = x.Login_Name,
                Role = x.Tbl_Role.Role_Display,
                Email = x.Login_Email,
                Mobile = x.Login_Mobile

            }).ToList();


            return View(model);
        }
        [HttpGet]
        public ActionResult User_Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult User_Add(Model_UserAdd model)
        {
            return View();
        }

        [HttpGet]
        public ActionResult User_Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (db.Tbl_Login.Any(a => a.Login_ID == id))
            {
                var model = db.Tbl_Login.Where(a => a.Login_ID == id).Select(x => new Model_UserEdit
                {
                    ID = x.Login_ID,
                    Family = x.Login_Family,
                    Name = x.Login_Name,
                    Role = x.Login_RoleID,
                    Email = x.Login_Email,
                    Mobile = x.Login_Mobile,
                    
                }).SingleOrDefault();

                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }




        }

        [HttpPost]
        public ActionResult User_Edit(Model_UserEdit model)
        {
            return View();
        }

        [HttpGet]
        public ActionResult User_Delete(int? id)
        {
            if (id.HasValue)
            {
                Model_MessageModal model = new Model_MessageModal();

                var q = db.Tbl_Turn.Where(x => x.Turn_ID == id).SingleOrDefault();

                if (q != null)
                {
                    model.ID = id.Value;
                    model.Name = q.Turn_Name;
                    model.Description = "آیا از حذف کاربر مورد نظر اطمینان دارید ؟";

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
        public ActionResult User_Delete(int x)
        {
            return View();
        }

        #endregion

    }
}