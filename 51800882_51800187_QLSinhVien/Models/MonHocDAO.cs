using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class MonHocDAO
    {
        private QLSVContext db;

        public MonHocDAO(QLSVContext context)
        {
            this.db = context;
        }

        public List<MonHoc> GetAllMonHocs()
        {
            return db.MonHocs.ToList();
        }

        public bool AddMonHoc(MonHoc mh)
        {
            var oldMH = db.MonHocs.FirstOrDefault(m => m.MaMH == mh.MaMH);
            if (oldMH != null)
                return false;

            db.MonHocs.Add(mh);
            db.SaveChanges();
            return true;
        }

        public bool EditMH(MonHoc mh)
        {
            var oldMH = db.MonHocs.FirstOrDefault(m => m.MaMH == mh.MaMH);
            if (oldMH == null)
                return false;

            oldMH.TenMH = mh.TenMH;
            oldMH.SoTiet = mh.SoTiet;
            db.SaveChanges();
            return true;
        }

        public bool DeleteMH(string MaMH)
        {
            var mh = db.MonHocs.FirstOrDefault(m => m.MaMH == MaMH);
            if (mh == null)
                return false;

            var kq = db.KetQuas.FirstOrDefault(d => d.MaMH == MaMH);
            if (kq != null)
                return false;

            db.MonHocs.Remove(mh);
            db.SaveChanges();
            return true;
        }

        public MonHoc GetByID(string MaMH)
        {
            return db.MonHocs.FirstOrDefault(m => m.MaMH == MaMH);
        }

        public string GetTenMHByMaMH(string MaMH)
        {
            return db.MonHocs.Where(m => m.MaMH == MaMH).Select(m => m.TenMH).First().ToString();
        }
    }
}