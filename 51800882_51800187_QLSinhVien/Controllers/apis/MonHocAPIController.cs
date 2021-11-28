using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _51800882_51800187_QLSinhVien.Controllers.apis
{
    public class MonHocAPIController : ApiController
    {
        MonHocDAO dao = new MonHocDAO(new QLSVContext());
        // GET All MH
        public IHttpActionResult Get()
        {
            return Ok(dao.GetAllMonHocs());
        }

        // GET MH BY ID
        public IHttpActionResult Get(string id)
        {
            MonHoc mh = dao.GetByID(id);
            if (mh == null)
                return NotFound();

            return Ok(mh);
        }

        // Create
        public IHttpActionResult Post(MonHoc model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            if (!dao.AddMonHoc(model))
                return BadRequest("Mã MH đã tồn tại");

            return Ok();
        }

        // Update
        public IHttpActionResult Put(MonHoc model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            if (!dao.EditMH(model))
                return NotFound();

            return Ok();
        }

        // Delete
        public IHttpActionResult Delete(string id)
        {
            if (!dao.DeleteMH(id))
                return BadRequest("Mã MH không tồn tại");

            return Ok();
        }
    }
}