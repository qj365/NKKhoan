using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Areas.Admin.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private ApplicationDbContext _context;

        public TaskController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Admin/Task
        public ActionResult Index()
        {
            var task = _context.NKSLK.ToList();
            return View(task);
        }

        public ViewResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var nkslk = _context.NKSLK.SingleOrDefault(c => c.MaNKSLK == id);
            if (nkslk == null)
                return HttpNotFound();
            return View(nkslk);
        }
    }
}