namespace NKKhoan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietNhanCongKhoan")]
    public partial class ChiTietNhanCongKhoan
    {
        [Key]
        [Column(Order = 0)]
        public int MaNKSLK { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNhanCong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayThucHienKhoan { get; set; }

        public TimeSpan? GioBatDau { get; set; }

        public TimeSpan? GioKetThuc { get; set; }

        public virtual CongNhan CongNhan { get; set; }

        public virtual NKSLK NKSLK { get; set; }
    }
}
