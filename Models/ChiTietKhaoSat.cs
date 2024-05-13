namespace QuanLySachThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietKhaoSat")]
    public partial class ChiTietKhaoSat
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string maNguoiDung { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string maKhaoSat { get; set; }

        [StringLength(255)]
        public string phanHoi { get; set; }

        public virtual KhaoSat KhaoSat { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
