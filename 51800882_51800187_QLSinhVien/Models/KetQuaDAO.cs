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
            return db.KetQuas.Where(s => s.MaSV == masv).ToList();
        }
        
    }
}