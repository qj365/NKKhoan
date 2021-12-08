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
    public class EmployeeStatisticController : Controller
    {

        private ApplicationDbContext _context;

        public EmployeeStatisticController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Admin/EmployeeStatistic
        public ActionResult Index(string ngaybatdau = null, string ngayketthuc = null)
        {
            var employees = _context.CongNhan.Include(c => c.PhongBan).Include(c => c.ChucVu).ToList();
            var list = new List<EmployeeStatisticViewModel>();

            var nbd = DateTime.ParseExact("01/01/1753", "dd/MM/yyyy", CultureInfo.InvariantCulture); 
            var nkt = DateTime.ParseExact("31/12/9999", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!String.IsNullOrWhiteSpace(ngaybatdau))
            {
                nbd = DateTime.ParseExact(ngaybatdau, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrWhiteSpace(ngayketthuc))
            {
                nkt = DateTime.ParseExact(ngayketthuc, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            foreach (var employee in employees)
            {
                var item = new EmployeeStatisticViewModel(employee);

                int luong = 0;
                double ngaycong = 0;

                try
                {
                    luong = _context.Database.SqlQuery<int>("select dbo.TienLuongSanPhamCongNhan({0}, {1}, {2})", new object[] { item.MaNhanCong, nbd, nkt }).First();
                    ngaycong = _context.Database.SqlQuery<double>("select dbo.NhatKyLamViec({0}, {1}, {2})", new object[] { item.MaNhanCong, nbd, nkt }).First();
                }
                catch { }                 
                            

                item.LuongSP = luong;
                item.NgayCong = ngaycong;

                list.Add(item);
            }

            return View(list);
        }
    }
}