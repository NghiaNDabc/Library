namespace QuanLySachThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuTra")]
    public partial class PhieuTra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuTra()
        {
            ChiTietPhieuTras = new HashSet<ChiTietPhieuTra>();
        }

        [Key]
        [StringLength(10)]
        public string maPhieuTra { get; set; }

        [Required]
        [MaxLength(10)]
        public byte[] maPhieuMuon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuTra> ChiTietPhieuTras { get; set; }

        public virtual PhieuMuon PhieuMuon { get; set; }
    }
}
