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

    public partial class ChiTietPhuongTien
    {
        public int ID { get; set; }
        public Nullable<int> Tour_ID { get; set; }

        [Display(Name = "Phương tiện ")]
        [Required(ErrorMessage = "Chưa chọn phương tiện!")]
        public Nullable<int> PhuongTien_ID { get; set; }
    
        public virtual PhuongTien PhuongTien { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
