using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _51800882_51800187_QLSinhVien.Controllers
{
    public class HomeController : Controller
    {
        QLSVContext db = new QLSVContext();
        [Authorize]
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MyUser user)
        {
            var us = db.Users.FirstOrDefault(u => u.userName == user.userName && u.password == user.password);
            if (user.userName != null || user.password != null) {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
            }

            if (us == null)
            {       
                return View();
            }

            Session["User"] = us.userName;
            FormsAuthentication.SetAuthCookie(user.userName, false);
            var ticket = new FormsAuthenticationTicket(1, us.userName, DateTime.Now, DateTime.Now.AddDays(1),
                false, "admin");
            var encrypted = FormsAuthentication.Encrypt(ticket);
            var authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
            HttpContext.Response.Cookies.Add(authcookie);

            return Redirect("/");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}