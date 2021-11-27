using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace _51800882_51800187_QLSinhVien.Controllers.apis
{
    public class SinhVienController : Controller
    {
        SinhVienDAO dao = new SinhVienDAO(new QLSVContext());
        string apiUrl = "https://localhost:44328/api/";

        // GET: SinhVien
        public ActionResult Index()
        {
            ViewBag.Loai = 0;
            IList<SinhVien> sv = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("SinhVienAPI");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SinhVien>>();
                    readTask.Wait();

                    sv = readTask.Result;
                }
                else //web api sent error response 
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact admin.");
                }
            }
            return View(sv);
        }

        public ActionResult IndexByMaKhoa(string makhoa)
        {
            ViewBag.Loai = 1;
            IList<SinhVien> sv = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("SinhVienAPI?makhoa=" + makhoa);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SinhVien>>();
                    readTask.Wait();

                    sv = readTask.Result;
                }
                else //web api sent error response 
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact admin.");
                }
            }
            return View("Index", sv);
        }

        public ActionResult Create()
        {
            var db = new QLSVContext();
            ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SinhVien sv)
        {
            if (ModelState.IsValid)
            {
                var isExists = dao.GetByID(sv.MaSV);

                if (isExists == null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<SinhVien>("SinhVienAPI", sv);
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
                    ModelState.AddModelError(string.Empty, "Mã SV đã tồn tại.");
                }
            }
            var db = new QLSVContext();
            ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", sv.MaKhoa);

            return View(sv);
        }
    }
}