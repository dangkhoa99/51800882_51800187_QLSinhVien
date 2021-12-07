using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using _51800882_51800187_QLSinhVien.Res;

namespace _51800882_51800187_QLSinhVien.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // Trang chủ
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // Đổi ngôn ngữ khi click vào icon flag
        public ActionResult ChangeLanguage(string lang)
        {
            Session["lang"] = lang;
            //return RedirectToAction(null, new { language = lang });
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        // Trang 404
        public ActionResult NotFound()
        {
            return View();
        }

        // Tạo form login
        public ActionResult Login()
        {
            return View();
        }

        // Login
        // Lưu cookie
        [HttpPost]
        public ActionResult Login(MyUser user)
        {
            if (ModelState.IsValid)
            {
                if (user.userName != null || user.password != null)
                {
                    ViewBag.Error = LangResource.messErrorLogin;
                }

                MyUserDAO dao = new MyUserDAO(new QLSVContext());

                var us = dao.CheckUser(user);
                var data = us.MaGV + "," + us.id;
                if (us != null) {
                    if (us.MaGV == null)
                    {
                        data = "admin," + us.id;
                    }
                    var ticket = new FormsAuthenticationTicket(1, us.userName, DateTime.Now, DateTime.Now.AddDays(1), false, data);
                    var encrypted = FormsAuthentication.Encrypt(ticket);
                    var authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    HttpContext.Response.Cookies.Add(authcookie);

                    return Redirect("/");
                }
            }
            return View(user);
        }

        // Đăng xuất
        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}


//[HttpPost]
//public ActionResult Login(MyUser user)
//{
//    if (ModelState.IsValid)
//    {
//        if (user.userName != null || user.password != null)
//        {
//            ViewBag.Error = LangResource.messErrorLogin;
//        }

//        using (var client = new HttpClient())
//        {
//            string apiUrl = "https://localhost:44328/api/";
//            client.BaseAddress = new Uri(apiUrl);

//            //HTTP POST
//            var postTask = client.PostAsJsonAsync<MyUser>("MyUserAPI", user);
//            postTask.Wait();

//            var result = postTask.Result;

//            if (result.IsSuccessStatusCode)
//            {
//                var readTask = result.Content.ReadAsAsync<MyUser>();
//                readTask.Wait();

//                var us = readTask.Result;
//                if (us.MaGV == null)
//                {
//                    us.MaGV = "admin";
//                }
//                var ticket = new FormsAuthenticationTicket(1, us.userName, DateTime.Now, DateTime.Now.AddDays(1), false, us.MaGV);
//                var encrypted = FormsAuthentication.Encrypt(ticket);
//                var authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
//                HttpContext.Response.Cookies.Add(authcookie);

//                return Redirect("/");
//            }
//        }
//    }
//    return View(user);
//}