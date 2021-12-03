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
    using System.Web;

    public partial class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            this.ChiTietDiaDiemThamQuan = new HashSet<ChiTietDiaDiemThamQuan>();
            this.ChiTietPhuongTien = new HashSet<ChiTietPhuongTien>();
            this.DatTour_ChiTiet = new HashSet<DatTour_ChiTiet>();
            this.HinhAnh = new HashSet<HinhAnh>();
            this.ChiTietDichVu = new HashSet<ChiTietDichVu>();
            this.BangPhanCong = new HashSet<BangPhanCong>();
        }

        [Display(Name = "Mã Tour ")]
        public int ID { get; set; }

        [Display(Name = "Tên Tour ")]
        [Required(ErrorMessage = "Tên Tour không được bỏ trống!")]
        public string TenTour { get; set; }

        [Display(Name = "Loại Tour")]
        [Required(ErrorMessage = "Chưa chọn Loại Tour!")]
        public Nullable<short> LoaiTour { get; set; }

        [Display(Name = "Nơi khởi hành")]
        [Required(ErrorMessage = "Chưa chọn nơi khởi hành!")]
        public Nullable<short> NoiKhoiHanh { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        [Required(ErrorMessage = "Chưa chọn ngày bắt đầu!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> NgayBD { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [Required(ErrorMessage = "Chưa chọn ngày kết thúc!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> NgayKT { get; set; }

        [Display(Name = "Đơn giá")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Đơn giá không được bỏ trống!")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng nhập số lớn hơn 1")]
        public Nullable<int> DonGia { get; set; }

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Số lượng không được bỏ trống!")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng nhập số lớn hơn 1")]
        public Nullable<int> SoLuong { get; set; }

        [Display(Name = "Hoạt động")]
        public Nullable<short> TrangThai { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string MoTa { get; set; }

        [Display(Name = "Hình ảnh ")]
        public IEnumerable<HttpPostedFileBase> DuLieuHinhAnh { get; set; }

        [Required(ErrorMessage = "Chưa chọn địa điểm tham quan!")]
        public List<int> selectedLocation { get; set; }

        [Required(ErrorMessage = "Chưa chọn phương tiện tham quan!")]
        public List<int> selectedTranpost { get; set; }

        [Display(Name = "Dịch vụ")]
        [Required(ErrorMessage = "Chưa chọn dịch vụ!")]
        public List<int> selectedServe { get; set; }

        [Display(Name = "Tỉnh")]
        //[Required(ErrorMessage = "Chưa chọn tỉnh!")]
        public List<int> Tinh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDiaDiemThamQuan> ChiTietDiaDiemThamQuan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhuongTien> ChiTietPhuongTien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatTour_ChiTiet> DatTour_ChiTiet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnh> HinhAnh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDichVu> ChiTietDichVu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BangPhanCong> BangPhanCong { get; set; }
    }
}
