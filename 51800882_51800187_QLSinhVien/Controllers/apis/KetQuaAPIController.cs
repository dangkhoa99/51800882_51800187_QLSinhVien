using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _51800882_51800187_QLSinhVien.Controllers.apis
{
    public class KetQuaAPIController : ApiController
    {
        KetQuaDAO dao = new KetQuaDAO(new QLSVContext());
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Ok(dao.GetAllKetQua());
        }

        // GET Diem By MaSV
        public IHttpActionResult GetKQByMaSV(string masv)
        {
            return Ok(dao.GetAllKetQuaByMaSV(masv));
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
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