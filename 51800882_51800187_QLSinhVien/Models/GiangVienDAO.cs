using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class GiangVienDAO
    {
        private QLSVContext db;

        public GiangVienDAO(QLSVContext context)
        {
            this.db = context;
        }

        public List<GiangVien> GetAllGV()
        {
            return db.GiangViens.ToList();
        }

        public List<GiangVien> GetAllGVByKhoa(string MaKhoa)
        {
            return db.GiangViens.Where(s => s.MaKhoa == MaKhoa).ToList();
        }

        public bool AddGV(GiangVien gv)
        {
            var oldGV = db.GiangViens.FirstOrDefault(s => s.MaGV == gv.MaGV);
            if (oldGV != null)
                return false;

            db.GiangViens.Add(gv);
            db.SaveChanges();
            return true;
        }

        public bool EditGV(GiangVien gv)
        {
            var oldGV = db.GiangViens.FirstOrDefault(s => s.MaGV == gv.MaGV);
            if (oldGV == null)
                return false;

            oldGV.HoTen = gv.HoTen;
            oldGV.GioiTinh = gv.GioiTinh;
            oldGV.NgaySinh = gv.NgaySinh;
            oldGV.MaKhoa = gv.MaKhoa;
            oldGV.MaMH = gv.MaMH;
            db.SaveChanges();
            return true;
        }

        public bool DeleteGV(string MaGV)
        {
            var gv = db.GiangViens.FirstOrDefault(s => s.MaGV == MaGV);
            if (gv == null)
                return false;

            var account = db.Users.FirstOrDefault(u => u.MaGV == MaGV);
            if (account != null)
                return false;

            db.GiangViens.Remove(gv);
            db.SaveChanges();
            return true;
        }

        public GiangVien GetByID(string MaGV)
        {
            return db.GiangViens.FirstOrDefault(s => s.MaGV == MaGV);
        }

        public string GetHoTenByMaGV(string MaGV)
        {
            return db.GiangViens.Where(s => s.MaGV == MaGV).Select(s => s.HoTen).First().ToString();
        }
    }
}