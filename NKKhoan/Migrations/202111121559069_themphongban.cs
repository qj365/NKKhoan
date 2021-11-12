namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themphongban : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhongBans",
                c => new
                    {
                        MaPhongBan = c.Int(nullable: false, identity: true),
                        TenPhongBan = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.MaPhongBan);
            
            AddColumn("dbo.CongNhan", "PhongBan_MaPhongBan", c => c.Int());
            CreateIndex("dbo.CongNhan", "PhongBan_MaPhongBan");
            AddForeignKey("dbo.CongNhan", "PhongBan_MaPhongBan", "dbo.PhongBans", "MaPhongBan");
            DropColumn("dbo.CongNhan", "PhongBan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CongNhan", "PhongBan", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.CongNhan", "PhongBan_MaPhongBan", "dbo.PhongBans");
            DropIndex("dbo.CongNhan", new[] { "PhongBan_MaPhongBan" });
            DropColumn("dbo.CongNhan", "PhongBan_MaPhongBan");
            DropTable("dbo.PhongBans");
        }
    }
}
