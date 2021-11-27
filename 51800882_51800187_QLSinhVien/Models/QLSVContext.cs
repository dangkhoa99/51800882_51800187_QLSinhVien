using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class QLSVContext : DbContext
    {
        public QLSVContext() : base("Data Source=DESKTOP-5EA1TTO;Initial Catalog=QLSinhVien_51800882_51800187;User ID=sa;Password=332211")
        {
        }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<KetQua> KetQuas { get; set; }
    }
}