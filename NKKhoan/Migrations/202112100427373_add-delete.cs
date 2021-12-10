namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChiTietNhanCongKhoan", "MaNhanCong", "dbo.CongNhan");
            DropForeignKey("dbo.ChiTietNhanCongKhoan", "MaNKSLK", "dbo.NKSLK");
            DropForeignKey("dbo.ChiTietCongViec", "MaNKSLK", "dbo.NKSLK");
            DropForeignKey("dbo.ChiTietCongViec", "MaCongViec", "dbo.CongViec");
            AddForeignKey("dbo.ChiTietNhanCongKhoan", "MaNhanCong", "dbo.CongNhan", "MaNhanCong", cascadeDelete: true);
            AddForeignKey("dbo.ChiTietNhanCongKhoan", "MaNKSLK", "dbo.NKSLK", "MaNKSLK", cascadeDelete: true);
            AddForeignKey("dbo.ChiTietCongViec", "MaNKSLK", "dbo.NKSLK", "MaNKSLK", cascadeDelete: true);
            AddForeignKey("dbo.ChiTietCongViec", "MaCongViec", "dbo.CongViec", "MaCongViec", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChiTietCongViec", "MaCongViec", "dbo.CongViec");
            DropForeignKey("dbo.ChiTietCongViec", "MaNKSLK", "dbo.NKSLK");
            DropForeignKey("dbo.ChiTietNhanCongKhoan", "MaNKSLK", "dbo.NKSLK");
            DropForeignKey("dbo.ChiTietNhanCongKhoan", "MaNhanCong", "dbo.CongNhan");
            AddForeignKey("dbo.ChiTietCongViec", "MaCongViec", "dbo.CongViec", "MaCongViec");
            AddForeignKey("dbo.ChiTietCongViec", "MaNKSLK", "dbo.NKSLK", "MaNKSLK");
            AddForeignKey("dbo.ChiTietNhanCongKhoan", "MaNKSLK", "dbo.NKSLK", "MaNKSLK");
            AddForeignKey("dbo.ChiTietNhanCongKhoan", "MaNhanCong", "dbo.CongNhan", "MaNhanCong");
        }
    }
}
