namespace ThuongMaiDienTu.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiaoDich")]
    public partial class GiaoDich
    {
        [Key]
        public int IDGiaoDich { get; set; }

        public int? IDViTien { get; set; }

        public decimal? SoDuVi { get; set; }

        public DateTime? NgayGiaoDich { get; set; }

        public string MoTa { get; set; }

        public virtual ViTien ViTien { get; set; }
    }
}
