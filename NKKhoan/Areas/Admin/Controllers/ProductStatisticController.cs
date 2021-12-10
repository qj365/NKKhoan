using NKKhoan.Areas.Admin.ViewModel;
using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Areas.Admin.Controllers
{
    public class ProductStatisticController : Controller
    {
        private ApplicationDbContext _context;

        public ProductStatisticController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index(string ngaybatdau = null, string ngayketthuc = null)
        {
            ViewBag.sp = _context.SanPham.ToList();
            ViewBag.cv = _context.CongViec.ToList();
            ViewBag.ctcv = _context.ChiTietCongViec.ToList();
            ViewBag.nkslk = _context.NKSLK.ToList();
            ViewBag.ctnck = _context.ChiTietNhanCongKhoan.ToList();
            ViewBag.cn = _context.CongNhan.ToList();

            //var product = _context.SanPham.SingleOrDefault(c => c.MaSanPham == id);
            //if (product == null)
            //    return HttpNotFound();
            //return View(product);

            IQueryable<NKSLK> nkslkQuery = _context.NKSLK;

            ViewBag.Ngaybatdau = ngaybatdau;
            ViewBag.Ngayketthuc = ngayketthuc;

            bool NBD = string.IsNullOrEmpty(ngaybatdau);

            bool NKT = string.IsNullOrEmpty(ngayketthuc);

            StringBuilder SqlCommand = new StringBuilder();

            SqlCommand.Append(" SELECT ");

            SqlCommand.Append(" nkslk.MaNKSLK MaNKSLK, ");
            SqlCommand.Append(" nkslk.NgayThucHienKhoan NgayThucHienKhoan, ");
            SqlCommand.Append(" nkslk.GioBatDau GioBatDau, ");
            SqlCommand.Append(" nkslk.GioKetThuc GioKetThuc ");

            SqlCommand.Append(" FROM NKSLK nkslk");

            SqlCommand.Append(" WHERE 1=1 ");

            if (!NBD && NKT)
            {
                DateTime dtNBD = DateTime.Parse(ngaybatdau);
                string strNBD = dtNBD.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgayThucHienKhoan >= '" + strNBD + "'");
            }
            if (!NKT && NBD)
            {
                DateTime dtNKT = DateTime.Parse(ngayketthuc);
                string strNKT = dtNKT.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgayThucHienKhoan <= '" + strNKT + "'");
            }
            if (!NBD && !NKT)
            {
                DateTime dtNBD = DateTime.Parse(ngaybatdau);
                DateTime dtNKT = DateTime.Parse(ngayketthuc);

                string strNBD = dtNBD.ToString("yyyy/MM/dd");
                string strNKT = dtNKT.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgayThucHienKhoan >= '" + strNBD + "'" + " and NgayThucHienKhoan <= '" + strNKT + "'");
            }

            var lst = _context.Database.SqlQuery<NKSLK>("" + SqlCommand)
                .ToList<NKSLK>();
            return View(lst);
        }

        public ActionResult Info(int id)
        {
            ViewBag.sp = _context.SanPham.ToList();
            ViewBag.cv = _context.CongViec.ToList();
            ViewBag.ctcv = _context.ChiTietCongViec.ToList();
            ViewBag.nkslk = _context.NKSLK.ToList();
            ViewBag.ctnck = _context.ChiTietNhanCongKhoan.ToList();
            ViewBag.cn = _context.CongNhan.ToList();
            ViewBag.nkslkinfo = _context.NKSLK.SingleOrDefault(c => c.MaNKSLK == id);

            IQueryable<NKSLK> nkslkQuery = _context.NKSLK;

            StringBuilder SqlCommand = new StringBuilder();

            SqlCommand.Append(" SELECT ");

            SqlCommand.Append(" nkslk.MaNKSLK MaNKSLK, ");
            SqlCommand.Append(" nkslk.NgayThucHienKhoan NgayThucHienKhoan, ");
            SqlCommand.Append(" nkslk.GioBatDau GioBatDau, ");
            SqlCommand.Append(" nkslk.GioKetThuc GioKetThuc ");

            SqlCommand.Append(" FROM NKSLK nkslk");

            SqlCommand.Append(" WHERE 1=1 ");
            SqlCommand.AppendFormat(" and nkslk.MaNKSLK = {0}", id);

            var lst = _context.Database.SqlQuery<NKSLK>("" + SqlCommand)
                .ToList<NKSLK>();
            return View(lst);
        }

        public ActionResult InfoEmployee(int id)
        {
            ViewBag.sp = _context.SanPham.ToList();
            ViewBag.cv = _context.CongViec.ToList();
            ViewBag.ctcv = _context.ChiTietCongViec.ToList();
            ViewBag.nkslk = _context.NKSLK.ToList();
            ViewBag.ctnck = _context.ChiTietNhanCongKhoan.ToList();
            ViewBag.cn = _context.CongNhan.ToList();
            ViewBag.cninfo = _context.CongNhan.SingleOrDefault(c => c.MaNhanCong == id);
            ViewBag.pb = _context.PhongBan.ToList();
            ViewBag.cv = _context.ChucVu.ToList();

            IQueryable<CongNhan> cnQuery = _context.CongNhan;

            StringBuilder SqlCommand = new StringBuilder();

            SqlCommand.Append(" SELECT ");

            SqlCommand.Append(" cn.MaNhanCong MaNhanCong, ");
            SqlCommand.Append(" cn.HoTen HoTen, ");
            SqlCommand.Append(" cn.NgayNamSinh NgayNamSinh, ");
            SqlCommand.Append(" cn.QueQuan QueQuan, ");
            SqlCommand.Append(" cn.GioiTinh GioiTinh, ");
            SqlCommand.Append(" cn.MatKhau MatKhau, ");
            SqlCommand.Append(" cn.LuongHopDong LuongHopDong, ");
            SqlCommand.Append(" cn.LuongBaoHiem LuongBaoHiem, ");
            SqlCommand.Append(" cn.MaPhongBan MaPhongBan, ");
            SqlCommand.Append(" cn.MaChucVu MaChucVu ");

            SqlCommand.Append(" FROM CongNhan cn");

            SqlCommand.Append(" WHERE 1=1 ");
            SqlCommand.AppendFormat(" and cn.MaNhanCong = {0}", id);

            var lst = _context.Database.SqlQuery<CongNhan>("" + SqlCommand)
                .ToList<CongNhan>();
            return View(lst);
        }

        public ActionResult InfoProduct(int id)
        {
            ViewBag.sp = _context.SanPham.ToList();
            ViewBag.cv = _context.CongViec.ToList();
            ViewBag.ctcv = _context.ChiTietCongViec.ToList();
            ViewBag.nkslk = _context.NKSLK.ToList();
            ViewBag.ctnck = _context.ChiTietNhanCongKhoan.ToList();
            ViewBag.cn = _context.CongNhan.ToList();
            ViewBag.spinfo = _context.SanPham.SingleOrDefault(c => c.MaSanPham == id);
            ViewBag.pb = _context.PhongBan.ToList();
            ViewBag.cv = _context.ChucVu.ToList();

            IQueryable<SanPham> spQuery = _context.SanPham;

            StringBuilder SqlCommand = new StringBuilder();

            SqlCommand.Append(" SELECT ");
            SqlCommand.Append(" sp.MaSanPham MaSanPham, ");
            SqlCommand.Append(" sp.TenSanPham TenSanPham, ");
            SqlCommand.Append(" sp.SoDangKy SoDangKy, ");
            SqlCommand.Append(" sp.HanSuDung HanSuDung, ");
            SqlCommand.Append(" sp.QuyCach QuyCach, ");
            SqlCommand.Append(" sp.NgayDangKy NgayDangKy, ");
            SqlCommand.Append(" sp.Anh Anh ");
            SqlCommand.Append(" FROM SanPham sp ");
            SqlCommand.Append(" WHERE 1=1 ");
            SqlCommand.AppendFormat(" and sp.MaSanPham = {0}", id);

            var lst = _context.Database.SqlQuery<SanPham>("" + SqlCommand)
                .ToList<SanPham>();
            return View(lst);
        }
    }
}