namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredtime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NKSLK", "GioBatDau", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.NKSLK", "GioKetThuc", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NKSLK", "GioKetThuc", c => c.Time(precision: 7));
            AlterColumn("dbo.NKSLK", "GioBatDau", c => c.Time(precision: 7));
        }
    }
}
