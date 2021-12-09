using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using System.Globalization;


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
        public ActionResult Index(int? minprice = null, int? maxprice = null, int? ssp = null, string stv = null, string sb = null)
        {
            ViewBag.SanPham = _context.SanPham.ToList();
            var jobQuery = _context.CongViec.Include(c => c.SanPham);
            if (!String.IsNullOrWhiteSpace(sb))
                jobQuery = jobQuery.Where(c => c.TenCongViec.Contains(sb));
            if (minprice != null)
                jobQuery = jobQuery.Where(c => c.DonGia >= minprice);

            if (maxprice != null)
                jobQuery = jobQuery.Where(c => c.DonGia  <= maxprice);
            if (ssp != null)
                jobQuery = jobQuery.Where(c => c.SanPham.MaSanPham == ssp);
            if (!String.IsNullOrWhiteSpace(stv))
            {
                int tb = _context.Database.SqlQuery<int>("Select AVG(dongia) from CongViec ").FirstOrDefault();
                if (stv == "tv1")
                    jobQuery = jobQuery.Where(c => c.DonGia > tb);
                if(stv == "tv2")
                    jobQuery = jobQuery.Where(c => c.DonGia < tb);
            }
            var job = jobQuery.ToList();
            return View(job);
        }
        public ViewResult Create()
        {
            ViewBag.SanPham = _context.SanPham.ToList();

            return View();
        }
        public ActionResult Edit(int id)
        {
            var job = _context.CongViec.SingleOrDefault(c => c.MaCongViec == id);
            if (job == null)
                return HttpNotFound();
            ViewBag.SanPham = _context.SanPham.ToList();

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
            {
                job.DonGia = (int?)(126360 * job.HeSoKhoan * job.DinhMucLaoDong / job.DinhMucKhoan);
                _context.CongViec.Add(job);
            }
            else
            {
                var jobInDb = _context.CongViec.Single(c => c.MaCongViec == job.MaCongViec);
                jobInDb.TenCongViec = job.TenCongViec;
                jobInDb.DinhMucKhoan = job.DinhMucKhoan;
                jobInDb.DonViKhoan = job.DonViKhoan;
                jobInDb.HeSoKhoan = job.HeSoKhoan;
                jobInDb.DinhMucLaoDong = job.DinhMucLaoDong;
                jobInDb.DonGia = (int?)(126360 * job.HeSoKhoan * job.DinhMucLaoDong / job.DinhMucKhoan);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Job");
        }
    }
}