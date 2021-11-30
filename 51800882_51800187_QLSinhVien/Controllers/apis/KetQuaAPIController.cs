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

        // Get by stt
        public IHttpActionResult Get(int stt)
        {
            KetQua kq = dao.GetByID(stt);
            if (kq == null)
                return NotFound();

            return Ok(kq);
        }

        // GET Diem By MaSV
        public IHttpActionResult GetKQByMaSV(string masv)
        {
            return Ok(dao.GetAllKetQuaByMaSV(masv));
        }

        // GET Diem By MaMH
        public IHttpActionResult GetKQByMaMH(string mamh)
        {
            return Ok(dao.GetAllKetQuaByMaMH(mamh));
        }

        // Create
        public IHttpActionResult Post(KetQua model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            if (!dao.AddKetQua(model))
                return BadRequest("STT đã tồn tại");

            return Ok();
        }

        // Update
        public IHttpActionResult Put(KetQua model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            if (!dao.EditKetQua(model))
                return NotFound();

            return Ok();
        }

        // Delete
        public IHttpActionResult Delete(int id)
        {
            if (!dao.DeleteKetQua(id))
                return BadRequest("STT không tồn tại");

            return Ok();
        }
    }
}