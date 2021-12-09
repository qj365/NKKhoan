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
            _ = new List<TaskViewModel>();

            List<TaskViewModel> taskViewModels = TaskDAO.getListTask();
            return View(taskViewModels);
        }

        public ViewResult Create()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            TaskViewModel task = new TaskViewModel();
            if (task == null)
                return HttpNotFound();
            task.nkslk = _context.NKSLK.SingleOrDefault(c => c.MaNKSLK == id);
            task.congnhan = TaskDAO.getListEmployeeWorkedOn(id);
            task.congviec = TaskDAO.getListJobWorkedOn(id);
            return View(task);
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
        public ActionResult Save(TaskViewModel task, int selectedshift, int[] selectedemployee = null, int[] selectedjob = null)
        {
            if (task.nkslk.GioBatDau == new TimeSpan(0, 0, 0) || task.nkslk.GioKetThuc == new TimeSpan(0, 0, 0))
            {
                switch (selectedshift)
                {
                    case 1:
                        task.nkslk.GioBatDau = new TimeSpan(6, 0, 0);
                        task.nkslk.GioKetThuc = new TimeSpan(14, 0, 0);
                        break;
                    case 2:
                        task.nkslk.GioBatDau = new TimeSpan(14, 0, 0);
                        task.nkslk.GioKetThuc = new TimeSpan(22, 0, 0);
                        break;
                    case 3:
                        task.nkslk.GioBatDau = new TimeSpan(22, 0, 0);
                        task.nkslk.GioKetThuc = new TimeSpan(6, 0, 0);
                        break;
                    default:
                        break;
                }
            }
            if (task.nkslk.MaNKSLK == 0)
            {
                _context.NKSLK.Add(task.nkslk);
                _context.SaveChanges();
                var id = _context.NKSLK.OrderByDescending(x => x.MaNKSLK).FirstOrDefault();
                if (selectedemployee != null)
                {
                    foreach (var item in selectedemployee)
                    {
                        TaskDAO.AssignEmployeeToTask(id.MaNKSLK, item);
                    }
                    _context.SaveChanges();
                }
                if (selectedjob != null)
                {
                    foreach (var item in selectedjob)
                    {
                        TaskDAO.AssignJobToTask(id.MaNKSLK, item);
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
                if (selectedemployee == null)
                {
                    TaskDAO.FreeEmployeeFromTask(task.nkslk.MaNKSLK);
                }
                else
                {
                    foreach (var item in selectedemployee)
                    {
                        if (!_context.ChiTietNhanCongKhoan.Where(x => x.MaNKSLK == task.nkslk.MaNKSLK).Any(x => x.MaNhanCong == item))
                        {
                            TaskDAO.AssignEmployeeToTask(task.nkslk.MaNKSLK, item);
                        }
                    }
                    foreach (var item in _context.ChiTietNhanCongKhoan.Where(x => x.MaNKSLK == task.nkslk.MaNKSLK))
                    {
                        if (!selectedemployee.Any(x => x == item.MaNhanCong))
                        {
                            TaskDAO.FreeEmployeeFromTask(task.nkslk.MaNKSLK, item.MaNhanCong);
                        }
                    }
                }
                if (selectedjob == null)
                {
                    TaskDAO.FreeJobFromTask(task.nkslk.MaNKSLK);
                }
                else
                {
                    foreach (var item in selectedjob)
                    {
                        if (!_context.ChiTietCongViec.Where(x => x.MaNKSLK == task.nkslk.MaNKSLK).Any(x => x.MaCongViec == item))
                        {
                            TaskDAO.AssignJobToTask(task.nkslk.MaNKSLK, item);
                        }
                    }
                    foreach (var item in _context.ChiTietCongViec.Where(x => x.MaNKSLK == task.nkslk.MaNKSLK))
                    {
                        if (!selectedjob.Any(x => x == item.MaCongViec))
                        {
                            TaskDAO.FreeJobFromTask(task.nkslk.MaNKSLK, item.MaCongViec);
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Task");
        }

        [ChildActionOnly]
        public ActionResult ListBoxShift(TimeSpan? giobatdau = null, TimeSpan? gioketthuc = null)
        {
            var shift = new ShiftListBoxViewModel();
            var shiftlistitem = new List<SelectListItem>();
            shiftlistitem.Add(new SelectListItem() { Text = "Ca 1 (6h-14h)", Value = "1" });
            shiftlistitem.Add(new SelectListItem() { Text = "Ca 2 (14h-22h)", Value = "2" });
            shiftlistitem.Add(new SelectListItem() { Text = "Ca 3 (22h-6h)", Value = "3" });
            shift.shifts = shiftlistitem;
            if (giobatdau != null && gioketthuc != null)
            {
                if (giobatdau == new TimeSpan(6, 0, 0) && gioketthuc == new TimeSpan(14, 0, 0))
                {
                    shift.selectedshift = 1;
                }
                else if (giobatdau == new TimeSpan(14, 0, 0) && gioketthuc == new TimeSpan(22, 0, 0))
                {
                    shift.selectedshift = 2;
                }
                else if (giobatdau == new TimeSpan(22, 0, 0) && gioketthuc == new TimeSpan(6, 0, 0))
                {
                    shift.selectedshift = 3;
                }
            }
            return PartialView(shift);
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