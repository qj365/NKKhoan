namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themspvaocv : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CongViec", "SanPham_MaSanPham", "dbo.SanPham");
            DropIndex("dbo.CongViec", new[] { "SanPham_MaSanPham" });
            RenameColumn(table: "dbo.CongViec", name: "SanPham_MaSanPham", newName: "MaSanPham");
            AlterColumn("dbo.CongViec", "MaSanPham", c => c.Int(nullable: false));
            CreateIndex("dbo.CongViec", "MaSanPham");
            AddForeignKey("dbo.CongViec", "MaSanPham", "dbo.SanPham", "MaSanPham", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CongViec", "MaSanPham", "dbo.SanPham");
            DropIndex("dbo.CongViec", new[] { "MaSanPham" });
            AlterColumn("dbo.CongViec", "MaSanPham", c => c.Int());
            RenameColumn(table: "dbo.CongViec", name: "MaSanPham", newName: "SanPham_MaSanPham");
            CreateIndex("dbo.CongViec", "SanPham_MaSanPham");
            AddForeignKey("dbo.CongViec", "SanPham_MaSanPham", "dbo.SanPham", "MaSanPham");
        }
    }
}
