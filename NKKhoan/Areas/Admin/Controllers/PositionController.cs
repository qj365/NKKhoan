using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Areas.Admin.Controllers
{
    public class PositionController : Controller
    {
        private ApplicationDbContext _context;
        public PositionController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin/Position
        public ActionResult Index()
        {
            var position = _context.ChucVu.ToList();
            return View(position);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var position = _context.ChucVu.SingleOrDefault(c => c.MaChucVu == id);
            if (position == null)
                return HttpNotFound();
            return View(position);
        }
        public ActionResult Delete(int id)
        {
            var position = _context.ChucVu.SingleOrDefault(c => c.MaChucVu == id);
            if (position == null)
                return HttpNotFound();
            else
            {
                _context.ChucVu.Remove(position);
                _context.SaveChanges();
                return RedirectToAction("Index", "Position");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ChucVu position)
        {
            if (position.MaChucVu == 0)
                _context.ChucVu.Add(position);
            else
            {
                var positionInDb = _context.ChucVu.Single(c => c.MaChucVu == position.MaChucVu);
                positionInDb.TenChucVu = position.TenChucVu;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Position");
        }
    }
}