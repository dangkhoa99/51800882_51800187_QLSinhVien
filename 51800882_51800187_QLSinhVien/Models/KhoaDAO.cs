using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class KhoaDAO
    {
        private QLSVContext db;
        public KhoaDAO(QLSVContext context)
        {
            this.db = context;
        }

        public List<Khoa> GetAllKhoas()
        {
            return db.Khoas.ToList() ;
        }

        public bool AddKhoa(Khoa kh)
        {
            var oldKhoa = db.Khoas.FirstOrDefault(k => k.MaKhoa == kh.MaKhoa);
            if (oldKhoa != null)
                return false;

            db.Khoas.Add(kh);
            db.SaveChanges();
            return true;
        }

        public bool EditKhoa(Khoa kh)
        {
            var oldKhoa = db.Khoas.FirstOrDefault(k => k.MaKhoa == kh.MaKhoa);
            if (oldKhoa == null)
                return false;

            oldKhoa.TenKhoa = kh.TenKhoa;
            db.SaveChanges();
            return true;
        }

        public bool DeleteKhoa(String MaKhoa)
        {
            var kh = db.Khoas.FirstOrDefault(k => k.MaKhoa == MaKhoa);
            if (kh == null)
                return false;

            db.Khoas.Remove(kh);
            db.SaveChanges();
            return true;
        }

        public Khoa GetByID(string MaKhoa)
        {
            return db.Khoas.FirstOrDefault(k => k.MaKhoa == MaKhoa);
        }
    }
}