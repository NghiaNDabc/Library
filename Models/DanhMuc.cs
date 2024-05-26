namespace QuanLySachThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMuc")]
    public partial class DanhMuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMuc()
        {
            Saches = new HashSet<Sach>();
        }

        [Key]
        [StringLength(10)]
        [DisplayName("Mã danh mục")]
        public string maDanhMuc { get; set; }

        [StringLength(50)]
        [DisplayName("Tên danh mục")]
        public string tenDanhMuc { get; set; }

        [StringLength(200)]
        [DisplayName("Mô tả")]
        public string moTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
