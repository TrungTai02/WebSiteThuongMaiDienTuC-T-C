namespace ThuongMaiDienTu.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhAnhBaiDang")]
    public partial class HinhAnhBaiDang
    {
        [Key]
        public int IDHinhAnh { get; set; }

        public int? IDBaiDang { get; set; }

        [StringLength(255)]
        public string DuongDanHinhAnh { get; set; }

        public virtual BaiDang BaiDang { get; set; }

        public virtual BaiDang BaiDang1 { get; set; }
    }
}
