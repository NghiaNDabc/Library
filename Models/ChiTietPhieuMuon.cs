namespace QuanLySachThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuMuon")]
    public partial class ChiTietPhieuMuon
    {
        [Key]
        [Column(Order = 0)]
        [MaxLength(10)]
        public byte[] maPhieuMuon { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string maSach { get; set; }

        public int? soLuong { get; set; }

        public virtual PhieuMuon PhieuMuon { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
