using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Areas.Admin.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private ApplicationDbContext _context;
        public DepartmentController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin/Department
        public ActionResult Index()
        {
            var department = _context.PhongBan.ToList();
            return View(department);
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var department = _context.PhongBan.SingleOrDefault(c => c.MaPhongBan == id);
            if (department == null)
                return HttpNotFound();
            return View(department);
        }
        public ActionResult Delete(int id)
        {
            var department = _context.PhongBan.SingleOrDefault(c => c.MaPhongBan == id);
            if (department == null)
                return HttpNotFound();
            else
            {
                _context.PhongBan.Remove(department);
                _context.SaveChanges();
                return RedirectToAction("Index", "Department");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(PhongBan department)
        {
            if (department.MaPhongBan == 0)
                _context.PhongBan.Add(department);
            else
            {
                var departmentInDb = _context.PhongBan.Single(c => c.MaPhongBan == department.MaPhongBan);
                departmentInDb.TenPhongBan = department.TenPhongBan;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Department");
        }
    }
}