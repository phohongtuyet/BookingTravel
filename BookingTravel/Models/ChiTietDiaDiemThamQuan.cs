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

    public partial class ChiTietDiaDiemThamQuan
    {
        public int ID { get; set; }
        public Nullable<int> Tour_ID { get; set; }

        [Display(Name = "Địa danh ")]
        [Required(ErrorMessage = "Chưa chọn địa danh!")]
        public Nullable<int> DiaDiemThamQuan_ID { get; set; }
        public List<int> selectedLocation { get; set; }
        public virtual DiaDiemThamQuan DiaDiemThamQuan { get; set; }
        public virtual Tour Tour { get; set; }
    }
}