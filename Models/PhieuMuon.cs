namespace QuanLySachThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuMuon")]
    public partial class PhieuMuon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuMuon()
        {
            ChiTietPhieuMuons = new HashSet<ChiTietPhieuMuon>();
            PhieuTras = new HashSet<PhieuTra>();
        }

        [Key]
        [MaxLength(10)]
        public byte[] maPhieuMuon { get; set; }

        [Required]
        [StringLength(10)]
        public string maNguoiDung { get; set; }

        public DateTime? ngayMuon { get; set; }

        public DateTime? ngayTra { get; set; }

        [StringLength(50)]
        public string tinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuTra> PhieuTras { get; set; }
    }
}
