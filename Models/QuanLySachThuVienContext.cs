using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLySachThuVien.Models
{
    public partial class QuanLySachThuVienContext : DbContext
    {
        public QuanLySachThuVienContext()
            : base("name=QuanLySachThuVienContext")
        {
        }

        public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public virtual DbSet<ChiTietKhaoSat> ChiTietKhaoSats { get; set; }
        public virtual DbSet<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }
        public virtual DbSet<ChiTietPhieuTra> ChiTietPhieuTras { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<KhaoSat> KhaoSats { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }
        public virtual DbSet<PhieuTra> PhieuTras { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietGioHang>()
                .Property(e => e.maGH)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietGioHang>()
                .Property(e => e.maSach)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietKhaoSat>()
                .Property(e => e.maNguoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietKhaoSat>()
                .Property(e => e.maKhaoSat)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietKhaoSat>()
                .Property(e => e.phanHoi)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuMuon>()
                .Property(e => e.maSach)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuTra>()
                .Property(e => e.maPhieuTra)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuTra>()
                .Property(e => e.maSach)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .Property(e => e.maDanhMuc)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .Property(e => e.moTa)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.Saches)
                .WithRequired(e => e.DanhMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GioHang>()
                .Property(e => e.maGH)
                .IsUnicode(false);

            modelBuilder.Entity<GioHang>()
                .Property(e => e.maNguoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<GioHang>()
                .HasMany(e => e.ChiTietGioHangs)
                .WithRequired(e => e.GioHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhaoSat>()
                .Property(e => e.maKhaoSat)
                .IsUnicode(false);

            modelBuilder.Entity<KhaoSat>()
                .Property(e => e.noiDung)
                .IsUnicode(false);

            modelBuilder.Entity<KhaoSat>()
                .HasMany(e => e.ChiTietKhaoSats)
                .WithRequired(e => e.KhaoSat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.maNguoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.ChiTietKhaoSats)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.GioHangs)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.PhieuMuons)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.TaiKhoans)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuMuon>()
                .Property(e => e.maNguoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuMuon>()
                .HasMany(e => e.ChiTietPhieuMuons)
                .WithRequired(e => e.PhieuMuon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuMuon>()
                .HasMany(e => e.PhieuTras)
                .WithRequired(e => e.PhieuMuon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuTra>()
                .Property(e => e.maPhieuTra)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuTra>()
                .HasMany(e => e.ChiTietPhieuTras)
                .WithRequired(e => e.PhieuTra)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.maSach)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.maDanhMuc)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.tenSach)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.moTa)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ChiTietGioHangs)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ChiTietPhieuMuons)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ChiTietPhieuTras)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.tenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.matKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.maNguoiDung)
                .IsUnicode(false);
        }
    }
}
