using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BookingTravel.Models
{
    public class BestSaleModels
    {
        public BestSaleModels()
        {
            this.ChiTietDiaDiemThamQuan = new HashSet<ChiTietDiaDiemThamQuan>();
            this.ChiTietPhuongTien = new HashSet<ChiTietPhuongTien>();
            this.DatTour_ChiTiet = new HashSet<DatTour_ChiTiet>();
            this.HinhAnh = new HashSet<HinhAnh>();
            this.ChiTietDichVu = new HashSet<ChiTietDichVu>();
        }

        public int ID { get; set; }
        public Nullable<int> LoaiPhuongTien { get; set; }
        public Nullable<int> Tinh { get; set; }
        // public string TenDiaDanh { get; set; }
        public string TenTour { get; set; }
        public Nullable<int> DonGia { get; set; }
        // public Nullable<int> SoLuong { get; set; }
        public Nullable<short> SoLuong { get; set; }
        public string HinhAnh1 { get; set; }
        //  public int Tinh { get; set; }
        //  public Nullable<short> LoaiPhuongTien { get; set; }
        public string TenDiaDanh { get; set; }
        [Display(Name = "Hình ảnh ")]
        public HttpPostedFileBase DuLieuHinhAnh { get; set; }



        public List<int> selectedLocation { get; set; }
        public List<int> selectedTranpost { get; set; }



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
    }


    public class Myorders
    {
        public Myorders()
        {

            this.HinhAnh = new HashSet<HinhAnh>();

        }

        public int ID { get; set; }
        public string TenTour { get; set; }
        public string HinhAnh1 { get; set; }
        public HttpPostedFileBase DuLieuHinhAnhDH { get; set; }
        public Nullable<int> DonGia { get; set; }
        public Nullable<short> SoLuong { get; set; }
        public Nullable<System.DateTime> NgayDatHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnh> HinhAnh { get; set; }
    }

    public class DoanhThuTour
    {

        public Nullable<int> Tour_ID { get; set; }
        public int ID { get; set; }
        public string TenTour { get; set; }
        public Nullable<short> SoLuong { get; set; }
        public Nullable<int> DonGia { get; set; }
        public decimal thanhtien { get; set; }



    }


}