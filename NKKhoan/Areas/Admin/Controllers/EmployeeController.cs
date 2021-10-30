using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Areas.Admin.Controllers
{
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
        public ActionResult Index()
        {
            var employee = _context.CongNhan.ToList();
            return View(employee);
        }

        public ViewResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var employee = _context.CongNhan.SingleOrDefault(c => c.MaNhanCong == id);
            if (employee == null)
                return HttpNotFound();
            return View(employee);
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
    }
}