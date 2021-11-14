namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suaspcv : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChiTietCongViec", "SanPhamApDung", "dbo.SanPham");
            DropIndex("dbo.ChiTietCongViec", new[] { "SanPhamApDung" });
            DropPrimaryKey("dbo.ChiTietCongViec");
            AddColumn("dbo.CongViec", "SanPham_MaSanPham", c => c.Int());
            AddPrimaryKey("dbo.ChiTietCongViec", new[] { "MaNKSLK", "MaCongViec" });
            CreateIndex("dbo.CongViec", "SanPham_MaSanPham");
            AddForeignKey("dbo.CongViec", "SanPham_MaSanPham", "dbo.SanPham", "MaSanPham");
            DropColumn("dbo.ChiTietCongViec", "SanPhamApDung");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChiTietCongViec", "SanPhamApDung", c => c.Int(nullable: false));
            DropForeignKey("dbo.CongViec", "SanPham_MaSanPham", "dbo.SanPham");
            DropIndex("dbo.CongViec", new[] { "SanPham_MaSanPham" });
            DropPrimaryKey("dbo.ChiTietCongViec");
            DropColumn("dbo.CongViec", "SanPham_MaSanPham");
            AddPrimaryKey("dbo.ChiTietCongViec", new[] { "MaNKSLK", "MaCongViec", "SanPhamApDung" });
            CreateIndex("dbo.ChiTietCongViec", "SanPhamApDung");
            AddForeignKey("dbo.ChiTietCongViec", "SanPhamApDung", "dbo.SanPham", "MaSanPham");
        }
    }
}
