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
        public ActionResult Index(string masv)
        {
            IList<SinhVien> sv = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("KetQuaAPI?masv=" + masv);
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
    }
}