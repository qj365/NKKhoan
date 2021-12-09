using NKKhoan.Areas.Admin.ViewModel;
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
            var sp = _context.SanPham.ToList();
            var viewModel = new JobViewModel
            {
                SanPhams = sp
            };

            return View("JobForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var job = _context.CongViec.SingleOrDefault(c => c.MaCongViec == id);
            if (job == null)
                return HttpNotFound();
            var sp = _context.SanPham.ToList();
            var viewModel = new JobViewModel
            {
                SanPhams = sp
            };

            return View("JobForm", viewModel);
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