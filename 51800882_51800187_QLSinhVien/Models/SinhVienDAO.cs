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

        public List<SinhVien> GetAllSinhVien()
        {
            return db.SinhViens.ToList();
        }

        public List<SinhVien> GetAllSinhVienByKhoa(String MaKhoa)
        {
            var sv = db.SinhViens.Where(s => s.Khoa.MaKhoa == MaKhoa);
            return sv.ToList();
        }

        public bool AddSinhVien(SinhVien sv)
        {
            var oldSV = db.SinhViens.FirstOrDefault(s => s.MaSV == sv.MaSV);
            if (oldSV != null)
                return false;

            db.SinhViens.Add(sv);
            db.SaveChanges();
            return true;
        }

        public SinhVien GetByID(string MaSV)
        {
            return db.SinhViens.FirstOrDefault(s => s.MaSV == MaSV);
        }
    }
}