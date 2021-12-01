using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace _51800882_51800187_QLSinhVien.Controllers
{
    public class KetQuaController : Controller
    {
        KetQuaDAO dao = new KetQuaDAO(new QLSVContext());
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
                else //web api sent error response 
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact admin.");
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
                else //web api sent error response 
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact admin.");
                }
            }
            return View("Index", kq);
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult IndexByMaMonHoc(string mamh)
        {
            var db = new QLSVContext();
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH", mamh);
            MonHocDAO mhdao = new MonHocDAO(new QLSVContext());
            ViewBag.Loai = 2;
            ViewBag.TenMH = mhdao.GetTenMHByMaMH(mamh);
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
                else //web api sent error response 
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact admin.");
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
                //ViewBag.MaSV = db.SinhViens.Where(s => s.MaSV == masv).Select(s => s.HoTen).FirstOrDefault().ToString();
                ViewBag.MaSV = new SelectList(db.SinhViens.Where(s => s.MaSV == masv).ToList(), "MaSV", "HoTen");
                var test = db.KetQuas.Where(k => k.MaSV == masv).Select(k => k.MaMH).ToList();
                ViewBag.MaMH = new SelectList(db.MonHocs.Where(m => !test.Contains(m.MaMH)), "MaMH", "TenMH");
            }



            //ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "HoTen");
            //ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH");
            return View();
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
                        //return RedirectToAction("Index");
                        return RedirectToAction("IndexByMaSinhVien", new { masv = kq.MaSV });
                    }
                }

            }
            var db = new QLSVContext();
            //ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "HoTen", kq.MaSV);
            //ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH", kq.MaMH);

            if (kq.MaSV != null)
            {
                //ViewBag.MaSV = db.SinhViens.Where(s => s.MaSV == kq.MaSV).Select(s => s.HoTen).FirstOrDefault().ToString();
                ViewBag.MaSV = new SelectList(db.SinhViens.Where(s => s.MaSV == kq.MaSV).ToList(), "MaSV", "HoTen");
                var test = db.KetQuas.Where(k => k.MaSV == kq.MaSV).Select(k => k.MaMH).ToList();
                ViewBag.MaMH = new SelectList(db.MonHocs.Where(m => !test.Contains(m.MaMH)), "MaMH", "TenMH", kq.MaMH);
            }

            return View(kq);
        }

        [Authorize(Roles = "admin")]
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
            return View(kq);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(KetQua kq)
        {
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
                        //return RedirectToAction("Index");
                        return RedirectToAction("IndexByMaSinhVien", new { masv = kq.MaSV });
                    }
                }
            }
            var db = new QLSVContext();
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "HoTen", kq.MaSV);
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH", kq.MaMH);
            return View(kq);
        }
    }
}