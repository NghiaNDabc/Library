namespace QuanLySachThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietGioHang")]
    public partial class ChiTietGioHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string maGH { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string maSach { get; set; }

        public int? soLuong { get; set; }

        public virtual GioHang GioHang { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
