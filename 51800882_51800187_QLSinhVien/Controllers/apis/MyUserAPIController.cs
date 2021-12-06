using _51800882_51800187_QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _51800882_51800187_QLSinhVien.Controllers.apis
{
    public class MyUserAPIController : ApiController
    {
        MyUserDAO dao = new MyUserDAO(new QLSVContext());

        // GET all
        public IHttpActionResult Get()
        {
            return Ok(dao.GetAllUser());
        }

        // Login
        public IHttpActionResult Post(MyUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            if (dao.CheckUser(model) == null)
                return BadRequest("Not Found");

            return Ok(dao.CheckUser(model));
        }

        public IHttpActionResult Put(MyUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            if (!dao.AddUser(model))
                return BadRequest("Error");

            return Ok();
        }
    }
}