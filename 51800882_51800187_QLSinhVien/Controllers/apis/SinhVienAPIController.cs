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
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Ok(dao.GetAllSinhVien());
        }

        // GET api/<controller>/5
        public IHttpActionResult GetSVByMaKhoa(string makhoa)
        {
            return Ok(dao.GetAllSinhVienByKhoa(makhoa));
        }

        // POST api/<controller>
        public IHttpActionResult Post(SinhVien model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            if (!dao.AddSinhVien(model))
                return BadRequest("Mã SV đã tồn tại");

            return Ok();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}