using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NKKhoan.Models;
using NKKhoan.Areas.Admin.ViewModel;
using System.Data.Entity;

namespace NKKhoan.Areas.Admin.Controllers
{
    public class JobStatisticController : Controller
    {
        private ApplicationDbContext _context;
        public JobStatisticController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin/JobStatistic
        public ActionResult Index()
        {
            var jobs = _context.CongViec.ToList();
            var list = new List<JobViewModel>();
            foreach (var job in jobs)
            {
                var item = new JobViewModel(job);

                item.MaNKSLK = _context.ChiTietCongViec.Where(c => c.MaCongViec == item.MaCongViec).Count();

                list.Add(item);
            }

            return View(list);
        }
    }
}