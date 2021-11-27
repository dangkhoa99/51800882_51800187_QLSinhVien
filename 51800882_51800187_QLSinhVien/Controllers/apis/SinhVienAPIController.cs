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
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public IHttpActionResult Get(string makhoa)
        {
            return Ok(dao.GetAllSinhVienByKhoa(makhoa));
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