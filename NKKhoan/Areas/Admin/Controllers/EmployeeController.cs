using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using NKKhoan.Areas.Admin.ViewModel;
using System.Globalization;


namespace NKKhoan.Areas.Admin.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _context;

        public EmployeeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Admin/Employee
        public ActionResult Index(int? spb = null, int? scv = null, int? maxage = null, int? minage = null, string sgt = null,string sb = null, string tuan = null)
        {
            
            ViewBag.PhongBan = _context.PhongBan.ToList();
            ViewBag.ChucVu = _context.ChucVu.ToList();
            var employeeQuery = _context.CongNhan.Include(c => c.PhongBan).Include(c => c.ChucVu);

            if (!String.IsNullOrWhiteSpace(sb))
                employeeQuery = employeeQuery.Where(c => c.HoTen.Contains(sb));

            if (!String.IsNullOrWhiteSpace(sgt))
                employeeQuery = employeeQuery.Where(c => c.GioiTinh == sgt);

            if (spb != null)
                employeeQuery = employeeQuery.Where(c => c.PhongBan.MaPhongBan == spb);

            if (scv != null)
                employeeQuery = employeeQuery.Where(c => c.ChucVu.MaChucVu == scv);

            if (minage != null)                       
                employeeQuery = employeeQuery.Where(c => DbFunctions.DiffDays(c.NgayNamSinh, DateTime.Now) / 365 >= minage);            

            if (maxage != null)
                employeeQuery = employeeQuery.Where(c => DbFunctions.DiffDays(c.NgayNamSinh, DateTime.Now) / 365 <= maxage);

            if (!String.IsNullOrWhiteSpace(tuan))
            {
                var t = DateTime.ParseExact(tuan, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ViewBag.Ndt = _context.Database.SqlQuery<DateTime>("select dbo.getFirstDateOfWeek({0})", new object[] { t }).First().ToString("dd/MM/yyyy");
                ViewBag.Ncc = _context.Database.SqlQuery<DateTime>("select dbo.getFirstDateOfWeek({0})", new object[] { t }).First().ToString("dd/MM/yyyy");

                int gioccongchuan = _context.Database.SqlQuery<int>("select dbo.GioCongChuan({0})", new object[] { t }).First();
                var list = _context.Database.SqlQuery<WorkTimeViewModel>("select * from dbo.NhanVienLamVuotGio ({0}); ", new object[] { t }).ToList();                

                ViewBag.List = list;
                foreach (var item in list)
                {
                    employeeQuery = employeeQuery.Where(c => c.MaNhanCong == item.MaCN);
                }
            }

            var employee = employeeQuery.ToList();
            return View(employee);
        }

        public ViewResult Create()
        {
            var pb = _context.PhongBan.ToList();
            var cv = _context.ChucVu.ToList();

            var viewModel = new EmployeeViewModel
            {
                PhongBans = pb,
                ChucVus = cv
            };

            return View("EmployeeForm",viewModel);
        }

        public ActionResult Edit(int id)
        {
            var employee = _context.CongNhan.SingleOrDefault(c => c.MaNhanCong == id);
            if (employee == null)
                return HttpNotFound();

            var pb = _context.PhongBan.ToList();
            var cv = _context.ChucVu.ToList();

            var viewModel = new EmployeeViewModel(employee)
            {
                PhongBans = pb,
                ChucVus = cv
            };
            return View("EmployeeForm",viewModel);
        }

        public ActionResult Delete(int id)
        {
            var employee = _context.CongNhan.SingleOrDefault(c => c.MaNhanCong == id);
            if (employee == null)
                return HttpNotFound();
            else
            {
                _context.CongNhan.Remove(employee);
                _context.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CongNhan employee)
        {
            if (employee.MaNhanCong == 0)
                _context.CongNhan.Add(employee);
            else
            {
                var employeeInDb = _context.CongNhan.Single(c => c.MaNhanCong == employee.MaNhanCong);
                employeeInDb.HoTen = employee.HoTen;
                employeeInDb.NgayNamSinh = employee.NgayNamSinh;
                employeeInDb.PhongBan = employee.PhongBan;
                employeeInDb.ChucVu = employee.ChucVu;
                employeeInDb.QueQuan = employee.QueQuan;
                employeeInDb.GioiTinh = employee.GioiTinh;
                employeeInDb.LuongBaoHiem = employee.LuongBaoHiem;
                employeeInDb.LuongHopDong = employee.LuongHopDong;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }

        public ActionResult Info(int id, string ngaybatdau = null, string ngayketthuc = null)
        {
            var infosQuery = _context.ChiTietNhanCongKhoan.Where(c=>c.MaNhanCong == id);

            if (!String.IsNullOrWhiteSpace(ngaybatdau))
            {
                var nbd = DateTime.ParseExact(ngaybatdau, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                infosQuery = infosQuery.Where(c=>c.NKSLK.NgayThucHienKhoan >= nbd);
            }

            if (!String.IsNullOrWhiteSpace(ngayketthuc))
            {
                var nkt = DateTime.ParseExact(ngayketthuc, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                infosQuery = infosQuery.Where(c => c.NKSLK.NgayThucHienKhoan <= nkt);
            }

            var infos = infosQuery.ToList();

            if (infos.Count() == 0)
                return HttpNotFound();

            var newInfos = new List<InfoViewModel>();
            int tongluong = 0;
            double tongngaycong = 0;
            foreach (var info in infos)
            {
                var InfoViewModel = new InfoViewModel(info);

                int luong = 0;
                double ngaycong = 0;
                               
                try
                {
                     luong = _context.Database.SqlQuery<int>("select dbo.TienLuongSanPhamCongNhan({0}, {1}, {2})", new object[] { id, info.NKSLK.NgayThucHienKhoan, info.NKSLK.NgayThucHienKhoan }).First();
                     ngaycong = _context.Database.SqlQuery<double>("select dbo.NhatKyLamViec({0}, {1}, {2})", new object[] { id, info.NKSLK.NgayThucHienKhoan, info.NKSLK.NgayThucHienKhoan }).First();
                    tongluong += luong;
                    tongngaycong += ngaycong;
                }
                catch { }              

                ViewBag.tongluong = tongluong;
                ViewBag.tongngaycong = tongngaycong;

                InfoViewModel.LuongSP = luong;
                InfoViewModel.NgayCong = (float)ngaycong;

                newInfos.Add(InfoViewModel);
            }
            return View (newInfos);
        }
    }
}