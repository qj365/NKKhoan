namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeemployee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CongNhan", "HoTen", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CongNhan", "NgayNamSinh", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.CongNhan", "PhongBan", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CongNhan", "ChucVu", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CongNhan", "QueQuan", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CongNhan", "LuongHopDong", c => c.Int(nullable: false));
            AlterColumn("dbo.CongNhan", "LuongBaoHiem", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CongNhan", "LuongBaoHiem", c => c.Int());
            AlterColumn("dbo.CongNhan", "LuongHopDong", c => c.Int());
            AlterColumn("dbo.CongNhan", "QueQuan", c => c.String(maxLength: 50));
            AlterColumn("dbo.CongNhan", "ChucVu", c => c.String(maxLength: 50));
            AlterColumn("dbo.CongNhan", "PhongBan", c => c.String(maxLength: 50));
            AlterColumn("dbo.CongNhan", "NgayNamSinh", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.CongNhan", "HoTen", c => c.String(maxLength: 50));
        }
    }
}
