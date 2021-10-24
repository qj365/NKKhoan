using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Controllers
{
    public class ScheduleController : Controller
    {
        private ApplicationDbContext _context;

        public ScheduleController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Schedule
        public ActionResult Schedule()
        {
            var schedule = _context.ChiTietCongViec.ToList();
            return View(schedule);
        }
    }
}