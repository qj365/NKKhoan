using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NKKhoan.Models
{
    public class ChucVu
    {
        [Key]
        [Display(Name = "Mã chức vụ")]
        public int MaChucVu { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên chức vụ")]
        [Required(ErrorMessage = "Vui lòng nhập tên chức vụ")]
        public string TenChucVu { get; set; }

        public virtual ICollection<CongNhan> CongNhan { get; set; }
    }
}