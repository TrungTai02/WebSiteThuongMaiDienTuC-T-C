namespace ThuongMaiDienTu.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            BaiDangs = new HashSet<BaiDang>();
            LichSuTroChuyens = new HashSet<LichSuTroChuyen>();
            PhanQuyens = new HashSet<PhanQuyen>();
            ViTiens = new HashSet<ViTien>();
        }

        public int UserID { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(150)]
        public string HoVaTen { get; set; }

        [StringLength(10)]
        public string SDT { get; set; }

        [StringLength(10)]
        public string NgaySinh { get; set; }

        [StringLength(1000)]
        public string DiaChi { get; set; }

        public int ChucVu { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public bool EmailConfimed { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiDang> BaiDangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuTroChuyen> LichSuTroChuyens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }
        public virtual ICollection<ViTien> ViTiens { get; set; }
    }
}
