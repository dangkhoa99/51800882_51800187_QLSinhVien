using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using _51800882_51800187_QLSinhVien.Res;

namespace _51800882_51800187_QLSinhVien.Controllers
{
    public class MonHocController : Controller
    {
        // GET: MonHoc
        MonHocDAO dao = new MonHocDAO(new QLSVContext());
        string apiUrl = "https://localhost:44328/api/";

        // Danh sách môn học
        [Authorize(Roles = "admin, user")]
        public ActionResult Index()
        {
            IList<MonHoc> mh = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("MonHocAPI");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MonHoc>>();
                    readTask.Wait();

                    mh = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact admin.");
                }
            }
            return View(mh);
        }

        // Danh sách môn học của một giảng viên dạy
        [Authorize(Roles = "user")]
        public ActionResult IndexByMaGV(string magv)
        {
            IList<MonHoc> mh = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("MonHocAPI?magv=" + magv);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MonHoc>>();
                    readTask.Wait();

                    mh = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact admin.");
                }
            }
            return View("Index", mh);
        }

        // Tạo form Create MonHoc
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var db = new QLSVContext();
            ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa");
            return View();
        }

        // Create MonHoc
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(MonHoc mh)
        {
            if (ModelState.IsValid)
            {
                var isExists = dao.GetByID(mh.MaMH);

                if (isExists == null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<MonHoc>("MonHocAPI", mh);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, LangResource.messExistsSubjectID);
                }
            }
            var db = new QLSVContext();
            ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", mh.MaKhoa);
            return View(mh);
        }

        // Tạo form Edit MonHoc
        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
            MonHoc mh = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("MonHocAPI?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MonHoc>();
                    readTask.Wait();

                    mh = readTask.Result;
                }
            }
            var db = new QLSVContext();
            ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", mh.MaKhoa);

            return View(mh);
        }

        // Edit MonHoc
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(MonHoc mh)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<MonHoc>("MonHocAPI", mh);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            var db = new QLSVContext();
            ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", mh.MaKhoa);
            return View(mh);
        }
    }
}