using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class SinhVienDAO
    {
        private QLSVContext db;
        public SinhVienDAO(QLSVContext context)
        {
            this.db = context;
        }

        public List<SinhVien> GetAllSinhVienByKhoa(String MaKhoa)
        {
            var sv = db.SinhViens.Where(s => s.Khoa.MaKhoa == MaKhoa);
            return sv.ToList();
        }
    }
}