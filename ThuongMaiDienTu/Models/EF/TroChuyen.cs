namespace ThuongMaiDienTu.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TroChuyen")]
    public partial class TroChuyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TroChuyen()
        {
            LichSuTroChuyens = new HashSet<LichSuTroChuyen>();
        }

        [Key]
        public int IDHoiThoai { get; set; }

        public int? User1ID { get; set; }

        public int? User2ID { get; set; }

        [ForeignKey("User1ID")]
        public virtual User USer1 { get; set; }

        [ForeignKey("User2ID")]
        public virtual User USer2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuTroChuyen> LichSuTroChuyens { get; set; }
    }
}
