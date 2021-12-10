using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKKhoan.Areas.Admin.ViewModels
{
    public class ShiftListBoxViewModel
    {
        public List<SelectListItem> shifts { set; get; }
        public int selectedshift { set; get; }

        [Display(Name = "Ca")]
        public string shiftname { set; get; }
    }
}