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

        // GET: KetQua
        public ActionResult Index()
        {
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

        public ActionResult IndexByMaSinhVien(string masv)
        {
            SinhVienDAO svdao = new SinhVienDAO(new QLSVContext());
            ViewBag.Loai = 1;
            ViewBag.TenSV = svdao.GetHoTenByMaSV(masv);
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

        public ActionResult Create()
        {
            var db = new QLSVContext();
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "HoTen");
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH");
            return View();
        }

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
                        return RedirectToAction("Index");
                    }
                }

            }
            var db = new QLSVContext();
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "HoTen", kq.MaSV);
            ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH", kq.MaMH);

            return View(kq);
        }

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
                        return RedirectToAction("Index");
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