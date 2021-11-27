using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _51800882_51800187_QLSinhVien.Controllers.apis
{
    public class KhoaAPIController : ApiController
    {
        KhoaDAO dao = new KhoaDAO(new QLSVContext());

        // Read
        public IHttpActionResult Get()
        {
            return Ok(dao.GetAllKhoas());
        }

        // Get by id
        public IHttpActionResult Get(string id)
        {
            Khoa kh = dao.GetByID(id);
            if (kh == null)
                return NotFound();

            return Ok(kh);
        }

        // Create
        public IHttpActionResult Post(Khoa model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            if (!dao.AddKhoa(model))
                return BadRequest("Mã Khoa đã tồn tại");

            return Ok();
        }

        // Update
        public IHttpActionResult Put(Khoa model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            if (!dao.EditKhoa(model))
                return NotFound();

            return Ok();
        }

        // Delete
        public IHttpActionResult Delete(String id)
        {
            if (!dao.DeleteKhoa(id))
                return BadRequest("Mã Khoa không tồn tại");

            return Ok();
        }
    }
}