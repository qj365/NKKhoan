
namespace NKKhoan.Areas.Admin.ViewModels
{
    using NKKhoan.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public partial class TaskViewModel
    {
        public TaskViewModel()
        {
            nkslk = new NKSLK();
            congnhan = new List<CongNhan>();
            congviec = new List<CongViec>();
        }
        public virtual NKSLK nkslk { get; set; }
        public virtual IEnumerable<CongNhan> congnhan { get; set; }
        public virtual IEnumerable<CongViec> congviec { get; set; }
    }
}