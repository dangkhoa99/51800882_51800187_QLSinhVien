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
    public class MyUserController : Controller
    {
        // GET: MyUser
        MyUserDAO dao = new MyUserDAO(new QLSVContext());
        string apiUrl = "https://localhost:44328/api/";

        // Danh sách User
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            IList<MyUser> users = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("MyUserAPI");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MyUser>>();
                    readTask.Wait();

                    users = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact admin.");
                }
            }
            return View(users);
        }

        // Tạo form Create User
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var db = new QLSVContext();
            var test = db.Users.Select(k => k.MaGV).ToList();
            ViewBag.MaGV = new SelectList(db.GiangViens.Where(m => !test.Contains(m.MaGV)), "MaGV", "HoTen");
            return View();
        }

        // Tạo Users
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(MyUser u)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<MyUser>("MyUserAPI", u);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            var db = new QLSVContext();
            var test = db.Users.Select(k => k.MaGV).ToList();
            ViewBag.MaGV = new SelectList(db.GiangViens.Where(m => !test.Contains(m.MaGV)), "MaGV", "HoTen", u.MaGV);
            return View(u);
        }

        // tạo form Edit user
        [Authorize(Roles = "admin, user")]
        public ActionResult Edit(int id)
        {
            MyUser u = null;
            ViewBag.id = id;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("MyUserAPI?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MyUser>();
                    readTask.Wait();

                    u = readTask.Result;
                }
            }
            var db = new QLSVContext();
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "HoTen", u.MaGV);

            return View(u);
        }

        // Edit user
        [Authorize(Roles = "admin, user")]
        [HttpPost]
        public ActionResult Edit(MyUser u)
        {
            ViewBag.id = u.id;
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            var data = ticket.UserData;
            string gv = data.Split(",".ToCharArray())[0];
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<MyUser>("MyUserAPI", u);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        if (gv != "admin")
                            return RedirectToAction("Detail", new { id = u.id });
                        return RedirectToAction("Index");
                    }
                }
            }
            var db = new QLSVContext();
            var test = db.Users.Select(k => k.MaGV).ToList();
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "HoTen", u.MaGV);
            return View(u);
        }

        // Chi tiết 1 User
        [Authorize(Roles = "user")]
        public ActionResult Detail(int id)
        {
            MyUser u = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("MyUserAPI?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MyUser>();
                    readTask.Wait();

                    u = readTask.Result;
                }
            }
            return View(u);
        }
    }
}