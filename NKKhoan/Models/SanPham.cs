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
        

        [Key]
        [Display(Name ="Mã sản phẩm")]
        public int MaSanPham { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string TenSanPham { get; set; }

        [StringLength(50)]
        [Display(Name = "Số đăng ký")]
        [Required(ErrorMessage = "Vui lòng nhập số đăng ký")]
        public string SoDangKy { get; set; }

        [Display(Name = "Hạn sử dụng")]
        [Required(ErrorMessage = "Vui lòng nhập hạn sử dụng")]
        [Range(0, int.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public int? HanSuDung { get; set; }

        [StringLength(50)]
        [Display(Name = "Quy cách")]
        [Required(ErrorMessage = "Vui lòng nhập quy cách")]
        public string QuyCach { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày đăng ký")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Vui lòng nhập ngày đăng ký")]
        public DateTime? NgayDangKy { get; set; }

        [Required(ErrorMessage = "Vui lòng upload ảnh")]
        public string Anh { get; set; }

        public virtual ICollection<CongViec> CongViec { get; set; }


    }
}
