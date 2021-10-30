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
        [Display(Name = "Mã khoán")]
        public int MaNKSLK { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã nhân công")]
        public int MaNhanCong { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày thực hiện thực")]
        public DateTime? NgayThucHienKhoan { get; set; }

        [Display(Name = "Giờ bắt đầu thực")]
        public TimeSpan? GioBatDau { get; set; }

        [Display(Name = "Giở kết thúc thực")]
        public TimeSpan? GioKetThuc { get; set; }

        public virtual CongNhan CongNhan { get; set; }

        public virtual NKSLK NKSLK { get; set; }
    }
}
