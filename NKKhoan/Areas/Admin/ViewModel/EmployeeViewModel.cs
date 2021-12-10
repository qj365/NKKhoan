using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace NKKhoan.Areas.Admin.ViewModel
{
    public class EmployeeViewModel
    {
        public string Title
        {
            get
            {
                return MaNhanCong == 0 ? "Thêm công nhân" : "Sửa công nhân";
            }
        }

        public EmployeeViewModel()
        {
            MaNhanCong = 0;
        }

        public EmployeeViewModel(CongNhan congNhan)
        {
            HoTen = congNhan.HoTen;
            NgayNamSinh = congNhan.NgayNamSinh;
            MaChucVu = congNhan.MaChucVu;
            MaNhanCong = congNhan.MaNhanCong;
            MaPhongBan = congNhan.MaPhongBan;
            QueQuan = congNhan.QueQuan;
            GioiTinh = congNhan.GioiTinh;
            LuongBaoHiem = congNhan.LuongBaoHiem;
            LuongHopDong = congNhan.LuongHopDong;
            
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
        [Range(0, int.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public int? LuongHopDong { get; set; }

        [Display(Name = "Lương bảo hiểm")]
        [Required(ErrorMessage = "Vui lòng nhập lương bảo hiểm")]
        [Range(0, int.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public int? LuongBaoHiem { get; set; }

        [Display(Name = "Phòng ban")]
        [Required]
        public int? MaPhongBan { get; set; }
        [ForeignKey("MaPhongBan")]
        

        [Display(Name = "Chức vụ")]
        [Required]
        public int? MaChucVu { get; set; }
        [ForeignKey("MaChucVu")]
        

        public IEnumerable<PhongBan> PhongBans { get; set; }
        public IEnumerable<ChucVu> ChucVus { get; set; }
    }
}