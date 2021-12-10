﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Areas.Admin.ViewModels
{
    public class EmployeeListBoxViewModel
    {
        public List<SelectListItem> employees { set; get; }
        public int[] selectedemployee { set; get; }
        [Display(Name = "Công nhân")]
        public string employeeName { set; get; }
    }
}