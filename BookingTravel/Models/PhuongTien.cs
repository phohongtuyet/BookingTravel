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

    public partial class PhuongTien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhuongTien()
        {
            this.Tour = new HashSet<Tour>();
        }
        [Display(Name = "Mã phương tiện")]
        public int ID { get; set; }

        [Display(Name = "Tên phương tiện")]
        [Required(ErrorMessage = "Tên phương tiện không được bỏ trống!")]
        public string TenPhuongTien { get; set; }

        [Display(Name = "Loại phương tiện")]
        [Required(ErrorMessage = "Chưa chọn loại phương tiện!")]
        public Nullable<short> LoaiPhuongTien { get; set; }

        [Display(Name = "Số chỗ")]
        [Required(ErrorMessage = "Số chỗ không được bỏ trống!")]
        public Nullable<int> SoCho { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tour { get; set; }
    }
}
