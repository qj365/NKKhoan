namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themanh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanPham", "Anh", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SanPham", "Anh");
        }
    }
}
