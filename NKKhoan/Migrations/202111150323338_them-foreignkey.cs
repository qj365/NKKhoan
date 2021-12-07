namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themforeignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CongNhan", "ChucVu_MaChucVu", "dbo.ChucVus");
            DropForeignKey("dbo.CongNhan", "PhongBan_MaPhongBan", "dbo.PhongBans");
            DropIndex("dbo.CongNhan", new[] { "ChucVu_MaChucVu" });
            DropIndex("dbo.CongNhan", new[] { "PhongBan_MaPhongBan" });
            RenameColumn(table: "dbo.CongNhan", name: "ChucVu_MaChucVu", newName: "MaChucVu");
            RenameColumn(table: "dbo.CongNhan", name: "PhongBan_MaPhongBan", newName: "MaPhongBan");
            AlterColumn("dbo.CongNhan", "MaChucVu", c => c.Int(nullable: false));
            AlterColumn("dbo.CongNhan", "MaPhongBan", c => c.Int(nullable: false));
            CreateIndex("dbo.CongNhan", "MaPhongBan");
            CreateIndex("dbo.CongNhan", "MaChucVu");
            AddForeignKey("dbo.CongNhan", "MaChucVu", "dbo.ChucVus", "MaChucVu", cascadeDelete: true);
            AddForeignKey("dbo.CongNhan", "MaPhongBan", "dbo.PhongBans", "MaPhongBan", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CongNhan", "MaPhongBan", "dbo.PhongBans");
            DropForeignKey("dbo.CongNhan", "MaChucVu", "dbo.ChucVus");
            DropIndex("dbo.CongNhan", new[] { "MaChucVu" });
            DropIndex("dbo.CongNhan", new[] { "MaPhongBan" });
            AlterColumn("dbo.CongNhan", "MaPhongBan", c => c.Int());
            AlterColumn("dbo.CongNhan", "MaChucVu", c => c.Int());
            RenameColumn(table: "dbo.CongNhan", name: "MaPhongBan", newName: "PhongBan_MaPhongBan");
            RenameColumn(table: "dbo.CongNhan", name: "MaChucVu", newName: "ChucVu_MaChucVu");
            CreateIndex("dbo.CongNhan", "PhongBan_MaPhongBan");
            CreateIndex("dbo.CongNhan", "ChucVu_MaChucVu");
            AddForeignKey("dbo.CongNhan", "PhongBan_MaPhongBan", "dbo.PhongBans", "MaPhongBan");
            AddForeignKey("dbo.CongNhan", "ChucVu_MaChucVu", "dbo.ChucVus", "MaChucVu");
        }
    }
}
