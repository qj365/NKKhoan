namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SanPham", "TenSanPham", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.SanPham", "SoDangKy", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.SanPham", "HanSuDung", c => c.Int(nullable: false));
            AlterColumn("dbo.SanPham", "QuyCach", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.SanPham", "NgayDangKy", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SanPham", "NgayDangKy", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.SanPham", "QuyCach", c => c.String(maxLength: 50));
            AlterColumn("dbo.SanPham", "HanSuDung", c => c.Int());
            AlterColumn("dbo.SanPham", "SoDangKy", c => c.String(maxLength: 50));
            AlterColumn("dbo.SanPham", "TenSanPham", c => c.String(maxLength: 50));
        }
    }
}
