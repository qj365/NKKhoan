using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Xunit;

namespace NKKhoan.Areas.Admin.ViewModel
{
    public class EmployeeStatisticViewModel
    {
        public EmployeeStatisticViewModel()
        {

        }

        public EmployeeStatisticViewModel(CongNhan congNhan)
        {
            HoTen = congNhan.HoTen;
            MaNhanCong = congNhan.MaNhanCong;
            PhongBan = congNhan.PhongBan;
            ChucVu = congNhan.ChucVu;
            LuongSP = 0;
            NgayCong = 0;

        }

        [Key]
        [Display(Name = "Mã công nhân")]
        public int MaNhanCong { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string HoTen { get; set; }

        [Display(Name = "Lương sản phẩm")]
        public int? LuongSP { get; set; }

        [Display(Name = "Ngày công")]
        public double? NgayCong { get; set; }

        public virtual PhongBan PhongBan { get; set; }

        public virtual ChucVu ChucVu { get; set; }


    }
}