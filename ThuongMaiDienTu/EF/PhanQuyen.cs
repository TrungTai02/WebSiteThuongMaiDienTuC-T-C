namespace ThuongMaiDienTu.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhanQuyen")]
    public partial class PhanQuyen
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idNhanVien { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idChucNang { get; set; }

        [StringLength(10)]
        public string GhiChu { get; set; }

        public virtual ChucNang ChucNang { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
