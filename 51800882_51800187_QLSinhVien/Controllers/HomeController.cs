using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _51800882_51800187_QLSinhVien.Controllers
{
    public class HomeController : Controller
    {
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
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = "https://localhost:44328/api/";
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<MyUser>("MyUserAPI", user);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<MyUser>();
                        readTask.Wait();

                        var us = readTask.Result;

                        Session["user"] = us.userName;
                        FormsAuthentication.SetAuthCookie(user.userName, false);
                        var ticket = new FormsAuthenticationTicket(1, us.userName, DateTime.Now, DateTime.Now.AddDays(1), false, "admin");
                        var encrypted = FormsAuthentication.Encrypt(ticket);
                        var authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        HttpContext.Response.Cookies.Add(authcookie);
                        return Redirect("/");
                    }
                }
            }
            if (user.userName != null || user.password != null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
            }
            return View(user);
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