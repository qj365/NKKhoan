using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NKKhoan.Areas.Admin.Controllers
{
    public class JobController : Controller
    {
        private ApplicationDbContext _context;
        public JobController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin/Job
        public ActionResult Index()
        {
            var job = _context.CongViec.ToList();
            return View(job);
        }
        public ViewResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var job = _context.CongViec.SingleOrDefault(c => c.MaCongViec == id);
            if (job == null)
                return HttpNotFound();
            return View(job);
        }

        public ActionResult Delete(int id)
        {
            var job = _context.CongViec.SingleOrDefault(c => c.MaCongViec == id);
            if (job == null)
                return HttpNotFound();
            else
            {
                _context.CongViec.Remove(job);
                _context.SaveChanges();
                return RedirectToAction("Index", "Job");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CongViec job)
        {
            if (job.MaCongViec == 0)
                _context.CongViec.Add(job);
            else
            {
                var jobInDb = _context.CongViec.Single(c => c.MaCongViec == job.MaCongViec);
                jobInDb.TenCongViec = job.TenCongViec;
                jobInDb.DinhMucKhoan = job.DinhMucKhoan;
                jobInDb.DonViKhoan = job.DonViKhoan;
                jobInDb.HeSoKhoan = job.HeSoKhoan;
                jobInDb.DinhMucLaoDong = job.DinhMucLaoDong;
                int? dongia =(int?)( 126360 * jobInDb.HeSoKhoan * jobInDb.DinhMucLaoDong / jobInDb.DinhMucKhoan);
                jobInDb.DonGia = dongia;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Job");
        }
    }
}