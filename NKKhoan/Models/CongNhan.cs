namespace NKKhoan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CongNhan")]
    public partial class CongNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CongNhan()
        {
            ChiTietNhanCongKhoan = new HashSet<ChiTietNhanCongKhoan>();
        }

        [Key]
        [Display(Name = "Mã công nhân")]
        public int MaNhanCong { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày sinh")]
        public DateTime? NgayNamSinh { get; set; }

        [StringLength(50)]
        [Display(Name = "Phòng ban")]
        public string PhongBan { get; set; }

        [StringLength(50)]
        [Display(Name = "Chức vụ")]
        public string ChucVu { get; set; }

        [StringLength(50)]
        [Display(Name = "Quê quán")]
        public string QueQuan { get; set; }

        [StringLength(50)]
        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }

        [StringLength(100)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [Display(Name = "Lương hợp đồng")]
        public int? LuongHopDong { get; set; }

        [Display(Name = "Lương bảo hiểm")]
        public int? LuongBaoHiem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietNhanCongKhoan> ChiTietNhanCongKhoan { get; set; }
    }
}
