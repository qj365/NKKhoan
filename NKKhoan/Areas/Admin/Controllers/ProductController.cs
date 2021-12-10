using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Areas.Admin.Controllers
{
    [Authorize]
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
        public ActionResult Index(string Select = null, string searchTuHSD = null, 
            string searchDenHSD = null, string searchTuNDK = null, string searchDenNDK = null)
        {
            IQueryable<SanPham> productQuery = _context.SanPham;

            ViewBag.SearchTuHSD = searchTuHSD;
            ViewBag.SearchDenHSD = searchDenHSD;
            ViewBag.SearchTuNDK = searchTuNDK;
            ViewBag.SearchDenNDK = searchDenNDK;

            bool TuHSD = string.IsNullOrEmpty(searchTuHSD);

            bool DenHSD = string.IsNullOrEmpty(searchDenHSD);

            bool TuNDK = string.IsNullOrEmpty(searchTuNDK);

            bool DenNDK = string.IsNullOrEmpty(searchDenNDK);

            StringBuilder SqlCommand = new StringBuilder();

            SqlCommand.Append(" SELECT ");
            SqlCommand.Append(" sp.MaSanPham MaSanPham, ");
            SqlCommand.Append(" sp.TenSanPham TenSanPham, ");
            SqlCommand.Append(" sp.SoDangKy SoDangKy, ");
            SqlCommand.Append(" sp.HanSuDung HanSuDung, ");
            SqlCommand.Append(" sp.QuyCach QuyCach, ");
            SqlCommand.Append(" sp.NgayDangKy NgayDangKy, ");
            SqlCommand.Append(" sp.Anh Anh ");
            SqlCommand.Append(" FROM SanPham sp ");
            SqlCommand.Append(" WHERE 1=1 ");

            //Tim kiem theo Han su dung
            if (!TuHSD && DenHSD)
            {
                float fTuHSD = float.Parse(searchTuHSD);
                SqlCommand.Append(" and HanSuDung >= " + fTuHSD);
            }
            if (!DenHSD && TuHSD)
            {
                float fDenHSD = float.Parse(searchDenHSD);
                SqlCommand.Append(" and HanSuDung <= " + fDenHSD);
            }
            if (!TuHSD && !DenHSD)
            {
                float fTuHSD = float.Parse(searchTuHSD);
                float fDenHSD = float.Parse(searchDenHSD);

                SqlCommand.Append(" and HanSuDung >= " + fTuHSD + " and HanSuDung <= " + fDenHSD);
            }

            //Tim kiem theo ngay dang ky
            if (!TuNDK && DenNDK)
            {
                DateTime dtTuNDK = DateTime.Parse(searchTuNDK);
                string strTuNDK = dtTuNDK.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgayDangKy >= '" + strTuNDK + "'");
            }
            if (!DenNDK && TuNDK)
            {
                DateTime dtDenNDK = DateTime.Parse(searchDenNDK);
                string strDenNDK = dtDenNDK.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgayDangKy <= '" + strDenNDK + "'");
            }
            if (!TuNDK && !DenNDK)
            {
                DateTime dtTuNDK = DateTime.Parse(searchTuNDK);
                DateTime dtDenNDK = DateTime.Parse(searchDenNDK);

                string strTuNDK = dtTuNDK.ToString("yyyy/MM/dd");
                string strDenNDK = dtDenNDK.ToString("yyyy/MM/dd");
                SqlCommand.Append(" and NgayDangKy >= '" + strTuNDK + "'" + " and NgayDangKy <= '" + strDenNDK + "'");
            }
            //CultureInfo provider = CultureInfo.InvariantCulture;
            //DateTime ngaydangky = DateTime.ParseExact("15/08/2019", "dd/MM/yyyy", provider);

            //if (Select == "1")
            //{
            //    productQuery = productQuery.Where(c => c.NgayDangKy < ngaydangky);
            //}

            //if (Select == "2")
            //{
            //    productQuery = productQuery.Where(c => c.HanSuDung > 12);
            //}
            //var product = productQuery.ToList();
            //return View(product);

            var lst = _context.Database.SqlQuery<SanPham>("" + SqlCommand)
                .ToList<SanPham>();
            return View(lst);
            
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

        public ActionResult Info(int id)
        {
            var product = _context.SanPham.SingleOrDefault(c => c.MaSanPham == id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SanPham product, HttpPostedFileBase photo)
        {
            if (product.MaSanPham == 0)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Data/ProductImage/"),
                                            System.IO.Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);

                    product.Anh = photo.FileName;
                }
                else
                    product.Anh = "NoImage.png";
                _context.SanPham.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }               
            else
            {
                var productInDb = _context.SanPham.Single(c => c.MaSanPham == product.MaSanPham);
                productInDb.TenSanPham = product.TenSanPham;
                productInDb.SoDangKy = product.SoDangKy;
                productInDb.QuyCach = product.QuyCach;
                productInDb.NgayDangKy = product.NgayDangKy;
                productInDb.HanSuDung = product.HanSuDung;
                string photoBefore = productInDb.Anh;
                if (photo != null && photo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Data/ProductImage/"),
                                            System.IO.Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);

                    productInDb.Anh = photo.FileName;                   
                }
                else if(photo == null)
                {
                    productInDb.Anh = photoBefore;
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }             
        }
    }
}