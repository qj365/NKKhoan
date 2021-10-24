using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Controllers
{
    public class WorkerController : Controller
    {
        // GET: Worker
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Worker()
        {
            return View();
        }
    }
}