using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Admin/Product
        public ActionResult Index(string Select = null)
        {
            IQueryable<SanPham> productQuery = _context.SanPham;

            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime ngaydangky = DateTime.ParseExact("15/08/2019", "dd/MM/yyyy", provider);

            if (Select == "1")
            {
                productQuery = productQuery.Where(c => c.NgayDangKy < ngaydangky);
            }

            if (Select == "2")
            {
                productQuery = productQuery.Where(c => c.HanSuDung > 12);
            }

            var product = productQuery.ToList();
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var product = _context.SanPham.SingleOrDefault(c => c.MaSanPham == id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var product = _context.SanPham.SingleOrDefault(c => c.MaSanPham == id);
            if (product == null)
                return HttpNotFound();
            else
            {
                _context.SanPham.Remove(product);
                _context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SanPham product)
        {
            if (product.MaSanPham == 0)
                _context.SanPham.Add(product);
            else
            {
                var productInDb = _context.SanPham.Single(c => c.MaSanPham == product.MaSanPham);
                productInDb.TenSanPham = product.TenSanPham;
                productInDb.SoDangKy = product.SoDangKy;
                productInDb.QuyCach = product.QuyCach;
                productInDb.NgayDangKy = product.NgayDangKy;
                productInDb.HanSuDung = product.HanSuDung;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Product");


            //try
            //{
            //    if (product.MaSanPham == 0)
            //        _context.SanPham.Add(product);
            //    else
            //    {
            //        var productInDb = _context.SanPham.Single(c => c.MaSanPham == product.MaSanPham);
            //        productInDb.TenSanPham = product.TenSanPham;
            //        productInDb.SoDangKy = product.SoDangKy;
            //        productInDb.QuyCach = product.QuyCach;
            //        productInDb.NgayDangKy = product.NgayDangKy;
            //        productInDb.HanSuDung = product.HanSuDung;
            //    }

            //    _context.SaveChanges();
            //}
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            //return RedirectToAction("Index", "Product");
        }
    }
}