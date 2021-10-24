namespace NKKhoan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietCongViec")]
    public partial class ChiTietCongViec
    {
        [Key]
        [Column(Order = 0)]
        public int MaNKSLK { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaCongViec { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SanPhamApDung { get; set; }

        public decimal? SanLuongThucTe { get; set; }

        [StringLength(50)]
        public string SoLoSanPham { get; set; }

        public virtual CongViec CongViec { get; set; }

        public virtual NKSLK NKSLK { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
