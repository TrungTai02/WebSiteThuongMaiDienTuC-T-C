namespace ThuongMaiDienTu.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViTien")]
    public partial class ViTien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ViTien()
        {
            GiaoDiches = new HashSet<GiaoDich>();
        }

        [Key]
        public int IDViTien { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }

        public int SoluongXu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiaoDich> GiaoDiches { get; set; }

        public virtual User User { get; set; }
    }
}
