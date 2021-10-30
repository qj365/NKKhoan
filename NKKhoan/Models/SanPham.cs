namespace NKKhoan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietCongViec = new HashSet<ChiTietCongViec>();
        }

        [Key]
        [Display(Name ="Mã sản phẩm")]
        public int MaSanPham { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên sản phẩm")]
        public string TenSanPham { get; set; }

        [StringLength(50)]
        [Display(Name = "Số đăng ký")]
        public string SoDangKy { get; set; }

        [Display(Name = "Hạn sử dụng")]
        public int? HanSuDung { get; set; }

        [StringLength(50)]
        [Display(Name = "Quy cách")]
        public string QuyCach { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày đăng ký")]
        public DateTime? NgayDangKy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietCongViec> ChiTietCongViec { get; set; }
    }
}
