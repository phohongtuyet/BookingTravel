using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHXE.Models
{
    public class MuaNhieuNhatModels
    {
        public int ID { get; set; }
        public string TenXe { get; set; }
        public string HinhAnhBia { get; set; }
        public HttpPostedFileBase DuLieuHinhAnhBia { get; set; }
        public Nullable<long> DonGia { get; set; }
        public Nullable<short> SoLuong { get; set; }

    }
    public class Myorders
    {

        public int ID { get; set; }
        public string TenXe { get; set; }
        public string HinhAnhBia { get; set; }
        public HttpPostedFileBase DuLieuHinhAnhBia { get; set; }
        public Nullable<long> DonGia { get; set; }
        public Nullable<short> SoLuong { get; set; }
        public Nullable<System.DateTime> NgayDatHang { get; set; }

    }
}