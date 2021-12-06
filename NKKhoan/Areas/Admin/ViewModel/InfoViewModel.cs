using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NKKhoan.Areas.Admin.ViewModel
{
    public class InfoViewModel
    {

        public InfoViewModel(ChiTietNhanCongKhoan chitiet)
        {
            MaNKSLK = chitiet.MaNKSLK;
            MaNhanCong = chitiet.MaNhanCong;
            NgayThucHienKhoan = chitiet.NgayThucHienKhoan;
            GioBatDau = chitiet.GioBatDau;
            GioKetThuc = chitiet.GioKetThuc;
            CongNhan = chitiet.CongNhan;
            NKSLK = chitiet.NKSLK;
            LuongSP = 0;
            NgayCong = 0;
        }

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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayThucHienKhoan { get; set; }

        [Display(Name = "Giờ bắt đầu thực")]
        public TimeSpan? GioBatDau { get; set; }

        [Display(Name = "Giở kết thúc thực")]
        public TimeSpan? GioKetThuc { get; set; }

        public virtual CongNhan CongNhan { get; set; }

        public virtual NKSLK NKSLK { get; set; }

        [Display(Name = "Lương sản phầm")]
        public int LuongSP { get; set; }

        [Display(Name = "Ngày công")]
        public float NgayCong { get; set; }
    }
}