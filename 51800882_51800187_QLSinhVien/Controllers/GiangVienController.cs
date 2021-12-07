using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace _51800882_51800187_QLSinhVien.Controllers
{
    public class GiangVienController : Controller
    {
        GiangVienDAO dao = new GiangVienDAO(new QLSVContext());
        string apiUrl = "https://localhost:44328/api/";

        [Authorize(Roles = "admin")]
        // GET: GiangVien
        public ActionResult Index()
        {
            ViewBag.Loai = 0;
            //var db = new QLSVContext();
            //ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa");
            IList<GiangVien> gv = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("GiangVienAPI");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<GiangVien>>();
                    readTask.Wait();

                    gv = readTask.Result;
                }
                else //web api sent error response 
                {

                    ModelState.AddModelError(string.Empty, "Server error. Please contact admin.");
                }
            }
            return View(gv);
        }

        public ActionResult GetMH(string makhoa)
        {
            if (!string.IsNullOrWhiteSpace(makhoa))
            {
                var db = new QLSVContext();
                var c = db.MonHocs.Where(m => m.MaKhoa == makhoa).ToList().Select(u => new SelectListItem { Value=u.MaMH, Text=u.TenMH });

                return Json(c, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var db = new QLSVContext();
            //SelectList khoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa");
            //ViewBag.MaMH = new SelectList(db.MonHocs, "MaMH", "TenMH");
            List<SelectListItem> khoa = db.Khoas.Select(n =>
                                                            new SelectListItem
                                                            {
                                                                Value = n.MaKhoa,
                                                                Text = n.TenKhoa
                                                            }).ToList();

            var tmp = new SelectListItem()
            {
                Value = null,
                Text = "-- Khoa --"
            };

            List<SelectListItem> mh = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " -- Môn học --"
                }
            };
            khoa.Insert(0, tmp);
            ViewBag.MaKhoa = new SelectList(khoa, "Value", "Text");
            ViewBag.MaMH = mh;

            List<SelectListItem> GioiTinh = new List<SelectListItem>() {
                new SelectListItem(){ Text="Nam", Value="Nam" },
                new SelectListItem(){ Text="Nữ", Value="Nữ" }
            };

            ViewBag.GioiTinh = new SelectList(GioiTinh, "Value", "Text");

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(GiangVien gv)
        {
            if (ModelState.IsValid)
            {
                var isExists = dao.GetByID(gv.MaGV);

                if (isExists == null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<GiangVien>("GiangVienAPI", gv);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            // Create Account
                            MyUser user = new MyUser();
                            user.roles= "user";
                            user.MaGV = gv.MaGV;
                            user.userName = "user" + gv.MaGV;
                            user.password = "user" + gv.MaGV;

                            var putTask = client.PutAsJsonAsync<MyUser>("MyUserAPI", user);
                            putTask.Wait();

                            var result1 = putTask.Result;
                            if (result1.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index");
                            }

                            //return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Mã GV đã tồn tại.");
                }
            }
            var db = new QLSVContext();
            List<SelectListItem> khoa = db.Khoas.Select(n =>
                                                            new SelectListItem
                                                            {
                                                                Value = n.MaKhoa,
                                                                Text = n.TenKhoa
                                                            }).ToList();

            var tmp = new SelectListItem()
            {
                Value = null,
                Text = "-- Khoa --"
            };

            List<SelectListItem> mh = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " -- Môn học --"
                }
            };
            khoa.Insert(0, tmp);
            ViewBag.MaKhoa = new SelectList(khoa, "Value", "Text", gv.MaKhoa);
            ViewBag.MaMH = db.MonHocs.Where(m => m.MaKhoa == gv.MaKhoa).ToList().Select(u => new SelectListItem { Value = u.MaMH, Text = u.TenMH });

            List<SelectListItem> GioiTinh = new List<SelectListItem>() {
                new SelectListItem(){ Text="Nam", Value="Nam" },
                new SelectListItem(){ Text="Nữ", Value="Nữ" }
            };

            ViewBag.GioiTinh = new SelectList(GioiTinh, "Value", "Text", gv.GioiTinh);

            return View(gv);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
            GiangVien gv = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("GiangVienAPI?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GiangVien>();
                    readTask.Wait();

                    gv = readTask.Result;
                }
            }

            var db = new QLSVContext();
            List<SelectListItem> khoa = db.Khoas.Select(n =>
                                                            new SelectListItem
                                                            {
                                                                Value = n.MaKhoa,
                                                                Text = n.TenKhoa
                                                            }).ToList();

            var tmp = new SelectListItem()
            {
                Value = null,
                Text = "-- Khoa --"
            };

            List<SelectListItem> mh = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " -- Môn học --"
                }
            };
            khoa.Insert(0, tmp);
            ViewBag.MaKhoa = new SelectList(khoa, "Value", "Text", gv.MaKhoa);
            List<SelectListItem> mhList = db.MonHocs.Where(m => m.MaKhoa == gv.MaKhoa).Select(u => new SelectListItem { Value = u.MaMH, Text = u.TenMH }).ToList();
            ViewBag.MaMH = new SelectList(mhList, "Value", "Text", gv.MaMH);
            List <SelectListItem> GioiTinh = new List<SelectListItem>() {
                new SelectListItem(){ Text="Nam", Value="Nam" },
                new SelectListItem(){ Text="Nữ", Value="Nữ" }
            };

            ViewBag.GioiTinh = new SelectList(GioiTinh, "Value", "Text", gv.GioiTinh);



            return View(gv);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(GiangVien gv)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<GiangVien>("GiangVienAPI", gv);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            var db = new QLSVContext();
            List<SelectListItem> khoa = db.Khoas.Select(n =>
                                                            new SelectListItem
                                                            {
                                                                Value = n.MaKhoa,
                                                                Text = n.TenKhoa
                                                            }).ToList();

            var tmp = new SelectListItem()
            {
                Value = null,
                Text = "-- Khoa --"
            };

            List<SelectListItem> mh = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " -- Môn học --"
                }
            };
            khoa.Insert(0, tmp);
            ViewBag.MaKhoa = new SelectList(khoa, "Value", "Text", gv.MaKhoa);
            List<SelectListItem> mhList = db.MonHocs.Where(m => m.MaKhoa == gv.MaKhoa).Select(u => new SelectListItem { Value = u.MaMH, Text = u.TenMH }).ToList();
            ViewBag.MaMH = new SelectList(mhList, "Value", "Text", gv.MaMH);
            List<SelectListItem> GioiTinh = new List<SelectListItem>() {
                new SelectListItem(){ Text="Nam", Value="Nam" },
                new SelectListItem(){ Text="Nữ", Value="Nữ" }
            };

            ViewBag.GioiTinh = new SelectList(GioiTinh, "Value", "Text", gv.GioiTinh);
            return View(gv);
        }
    }
}