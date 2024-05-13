namespace QuanLySachThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuTra")]
    public partial class ChiTietPhieuTra
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string maPhieuTra { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string maSach { get; set; }

        public int? soLuong { get; set; }

        [StringLength(50)]
        public string tinhTrang { get; set; }

        public virtual PhieuTra PhieuTra { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
