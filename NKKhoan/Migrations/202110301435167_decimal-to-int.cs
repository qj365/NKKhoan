namespace NKKhoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class decimaltoint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CongNhan", "LuongHopDong", c => c.Int());
            AlterColumn("dbo.CongNhan", "LuongBaoHiem", c => c.Int());
            AlterColumn("dbo.ChiTietCongViec", "SanLuongThucTe", c => c.Int());
            AlterColumn("dbo.CongViec", "HeSoKhoan", c => c.Int());
            AlterColumn("dbo.CongViec", "DinhMucLaoDong", c => c.Int());
            AlterColumn("dbo.CongViec", "DonGia", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CongViec", "DonGia", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CongViec", "DinhMucLaoDong", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CongViec", "HeSoKhoan", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ChiTietCongViec", "SanLuongThucTe", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CongNhan", "LuongBaoHiem", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CongNhan", "LuongHopDong", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
