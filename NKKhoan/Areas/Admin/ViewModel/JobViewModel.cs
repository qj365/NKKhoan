using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace NKKhoan.Areas.Admin.ViewModel
{
    public class JobViewModel
    {
        public string Title
        {
            get
            {
                return MaCongViec == 0 ? "Thêm công việc" : "Sửa công việc";
            }
        }

        public JobViewModel()
        {
            MaCongViec = 0;
        }
        public JobViewModel(CongViec congViec)
        {
            TenCongViec = congViec.TenCongViec;
            DinhMucKhoan = congViec.DinhMucKhoan;
            DonViKhoan = congViec.DonViKhoan;
            HeSoKhoan = congViec.HeSoKhoan;
            DinhMucLaoDong = congViec.DinhMucLaoDong;
            DonGia = congViec.DonGia;
            MaSanPham = congViec.MaSanPham;

        }
        [Key]
        [Display(Name = "Mã công việc")]
        public int MaCongViec { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên công việc")]
        public string TenCongViec { get; set; }

        [Display(Name = "Định mức khoán")]
        public decimal? DinhMucKhoan { get; set; }

        [StringLength(50)]
        [Display(Name = "Đơn vị khoán")]
        public string DonViKhoan { get; set; }

        [Display(Name = "Hệ số khoán")]
        public int? HeSoKhoan { get; set; }

        [Display(Name = "Định mức lao động")]
        public int? DinhMucLaoDong { get; set; }

        [Display(Name = "Đơn giá")]
        public int? DonGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietCongViec> ChiTietCongViec { get; set; }

        [Display(Name = "Sản phẩm")]
        [Required]
        public int? MaSanPham { get; set; }
        [ForeignKey("MaSanPham")]


        public IEnumerable<SanPham> SanPhams { get; set; }
    }
}