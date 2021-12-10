using NKKhoan.Areas.Admin.ViewModel;
using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        private ApplicationDbContext _context;

        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.Cv = _context.CongViec.Count();
            ViewBag.Sp = _context.SanPham.Count();
            ViewBag.Cn = _context.CongNhan.Count();
            ViewBag.Sltt = _context.Database.SqlQuery<int>("select dbo.sanluongthucte()").FirstOrDefault();
            ViewBag.Dmk = _context.Database.SqlQuery<int>("select dbo.dinhmuckhoan()").FirstOrDefault();
            ViewBag.Chuaht = ViewBag.Dmk - ViewBag.Sltt;

            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var list = new List<EmployeeStatisticViewModel>();

            var employees = _context.CongNhan.ToList();
            foreach (var employee in employees)
            {
                var item = new EmployeeStatisticViewModel(employee);


                double ngaycong = 0;

                try
                {
                    ngaycong = _context.Database.SqlQuery<double>("select dbo.NhatKyLamViec({0}, {1}, {2})", new object[] { item.MaNhanCong, firstDayOfMonth, lastDayOfMonth }).FirstOrDefault();
                }
                catch { }

                item.NgayCong = ngaycong;

                list.Add(item);
            }
            list = list.OrderByDescending(c => c.NgayCong).Take(5).ToList();

            return View(list);
        }
    }
}