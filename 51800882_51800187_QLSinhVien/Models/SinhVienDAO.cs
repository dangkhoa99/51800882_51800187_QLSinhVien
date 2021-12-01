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

        public List<SinhVien> GetAllSinhVienByKhoa(string MaKhoa)
        {
            return db.SinhViens.Where(s => s.MaKhoa == MaKhoa).ToList();
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

        public bool EditSinhVien(SinhVien sv)
        {
            var oldSV = db.SinhViens.FirstOrDefault(s => s.MaSV == sv.MaSV);
            if (oldSV == null)
                return false;

            oldSV.HoTen = sv.HoTen;
            oldSV.GioiTinh = sv.GioiTinh;
            oldSV.NgaySinh = sv.NgaySinh;
            oldSV.MaKhoa = sv.MaKhoa;
            db.SaveChanges();
            return true;
        }

        public bool DeleteSinhVien(string MaSV)
        {
            var sv = db.SinhViens.FirstOrDefault(s => s.MaSV == MaSV);
            if (sv == null)
                return false;

            var kq = db.KetQuas.FirstOrDefault(d => d.MaSV == MaSV);
            if (kq != null)
                return false;

            db.SinhViens.Remove(sv);
            db.SaveChanges();
            return true;
        }

        public SinhVien GetByID(string MaSV)
        {
            return db.SinhViens.FirstOrDefault(s => s.MaSV == MaSV);
        }

        public string GetHoTenByMaSV(string MaSV)
        {
            return db.SinhViens.Where(s => s.MaSV == MaSV).Select(s => s.HoTen).First().ToString();
        }
    }
}