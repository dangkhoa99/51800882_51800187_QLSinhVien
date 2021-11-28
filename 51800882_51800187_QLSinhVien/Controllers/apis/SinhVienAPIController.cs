using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _51800882_51800187_QLSinhVien.Controllers.apis
{
    public class SinhVienAPIController : ApiController
    {
        SinhVienDAO dao = new SinhVienDAO(new QLSVContext());

        // GET All
        public IHttpActionResult Get()
        {
            return Ok(dao.GetAllSinhVien());
        }

        // Get SV by MaSV
        public IHttpActionResult Get(string id)
        {
            SinhVien sv = dao.GetByID(id);
            if (sv == null)
                return NotFound();

            return Ok(sv);
        }

        // GET SV By MAKHOA
        public IHttpActionResult GetSVByMaKhoa(string makhoa)
        {
            return Ok(dao.GetAllSinhVienByKhoa(makhoa));
        }

        // Create SV
        public IHttpActionResult Post(SinhVien model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            if (!dao.AddSinhVien(model))
                return BadRequest("Mã SV đã tồn tại");

            return Ok();
        }

        // Edit SV
        public IHttpActionResult Put(SinhVien model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            if (!dao.EditSinhVien(model))
                return NotFound();

            return Ok();
        }

        // DELETE SV
        public IHttpActionResult Delete(string id)
        {
            if (!dao.DeleteSinhVien(id))
                return BadRequest("Mã SV không tồn tại");

            return Ok();
        }
    }
}