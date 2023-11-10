using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ThuongMaiDienTu.EF
{
    public partial class ThuongMaiDienTuDbConText : DbContext
    {
        public ThuongMaiDienTuDbConText()
            : base("name=ThuongMaiDienTuDbConText")
        {
        }

        public virtual DbSet<ChucNang> ChucNangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChucNang>()
                .Property(e => e.TenChucNang)
                .IsFixedLength();

            modelBuilder.Entity<ChucNang>()
                .Property(e => e.MaChucNang)
                .IsFixedLength();

            modelBuilder.Entity<ChucNang>()
                .HasMany(e => e.PhanQuyens)
                .WithRequired(e => e.ChucNang)
                .HasForeignKey(e => e.idChucNang);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.TenNhanVien)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.NgaySinh)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.DiaChi)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.ChucVu)
                .IsFixedLength();

            modelBuilder.Entity<PhanQuyen>()
                .Property(e => e.GhiChu)
                .IsFixedLength();
        }
    }
}
