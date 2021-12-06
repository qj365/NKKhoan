using NKKhoan.Areas.Admin.ViewModels;
using NKKhoan.Models;
using NKKhoan.Areas.Admin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace NKKhoan.Areas.Admin.Controllers
{
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
            List<TaskViewModel> taskViewModels = new List<TaskViewModel>();

            taskViewModels = TaskDAO.getListTask();
            return View(taskViewModels);
        }

        public ViewResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            TaskViewModel task = new TaskViewModel();
            if (task == null)
                return HttpNotFound();
            task.nkslk = _context.NKSLK.SingleOrDefault(c => c.MaNKSLK == id);
            return View(task);
        }

        public ActionResult Delete(int id)
        {
            var task = _context.NKSLK.SingleOrDefault(c => c.MaNKSLK == id);
            if (task == null)
                return HttpNotFound();
            else
            {
                _context.NKSLK.Remove(task);
                _context.SaveChanges();
                return RedirectToAction("Index", "Task");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TaskViewModel task, int[] selectedemployee = null, int[] selectedjob = null)
        {
            if (task.nkslk.MaNKSLK == 0)
            {
                _context.NKSLK.Add(task.nkslk);
                _context.SaveChanges();
                var id = _context.NKSLK.OrderByDescending(x => x.MaNKSLK).FirstOrDefault();
                if (selectedemployee != null)
                {
                    foreach (var item in selectedemployee)
                    {
                        TaskDAO.AssignEmployeeToTask(item, id.MaNKSLK);
                    }
                    _context.SaveChanges();
                }
                if (selectedjob != null)
                {
                    foreach (var item in selectedjob)
                    {
                        TaskDAO.AssignJobToTask(item, id.MaNKSLK);
                    }
                    _context.SaveChanges();
                }
            }
            else
            {
                var NKSLKInDb = _context.NKSLK.Single(c => c.MaNKSLK == task.nkslk.MaNKSLK);
                NKSLKInDb.MaNKSLK = task.nkslk.MaNKSLK;
                NKSLKInDb.NgayThucHienKhoan = task.nkslk.NgayThucHienKhoan;
                NKSLKInDb.GioBatDau = task.nkslk.GioBatDau;
                NKSLKInDb.GioKetThuc = task.nkslk.GioKetThuc;
                _context.SaveChanges();
                foreach (var item in selectedemployee)
                {
                    if (!_context.ChiTietNhanCongKhoan.Where(x => x.MaNKSLK == task.nkslk.MaNKSLK).Any(x => x.MaNhanCong == item))
                    {
                        TaskDAO.AssignEmployeeToTask(item, task.nkslk.MaNKSLK);
                    }
                }
                foreach (var item in _context.ChiTietNhanCongKhoan.Where(x => x.MaNKSLK == task.nkslk.MaNKSLK))
                {
                    if (!selectedemployee.Any(x => x == item.MaNhanCong))
                    {
                        TaskDAO.FreeEmployeeFromTask(item.MaNhanCong, task.nkslk.MaNKSLK);
                    }
                }

            }
            return RedirectToAction("Index", "Task");
        }

        [ChildActionOnly]
        public ActionResult ListBoxEmployee(int? MaNKSLK = null)
        {
            var employee = new EmployeeListBoxViewModel();
            employee.employees = _context.CongNhan.Select(a => new SelectListItem() { Value = a.MaNhanCong.ToString(), Text = a.HoTen }).ToList();
            if (MaNKSLK != null)
            {
                employee.selectedemployee = TaskDAO.getListEmployeeWorkedOn(MaNKSLK).Select(x => x.MaNhanCong).ToArray();
            }
            return PartialView(employee);
        }

        [ChildActionOnly]
        public ActionResult ListBoxJob(int? MaNKSLK = null)
        {
            var job = new JobListBoxViewModel();
            job.jobs = _context.CongViec.Select(a => new SelectListItem() { Value = a.MaCongViec.ToString(), Text = a.TenCongViec }).ToList();
            if (MaNKSLK != null)
            {
                job.selectedjob = TaskDAO.getListJobWorkedOn(MaNKSLK).Select(x => x.MaCongViec).ToArray();
            }
            return PartialView(job);
        }
    }
}