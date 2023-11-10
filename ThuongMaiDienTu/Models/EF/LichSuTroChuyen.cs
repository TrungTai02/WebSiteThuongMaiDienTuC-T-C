namespace ThuongMaiDienTu.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuTroChuyen")]
    public partial class LichSuTroChuyen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LichSuTroChuyenID { get; set; }

        public int? UserID { get; set; }

        public int? ReceiverUserID { get; set; }

        public string Message { get; set; }

        public DateTime? Timestamp { get; set; }

        public int? TroChuyenID { get; set; }

        public virtual TroChuyen TroChuyen { get; set; }

        public virtual User User { get; set; }
    }
}
