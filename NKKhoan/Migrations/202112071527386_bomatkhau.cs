namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bomatkhau : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CongNhan", "MatKhau");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CongNhan", "MatKhau", c => c.String(maxLength: 100));
        }
    }
}
