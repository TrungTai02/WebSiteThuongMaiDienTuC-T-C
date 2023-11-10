namespace ThuongMaiDienTu.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiDang")]
    public partial class BaiDang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiDang()
        {
            HinhAnhBaiDangs = new HashSet<HinhAnhBaiDang>();
            HinhAnhBaiDangs1 = new HashSet<HinhAnhBaiDang>();
        }

        [Key]
        public int IDBaiDang { get; set; }

        [StringLength(500)]
        public string ChuDe { get; set; }

        [StringLength(1000)]
        public string HinhAnh { get; set; }

        [StringLength(2000)]
        public string NoiDung { get; set; }

        public DateTime? NgayDang { get; set; }

        public DateTime? NgayHetHan { get; set; }

        public int? UserID { get; set; }

        [StringLength(500)]
        public string TrangThai { get; set; }

        public int IDDanhMuc { get; set; }

        public int? IDKiemDuyet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnhBaiDang> HinhAnhBaiDangs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual TrangThai TrangThai1 { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnhBaiDang> HinhAnhBaiDangs1 { get; set; }
    }
}
