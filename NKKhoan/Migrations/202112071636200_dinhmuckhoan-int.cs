namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dinhmuckhoanint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CongViec", "DinhMucKhoan", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CongViec", "DinhMucKhoan", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
