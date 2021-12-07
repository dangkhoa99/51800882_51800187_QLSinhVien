using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class QLSVContext : DbContext
    {
        public QLSVContext() : base("Data Source=DESKTOP-MKG1SEQ\\SQLEXPRESS;Initial Catalog=QLSinhVien_51800882_51800187;User ID=sa;Password=1234")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<KetQua> KetQuas { get; set; }
        public DbSet<MyUser> Users { get; set; }
        public DbSet<GiangVien> GiangViens { get; set; }
    }
}