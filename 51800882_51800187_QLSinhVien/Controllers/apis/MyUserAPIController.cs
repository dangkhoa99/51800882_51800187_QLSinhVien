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

        // Get by id
        public IHttpActionResult Get(int id)
        {
            MyUser u = dao.GetByID(id);
            if (u == null)
                return NotFound();

            return Ok(u);
        }

        // Login
        //[HttpPost]
        //public IHttpActionResult Login(MyUser model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Not a valid model");

        //    if (dao.CheckUser(model) == null)
        //        return BadRequest("Not Found");

        //    return Ok(dao.CheckUser(model));
        //}

        // Create User
        public IHttpActionResult Post(MyUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            if (!dao.AddUser(model))
                return BadRequest("Exist Username in DB");

            return Ok(model);
        }
        
        // Edit User
        public IHttpActionResult Put(MyUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            if (!dao.EditUser(model))
                return BadRequest("Error");

            return Ok();
        }

        // Delete
        public IHttpActionResult Delete(int id)
        {
            if (!dao.DeleteUser(id))
                return BadRequest("Xóa thất bại");

            return Ok();
        }
    }
}