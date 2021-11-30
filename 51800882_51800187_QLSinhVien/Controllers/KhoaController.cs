using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace _51800882_51800187_QLSinhVien.Controllers
{
    public class KhoaController : Controller
    {
        KhoaDAO dao = new KhoaDAO(new QLSVContext());
        string apiUrl = "https://localhost:44328/api/";

        // GET: Khoa
        [Authorize(Roles = "admin, user")]
        public ActionResult Index()
        {
            IList<Khoa> khoas = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("KhoaAPI");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Khoa>>();
                    readTask.Wait();

                    khoas = readTask.Result;
                }
                else //web api sent error response 
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact admin.");
                }
            }
            return View(khoas);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create() {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(Khoa kh)
        {
            if (ModelState.IsValid)
            {
                var isExists = dao.GetByID(kh.MaKhoa);

                if (isExists == null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<Khoa>("KhoaAPI", kh);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                else {
                    ModelState.AddModelError(string.Empty, "Mã Khoa đã tồn tại.");
                }
            }
            return View(kh);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
            Khoa kh = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("KhoaAPI?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Khoa>();
                    readTask.Wait();

                    kh = readTask.Result;
                }
            }

            return View(kh);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Khoa kh)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<Khoa>("KhoaAPI", kh);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(kh);
        }

        //public ActionResult Delete(String id)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(apiUrl);

        //        //HTTP DELETE
        //        var deleteTask = client.DeleteAsync("KhoaAPI/" + id);
        //        deleteTask.Wait();

        //        var result = deleteTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {

        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}