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

namespace Wtiau.Health.Web.Controllers
{
    public class AccountController : Controller
    {
        HealthEntities db = new HealthEntities();

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Dashboard");

            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Model_Login model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Dashboard");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.State = "Error";

                return View("Login", model);

            }
            var q = db.Tbl_Login.Where(a => a.Login_Email == model.Username || a.Login_Mobile == model.Username).SingleOrDefault();

            if (q == null)
            {
                TempData["TosterState"] = "error";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "کاربر یافت نشد !";

                return View();
            }


            var SaltPassword = model.Password + q.Login_PasswordSalt;
            var SaltPasswordBytes = Encoding.UTF8.GetBytes(SaltPassword);
            var SaltPasswordHush = Convert.ToBase64String(SHA512.Create().ComputeHash(SaltPasswordBytes));


            if (q.Login_PasswordHash == SaltPasswordHush)
            {
                string s = string.Empty;

                s = Rep_UserRole.Get_RoleNameWithID(q.Login_RoleID);

                var Ticket = new FormsAuthenticationTicket(0, model.Username, DateTime.Now, model.RemenberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddDays(1), true, s);
                var EncryptedTicket = FormsAuthentication.Encrypt(Ticket);
                var Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptedTicket)
                {
                    Expires = Ticket.Expiration
                };
                Response.Cookies.Add(Cookie);

                TempData["TosterState"] = "success";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "خوش آمدید";

                return RedirectToAction("index", "Dashboard");
            }
            else
            {
                TempData["TosterState"] = "error";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "پسورد نادرست است !";

                return View();
            }

        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            var Cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                Expires = DateTime.Now.AddDays(-1)
            };

            Response.Cookies.Add(Cookie);
            Session.RemoveAll();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Dashboard");

            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(Model_Register model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Dashboard");
            }

            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            Tbl_Login _Login = new Tbl_Login();

            _Login.Login_Guid = Guid.NewGuid();
            _Login.Login_Email = model.Email;
            _Login.Login_Name = model.Name;
            _Login.Login_Family = model.Family;
            _Login.Login_Mobile = model.Mobile;
            _Login.Login_RoleID = 1;

            var Salt = Guid.NewGuid().ToString("N");
            var SaltPassword = model.Password + Salt;
            var SaltPasswordBytes = Encoding.UTF8.GetBytes(SaltPassword);
            var SaltPasswordHush = Convert.ToBase64String(SHA512.Create().ComputeHash(SaltPasswordBytes));

            _Login.Login_PasswordHash = SaltPasswordHush;
            _Login.Login_PasswordSalt = Salt;

            db.Tbl_Login.Add(_Login);


            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                TempData["TosterState"] = "success";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "ثبت نام با موفقیت انجام شده";

                return RedirectToAction("Login");
            }
            else
            {
                TempData["TosterState"] = "error";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "خطا";

                return View();
            }
        }

        [HttpGet]
        public ActionResult PortalLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Portal");

            }
            return View();
        }

        [HttpPost]
        public ActionResult PortalLogin(Model_PortalLogin model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Portal");
            }

            if (!ModelState.IsValid)
            {
                TempData["TosterState"] = "error";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "خطا";

                return View("PortalLogin", model);

            }
            var q = db.Tbl_Student.Where(a => a.Student_Code == model.StudentCode).SingleOrDefault();

            if (q == null)
            {
                TempData["TosterState"] = "error";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "کاربر یافت نشد !";

                return View();
            }

            if (q.Student_NationalCode == model.NationalCode)
            {
                string s = string.Empty;

                s = "Student";

                var Ticket = new FormsAuthenticationTicket(0, model.StudentCode, DateTime.Now, DateTime.Now.AddDays(1), true, s);
                var EncryptedTicket = FormsAuthentication.Encrypt(Ticket);
                var Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptedTicket)
                {
                    Expires = Ticket.Expiration
                };
                Response.Cookies.Add(Cookie);

                TempData["TosterState"] = "success";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "خوش آمدید";

                return RedirectToAction("index", "Portal");
            }
            else
            {
                TempData["TosterState"] = "error";
                TempData["TosterType"] = TosterType.Maseage;
                TempData["TosterMassage"] = "پسورد نادرست است !";

                return View();
            }

        }

        public ActionResult PortalLogout()
        {
            FormsAuthentication.SignOut();
            var Cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                Expires = DateTime.Now.AddDays(-1)
            };

            Response.Cookies.Add(Cookie);
            Session.RemoveAll();

            return RedirectToAction("PortalLogin", "Account");
        }

    }
}