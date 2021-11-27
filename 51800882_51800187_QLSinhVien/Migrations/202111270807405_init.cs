namespace _51800882_51800187_QLSinhVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KetQuas",
                c => new
                    {
                        LanThi = c.Int(nullable: false, identity: true),
                        Diem = c.Int(nullable: false),
                        MonHocs_MaMH = c.String(maxLength: 5),
                        SinhViens_MaSV = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.LanThi)
                .ForeignKey("dbo.MonHocs", t => t.MonHocs_MaMH)
                .ForeignKey("dbo.SinhViens", t => t.SinhViens_MaSV)
                .Index(t => t.MonHocs_MaMH)
                .Index(t => t.SinhViens_MaSV);
            
            CreateTable(
                "dbo.MonHocs",
                c => new
                    {
                        MaMH = c.String(nullable: false, maxLength: 5),
                        TenMH = c.String(nullable: false, maxLength: 100),
                        SoTiet = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaMH);
            
            CreateTable(
                "dbo.SinhViens",
                c => new
                    {
                        MaSV = c.String(nullable: false, maxLength: 5),
                        HoTen = c.String(nullable: false, maxLength: 100),
                        NgaySinh = c.DateTime(nullable: false),
                        GioiTinh = c.String(),
                        Khoa_MaKhoa = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.MaSV)
                .ForeignKey("dbo.Khoas", t => t.Khoa_MaKhoa)
                .Index(t => t.Khoa_MaKhoa);
            
            CreateTable(
                "dbo.Khoas",
                c => new
                    {
                        MaKhoa = c.String(nullable: false, maxLength: 5),
                        TenKhoa = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.MaKhoa);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SinhViens", "Khoa_MaKhoa", "dbo.Khoas");
            DropForeignKey("dbo.KetQuas", "SinhViens_MaSV", "dbo.SinhViens");
            DropForeignKey("dbo.KetQuas", "MonHocs_MaMH", "dbo.MonHocs");
            DropIndex("dbo.SinhViens", new[] { "Khoa_MaKhoa" });
            DropIndex("dbo.KetQuas", new[] { "SinhViens_MaSV" });
            DropIndex("dbo.KetQuas", new[] { "MonHocs_MaMH" });
            DropTable("dbo.Khoas");
            DropTable("dbo.SinhViens");
            DropTable("dbo.MonHocs");
            DropTable("dbo.KetQuas");
        }
    }
}
