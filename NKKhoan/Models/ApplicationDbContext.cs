using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NKKhoan.Models
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<CongNhan> CongNhan { get; set; }
        public virtual DbSet<CongViec> CongViec { get; set; }
        public virtual DbSet<ChiTietCongViec> ChiTietCongViec { get; set; }
        public virtual DbSet<ChiTietNhanCongKhoan> ChiTietNhanCongKhoan { get; set; }
        public virtual DbSet<NKSLK> NKSLK { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<PhongBan> PhongBan { get; set; }
        public virtual DbSet<ChucVu> ChucVu { get; set; }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CongNhan>()
                .HasMany(e => e.ChiTietNhanCongKhoan)
                .WithRequired(e => e.CongNhan)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CongViec>()
                .HasMany(e => e.ChiTietCongViec)
                .WithRequired(e => e.CongViec)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<NKSLK>()
                .HasMany(e => e.ChiTietCongViec)
                .WithRequired(e => e.NKSLK)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<NKSLK>()
                .HasMany(e => e.ChiTietNhanCongKhoan)
                .WithRequired(e => e.NKSLK)
                .WillCascadeOnDelete(true);

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
