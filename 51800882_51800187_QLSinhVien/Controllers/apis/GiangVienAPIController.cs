using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _51800882_51800187_QLSinhVien.Controllers.apis
{
    public class GiangVienAPIController : ApiController
    {
        GiangVienDAO dao = new GiangVienDAO(new QLSVContext());

        // GET All
        public IHttpActionResult Get()
        {
            return Ok(dao.GetAllGV());
        }

        // Get SV by MaSV
        public IHttpActionResult Get(string id)
        {
            GiangVien gv = dao.GetByID(id);
            if (gv == null)
                return NotFound();

            return Ok(gv);
        }

        // GET SV By MAKHOA
        public IHttpActionResult GetGVByMaKhoa(string makhoa)
        {
            return Ok(dao.GetAllGVByKhoa(makhoa));
        }

        // Create SV
        public IHttpActionResult Post(GiangVien model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            if (!dao.AddGV(model))
                return BadRequest("Mã GV đã tồn tại");

            return Ok();
        }

        // Edit SV
        public IHttpActionResult Put(GiangVien model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            if (!dao.EditGV(model))
                return NotFound();

            return Ok();
        }

        // DELETE SV
        public IHttpActionResult Delete(string id)
        {
            if (!dao.DeleteGV(id))
                return BadRequest("Mã GV không tồn tại");

            return Ok();
        }
    }
}