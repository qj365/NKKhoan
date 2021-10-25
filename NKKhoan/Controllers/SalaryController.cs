using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Controllers
{
    public class SalaryController : Controller
    {
        // GET: Salary
        private ApplicationDbContext _context;

        public SalaryController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Salary()
        {
            var schedule = _context.ChiTietCongViec.ToList();
            return View(schedule);
        }
    }
}