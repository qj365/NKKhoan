namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletecascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChiTietNhanCongKhoan", "MaNKSLK", "dbo.NKSLK");
            DropForeignKey("dbo.ChiTietCongViec", "MaNKSLK", "dbo.NKSLK");
            AddForeignKey("dbo.ChiTietNhanCongKhoan", "MaNKSLK", "dbo.NKSLK", "MaNKSLK", cascadeDelete: true);
            AddForeignKey("dbo.ChiTietCongViec", "MaNKSLK", "dbo.NKSLK", "MaNKSLK", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChiTietCongViec", "MaNKSLK", "dbo.NKSLK");
            DropForeignKey("dbo.ChiTietNhanCongKhoan", "MaNKSLK", "dbo.NKSLK");
            AddForeignKey("dbo.ChiTietCongViec", "MaNKSLK", "dbo.NKSLK", "MaNKSLK");
            AddForeignKey("dbo.ChiTietNhanCongKhoan", "MaNKSLK", "dbo.NKSLK", "MaNKSLK");
        }
    }
}
