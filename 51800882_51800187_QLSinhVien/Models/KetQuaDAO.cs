using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class KetQuaDAO
    {
        private QLSVContext db;

        public KetQuaDAO(QLSVContext context)
        {
            this.db = context;
        }

        public List<KetQua> GetAllKetQua()
        {
            return db.KetQuas.ToList();
        }

        public List<KetQua> GetAllKetQuaByMaSV(string masv)
        {
            return db.KetQuas.Where(d => d.MaSV == masv).ToList();
        }

        public List<KetQua> GetAllKetQuaByMaMH(string mamh)
        {
            return db.KetQuas.Where(d => d.MaMH == mamh).ToList();
        }

        public bool AddKetQua(KetQua kq)
        {
            db.KetQuas.Add(kq);
            db.SaveChanges();
            return true;
        }

        public bool EditKetQua(KetQua kq)
        {
            var oldKQ = db.KetQuas.FirstOrDefault(d => d.STT == kq.STT);
            if (oldKQ == null)
                return false;

            oldKQ.MaMH = kq.MaMH;
            oldKQ.MaSV = kq.MaSV;
            oldKQ.Diem = kq.Diem;

            db.SaveChanges();
            return true;
        }

        public bool DeleteKetQua(int STT)
        {
            var kq = db.KetQuas.FirstOrDefault(d => d.STT == STT);
            if (kq == null)
                return false;

            db.KetQuas.Remove(kq);
            db.SaveChanges();
            return true;
        }

        public KetQua GetByID(int STT)
        {
            return db.KetQuas.FirstOrDefault(d => d.STT == STT);
        }
    }
}