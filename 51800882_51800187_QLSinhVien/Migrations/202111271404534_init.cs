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
                        MaMH = c.String(maxLength: 5),
                        MaSV = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.LanThi)
                .ForeignKey("dbo.MonHocs", t => t.MaMH)
                .ForeignKey("dbo.SinhViens", t => t.MaSV)
                .Index(t => t.MaMH)
                .Index(t => t.MaSV);
            
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
                        MaKhoa = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.MaSV)
                .ForeignKey("dbo.Khoas", t => t.MaKhoa)
                .Index(t => t.MaKhoa);
            
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
            DropForeignKey("dbo.SinhViens", "MaKhoa", "dbo.Khoas");
            DropForeignKey("dbo.KetQuas", "MaSV", "dbo.SinhViens");
            DropForeignKey("dbo.KetQuas", "MaMH", "dbo.MonHocs");
            DropIndex("dbo.SinhViens", new[] { "MaKhoa" });
            DropIndex("dbo.KetQuas", new[] { "MaSV" });
            DropIndex("dbo.KetQuas", new[] { "MaMH" });
            DropTable("dbo.Khoas");
            DropTable("dbo.SinhViens");
            DropTable("dbo.MonHocs");
            DropTable("dbo.KetQuas");
        }
    }
}
