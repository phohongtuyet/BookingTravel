using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDongHo.Models
{
    public class KhachHangEdit
    {
        public KhachHangEdit()
        {
        }

        public KhachHangEdit(KhachHang n)
        {
            ID = n.ID;
            HoVaten = n.HoVaten;
            DienThoai = n.DienThoai;
            DiaChi = n.DiaChi;
            TenDangNhap = n.TenDangNhap;
            MatKhau = n.MatKhau;
            XacNhanMatKhau = n.XacNhanMatKhau;
        }

        [Display(Name = "Mã KH")]
        public int ID { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được bỏ trống!")]
        public string HoVaten { get; set; }

        [Display(Name = "Điện thoại")]
        [MaxLength(10, ErrorMessage = "Số điện thoại đa 10 kí tự")]
        [RegularExpression(@"^((09|03|07|08|05)\d{8})$", ErrorMessage = "Số điện thoại không đúng")]
        public string DienThoai { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống!")]
        public string DiaChi { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessage = "Mật khẩu tối đa 100 kí tự")]
        [MinLength(3, ErrorMessage = "Mật khẩu tối thiểu 3 kí tự")]
        public string MatKhau { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
    }

    public class KhachHangLogin
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }

    public class KhachHangChangePassword
    {
        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Mật khẩu cũ không được bỏ trống!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessage = "Mật khẩu tối đa 100 kí tự")]
        [MinLength(3, ErrorMessage = "Mật khẩu tối thiểu 3 kí tự")]
        public string MatKhauMoi { get; set; }

        [Display(Name = "Xác nhận mật khẩu mới")]
        [Required(ErrorMessage = "Xác nhận khẩu không được bỏ trống!")]
        [DataType(DataType.Password)]
        [Compare("MatKhauMoi", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        public string XacNhanMatKhauMoi { get; set; }
     
    }

}