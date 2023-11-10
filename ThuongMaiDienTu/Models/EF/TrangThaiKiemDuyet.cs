namespace ThuongMaiDienTu.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrangThaiKiemDuyet")]
    public partial class TrangThaiKiemDuyet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDTrangThai { get; set; }

        [StringLength(100)]
        public string KieuTrangThai { get; set; }
    }
}
