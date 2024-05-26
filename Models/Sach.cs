namespace QuanLySachThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        [DisplayName("Mã sách")]
        public string maSach { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Mã danh mục")]
        public string maDanhMuc { get; set; }

        [StringLength(50)]
        [DisplayName("Tên sách")]
        public string tenSach { get; set; }

        [StringLength(50)]
        [DisplayName("Hình ảnh")]
        public string hinhAnh { get; set; }
        [DisplayName("Số trang")]
        public int? soTrang { get; set; }

        [StringLength(200)]
        [DisplayName("Mô tả")]
        public string moTa { get; set; }
        [DisplayName("Số lượng")]

        public int? soLuong { get; set; }
        [DisplayName("Năm xuất bản")]

        public int? namXuatBan { get; set; }

        [StringLength(50)]
        [DisplayName("Nhà xuất bản")]
        public string nhaXuatBan { get; set; }
        [DisplayName("Giá tiền")]

        public double? giaTien { get; set; }

        [StringLength(50)]
        [DisplayName("Tên tác giả")]
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
