//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookingTravel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class DiaDiemThamQuan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiaDiemThamQuan()
        {
            this.ChiTietDiaDiemThamQuan = new HashSet<ChiTietDiaDiemThamQuan>();
        }

        [Display(Name = "Mã địa danh")]
        public int ID { get; set; }

        [Display(Name = "Tên địa danh")]
        [Required(ErrorMessage = "Tên địa danh không được bỏ trống!")]
        public string TenDiaDanh { get; set; }


        [Display(Name = "Tỉnh")]
        [Required(ErrorMessage = "Chưa chọn tỉnh!")]
        public Nullable<short> Tinh { get; set; }


        [Display(Name = "Khách sạn")]
        [Required(ErrorMessage = "Chưa chọn kshách sạn!")]
        public Nullable<int> KhachSan_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDiaDiemThamQuan> ChiTietDiaDiemThamQuan { get; set; }
        public virtual KhachSan KhachSan { get; set; }
    }
}
