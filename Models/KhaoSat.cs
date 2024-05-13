namespace QuanLySachThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhaoSat")]
    public partial class KhaoSat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhaoSat()
        {
            ChiTietKhaoSats = new HashSet<ChiTietKhaoSat>();
        }

        [Key]
        [StringLength(10)]
        public string maKhaoSat { get; set; }

       [StringLength(50)]
        public string noiDung { get; set; }

        public DateTime? ngayBatDau { get; set; }

        public DateTime? ngayKetThuc { get; set; }

        [Required]
        [StringLength(50)]
        public string tenKhaoSat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietKhaoSat> ChiTietKhaoSats { get; set; }
    }
}
