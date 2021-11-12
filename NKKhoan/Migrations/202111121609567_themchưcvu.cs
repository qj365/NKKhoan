namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themchưcvu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChucVus",
                c => new
                    {
                        MaChucVu = c.Int(nullable: false, identity: true),
                        TenChucVu = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.MaChucVu);
            
            AddColumn("dbo.CongNhan", "ChucVu_MaChucVu", c => c.Int());
            CreateIndex("dbo.CongNhan", "ChucVu_MaChucVu");
            AddForeignKey("dbo.CongNhan", "ChucVu_MaChucVu", "dbo.ChucVus", "MaChucVu");
            DropColumn("dbo.CongNhan", "ChucVu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CongNhan", "ChucVu", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.CongNhan", "ChucVu_MaChucVu", "dbo.ChucVus");
            DropIndex("dbo.CongNhan", new[] { "ChucVu_MaChucVu" });
            DropColumn("dbo.CongNhan", "ChucVu_MaChucVu");
            DropTable("dbo.ChucVus");
        }
    }
}
