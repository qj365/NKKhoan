using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NKKhoan.Models
{
    public class PhongBan
    {
        [Key]
        [Display (Name = "Mã phòng ban")]
        public int MaPhongBan { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên phòng ban")]
        [Required(ErrorMessage = "Vui lòng nhập tên phòng ban")]
        public string TenPhongBan { get; set; }

        public virtual ICollection<CongNhan> CongNhan { get; set; }
    }
}