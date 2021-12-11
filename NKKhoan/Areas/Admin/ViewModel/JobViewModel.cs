using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NKKhoan.Areas.Admin.ViewModel
{
    public class JobViewModel
    {
        public JobViewModel()
        {

        }

        public JobViewModel(CongViec ct)
        {
            MaCongViec = ct.MaCongViec;
            TenCongViec = ct.TenCongViec;
            DinhMucKhoan = ct.DinhMucKhoan;
            DonViKhoan = ct.DonViKhoan;
            HeSoKhoan = ct.HeSoKhoan;
            DinhMucLaoDong = ct.DinhMucLaoDong;
            DonGia = ct.DonGia;
            MaSanPham = ct.MaSanPham;
            MaNKSLK = 0;
            TenSanPham = ct.SanPham.TenSanPham;

        }
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Tổng NKSLK")]
        public int MaNKSLK { get; set; }

        [Key]
        [Display(Name = "Mã công việc")]
        public int MaCongViec { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên công việc")]
        public string TenCongViec { get; set; }

        [Display(Name = "Định mức khoán")]
        public int? DinhMucKhoan { get; set; }

        [StringLength(50)]
        [Display(Name = "Đơn vị khoán")]
        public string DonViKhoan { get; set; }

        [Display(Name = "Hệ số khoán")]
        public int? HeSoKhoan { get; set; }

        [Display(Name = "Định mức lao động")]
        public int? DinhMucLaoDong { get; set; }

        [Display(Name = "Đơn giá")]
        public int? DonGia { get; set; }

        [Display(Name = "Sản phẩm")]
        [Required]
        public int? MaSanPham { get; set; }
        [ForeignKey("MaSanPham")]

        [StringLength(50)]
        [Display(Name = "Tên sản phẩm")]
        public string TenSanPham { get; set; }


    }
}