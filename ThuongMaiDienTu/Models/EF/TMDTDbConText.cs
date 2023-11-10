using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ThuongMaiDienTu.Models.EF
{
    public partial class TMDTDbConText : DbContext
    {
        public TMDTDbConText()
            : base("name=TMDTDbConText")
        {
        }

        public virtual DbSet<BaiDang> BaiDangs { get; set; }
        public virtual DbSet<ChucNang> ChucNangs { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<GiaoDich> GiaoDiches { get; set; }
        public virtual DbSet<HinhAnhBaiDang> HinhAnhBaiDangs { get; set; }
        public virtual DbSet<LichSuTroChuyen> LichSuTroChuyens { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }
        public virtual DbSet<TroChuyen> Hoithoai { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ViTien> ViTiens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiDang>()
                .HasMany(e => e.HinhAnhBaiDangs)
                .WithOptional(e => e.BaiDang)
                .HasForeignKey(e => e.IDBaiDang);

            modelBuilder.Entity<BaiDang>()
                .HasMany(e => e.HinhAnhBaiDangs1)
                .WithOptional(e => e.BaiDang1)
                .HasForeignKey(e => e.IDBaiDang)
                .WillCascadeOnDelete();

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

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.BaiDangs)
                .WithRequired(e => e.DanhMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhanQuyen>()
                .Property(e => e.GhiChu)
                .IsFixedLength();

            modelBuilder.Entity<TrangThai>()
                .HasMany(e => e.BaiDangs)
                .WithOptional(e => e.TrangThai1)
                .HasForeignKey(e => e.IDKiemDuyet);

            modelBuilder.Entity<TroChuyen>()
                .HasMany(e => e.LichSuTroChuyens)
                .WithOptional(e => e.TroChuyen)
                .HasForeignKey(e => e.TroChuyenID);

            modelBuilder.Entity<User>()
                .Property(e => e.HoVaTen)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.NgaySinh)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.DiaChi)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.PhanQuyens)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.idNhanVien);
        }
    }
}
