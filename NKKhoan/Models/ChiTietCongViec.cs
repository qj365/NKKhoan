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
        [Display(Name = "Mã khoán")]
        public int MaNKSLK { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã công việc")]
        public int MaCongViec { get; set; }


        [Display(Name = "Sản lượng thực tế")]
        public int? SanLuongThucTe { get; set; }


        public virtual CongViec CongViec { get; set; }

        public virtual NKSLK NKSLK { get; set; }


    }
}
