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
    public class KetQuaController : Controller
    {
        string apiUrl = "https://localhost:44328/api/";

        [Authorize(Roles = "admin, user")]
        // GET: KetQua
        public ActionResult Index()
        {
            var db = new QLSVContext();
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH");
            ViewBag.Loai = 0;

            IList<KetQua> kq = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("KetQuaAPI");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<KetQua>>();
                    readTask.Wait();

                    kq = readTask.Result;
                }
            }
            return View(kq);
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult IndexByMaSinhVien(string masv)
        {
            SinhVienDAO svdao = new SinhVienDAO(new QLSVContext());
            ViewBag.Loai = 1;
            ViewBag.TenSV = svdao.GetHoTenByMaSV(masv);
            ViewBag.MaSV = masv;

            IList<KetQua> kq = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("KetQuaAPI?masv=" + masv);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<KetQua>>();
                    readTask.Wait();

                    kq = readTask.Result;
                }
            }
            return View("Index", kq);
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult IndexByMaMonHoc(string mamh)
        {
            var db = new QLSVContext();
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH");

            MonHocDAO mhdao = new MonHocDAO(db);
            ViewBag.Loai = 2;
            ViewBag.TenMH = mhdao.GetTenMHByMaMH(mamh);
            ViewBag.aaa = mamh;

            IList<KetQua> kq = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("KetQuaAPI?mamh=" + mamh);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<KetQua>>();
                    readTask.Wait();

                    kq = readTask.Result;
                }
            }
            return View("Index", kq);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create(string masv)
        {
            var db = new QLSVContext();
            if (masv != null)
            {
                ViewBag.MaSV = new SelectList(db.SinhViens.Where(s => s.MaSV == masv).ToList(), "MaSV", "HoTen");
                var test = db.KetQuas.Where(k => k.MaSV == masv).Select(k => k.MaMH).ToList();
                ViewBag.MaMH = new SelectList(db.MonHocs.Where(m => !test.Contains(m.MaMH)), "MaMH", "TenMH");
                ViewBag.aaa = masv;
            }
            return View();
        }
        
        [Authorize(Roles = "user")]
        public ActionResult CreateFromMH(string mamh)
        {
            var db = new QLSVContext();
            if (mamh != null)
            {
                ViewBag.MaMH = new SelectList(db.MonHocs.Where(m => m.MaMH == mamh), "MaMH", "TenMH");
                var test = db.KetQuas.Where(k => k.MaMH == mamh).Select(k => k.MaSV).ToList();
                ViewBag.MaSV = new SelectList(db.SinhViens.Where(s => !test.Contains(s.MaSV)), "MaSV", "HoTen");
                ViewBag.aaa = mamh;
            }
            return View("Create");
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(KetQua kq)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<KetQua>("KetQuaAPI", kq);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("IndexByMaSinhVien", new { masv = kq.MaSV });
                    }
                }

            }
            var db = new QLSVContext();
            if (kq.MaSV != null)
            {
                ViewBag.MaSV = new SelectList(db.SinhViens.Where(s => s.MaSV == kq.MaSV).ToList(), "MaSV", "HoTen");
                var test = db.KetQuas.Where(k => k.MaSV == kq.MaSV).Select(k => k.MaMH).ToList();
                ViewBag.MaMH = new SelectList(db.MonHocs.Where(m => !test.Contains(m.MaMH)), "MaMH", "TenMH", kq.MaMH);
                ViewBag.aaa = kq.MaSV;
            }

            return View(kq);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult CreateFromMH(KetQua kq)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<KetQua>("KetQuaAPI", kq);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("IndexByMaMonHoc", new { mamh = kq.MaMH });
                    }
                }

            }
            var db = new QLSVContext();
            if (kq.MaMH != null)
            {
                ViewBag.MaMH = new SelectList(db.MonHocs.Where(m => m.MaMH == kq.MaMH), "MaMH", "TenMH");
                var test = db.KetQuas.Where(k => k.MaMH == kq.MaMH).Select(k => k.MaSV).ToList();
                ViewBag.MaSV = new SelectList(db.SinhViens.Where(s => !test.Contains(s.MaSV)), "MaSV", "HoTen", kq.MaSV);
                ViewBag.aaa = kq.MaMH;
            }

            return View("Create", kq);
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult Edit(int STT)
        {
            KetQua kq = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("KetQuaAPI?stt=" + STT);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<KetQua>();
                    readTask.Wait();

                    kq = readTask.Result;
                }
            }

            var db = new QLSVContext();
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "HoTen", kq.MaSV);
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH", kq.MaMH);
            ViewBag.aaa = kq.MaSV;

            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            var gv = ticket.UserData;
            if (gv != "admin")
                ViewBag.aaa = kq.MaMH;

            return View(kq);
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost]
        public ActionResult Edit(KetQua kq)
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            var gv = ticket.UserData;

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<KetQua>("KetQuaAPI", kq);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        
                        if (gv != "admin")
                            return RedirectToAction("IndexByMaMonHoc", new { mamh = kq.MaMH });
                        return RedirectToAction("IndexByMaSinhVien", new { masv = kq.MaSV });
                    }
                }
            }

            var db = new QLSVContext();
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "HoTen", kq.MaSV);
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH", kq.MaMH);
            ViewBag.aaa = kq.MaSV;
            if (gv != "admin")
                ViewBag.aaa = kq.MaMH;

            return View(kq);
        }
    }
}