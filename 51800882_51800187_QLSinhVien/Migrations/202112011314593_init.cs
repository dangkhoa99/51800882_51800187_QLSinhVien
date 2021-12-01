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
                        STT = c.Int(nullable: false, identity: true),
                        Diem = c.Int(nullable: false),
                        MaMH = c.String(nullable: false, maxLength: 5),
                        MaSV = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.STT)
                .ForeignKey("dbo.MonHocs", t => t.MaMH, cascadeDelete: true)
                .ForeignKey("dbo.SinhViens", t => t.MaSV, cascadeDelete: true)
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
                        GioiTinh = c.String(nullable: false),
                        MaKhoa = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.MaSV)
                .ForeignKey("dbo.Khoas", t => t.MaKhoa, cascadeDelete: true)
                .Index(t => t.MaKhoa);
            
            CreateTable(
                "dbo.Khoas",
                c => new
                    {
                        MaKhoa = c.String(nullable: false, maxLength: 5),
                        TenKhoa = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.MaKhoa);
            
            CreateTable(
                "dbo.MyUsers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userName = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                        roles = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SinhViens", "MaKhoa", "dbo.Khoas");
            DropForeignKey("dbo.KetQuas", "MaSV", "dbo.SinhViens");
            DropForeignKey("dbo.KetQuas", "MaMH", "dbo.MonHocs");
            DropIndex("dbo.SinhViens", new[] { "MaKhoa" });
            DropIndex("dbo.KetQuas", new[] { "MaSV" });
            DropIndex("dbo.KetQuas", new[] { "MaMH" });
            DropTable("dbo.MyUsers");
            DropTable("dbo.Khoas");
            DropTable("dbo.SinhViens");
            DropTable("dbo.MonHocs");
            DropTable("dbo.KetQuas");
        }
    }
}
