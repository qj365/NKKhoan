namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CongNhan", "MatKhau", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CongNhan", "MatKhau");
        }
    }
}
