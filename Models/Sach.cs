namespace QuanLySachThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
            ChiTietPhieuMuons = new HashSet<ChiTietPhieuMuon>();
            ChiTietPhieuTras = new HashSet<ChiTietPhieuTra>();
        }

        [Key]
        [StringLength(10)]
        public string maSach { get; set; }

        [Required]
        [StringLength(10)]
        public string maDanhMuc { get; set; }

        [StringLength(50)]
        public string tenSach { get; set; }

        [StringLength(50)]
        public string hinhAnh { get; set; }

        public int? soTrang { get; set; }

        [StringLength(200)]
        public string moTa { get; set; }

        public int? soLuong { get; set; }

        public int? namXuatBan { get; set; }

        [StringLength(50)]
        public string nhaXuatBan { get; set; }

        public double? giaTien { get; set; }

        [StringLength(50)]
        public string tenTacGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuTra> ChiTietPhieuTras { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
