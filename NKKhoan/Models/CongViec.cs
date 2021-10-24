namespace NKKhoan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CongViec")]
    public partial class CongViec
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CongViec()
        {
            ChiTietCongViec = new HashSet<ChiTietCongViec>();
        }

        [Key]
        public int MaCongViec { get; set; }

        [StringLength(50)]
        public string TenCongViec { get; set; }

        public decimal? DinhMucKhoan { get; set; }

        [StringLength(50)]
        public string DonViKhoan { get; set; }

        public decimal? HeSoKhoan { get; set; }

        public decimal? DinhMucLaoDong { get; set; }

        public decimal? DonGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietCongViec> ChiTietCongViec { get; set; }
    }
}
