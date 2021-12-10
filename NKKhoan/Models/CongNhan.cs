﻿namespace NKKhoan.Models
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
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayNamSinh { get; set; }

        [StringLength(50)]
        [Display(Name = "Quê quán")]
        [Required(ErrorMessage = "Vui lòng nhập quê quán")]
        public string QueQuan { get; set; }

        [StringLength(50)]
        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }


        [Display(Name = "Lương hợp đồng")]
        [Required(ErrorMessage = "Vui lòng nhập lương hợp đồng")]
        [Range(0,int.MaxValue,ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public int? LuongHopDong { get; set; }

        [Display(Name = "Lương bảo hiểm")]
        [Required(ErrorMessage = "Vui lòng nhập lương bảo hiểm")]
        [Range(0, int.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public int? LuongBaoHiem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietNhanCongKhoan> ChiTietNhanCongKhoan { get; set; }

        [Display(Name = "Phòng ban")]
        [Required]
        public int? MaPhongBan { get; set; }
        [ForeignKey("MaPhongBan")]
        public virtual PhongBan PhongBan { get; set; }

        [Display(Name = "Chức vụ")]
        [Required]
        public int? MaChucVu { get; set; }
        [ForeignKey("MaChucVu")]
        public virtual ChucVu ChucVu { get; set; }

    }
}
