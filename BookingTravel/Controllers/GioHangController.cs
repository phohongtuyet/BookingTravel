using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingTravel.Models;

namespace BookingTravel.Controllers
{
    public class GioHangController : Controller
    {
        private BookingTravelEntities db = new BookingTravelEntities();
        // GET: GioHang

        public ActionResult Index()
        {
            return View();
        }

        // GET: GioHang/ThemVaoGio/{maSP}
        public ActionResult ThemVaoGio(int maSP)
        {
            if (Session["cart"] == null) // nếu giỏ hàng rõng thì tạo mới giỏ hàng
            {
                var sp = db.Tour.Find(maSP);
                List<SanPhamTrongGio> cart = new List<SanPhamTrongGio>();
                cart.Add(new SanPhamTrongGio { tour = sp, soLuongTrongGio = 1 }); 
                Session["cart"] = cart;
            }
            else //đã có giỏ hàng
            {
                List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
                int index = isExist(maSP); // ktra sp có trong giỏ hàng
                if (index != -1)
                {
                    cart[index].soLuongTrongGio++;// có thì tăng số lượng
                }
                else// thêm sản phẩm  vào giỏ
                {
                    var sp = db.Tour.Find(maSP);
                    cart.Add(new SanPhamTrongGio { tour = sp, soLuongTrongGio = 1 });
                }
                Session["cart"] = cart;
            }

            return RedirectToAction("Index");
        }

        // GET: GioHang/CapNhatTang/{maSP}
        public ActionResult CapNhatTang(int maSP)
        {
            var sp = db.Tour.Find(maSP);
            List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.tour.ID == maSP)
                    if (item.soLuongTrongGio < sp.SoLuong) // số lượng đặt nhỏ hơn số lượng hiện có
                    {
                        item.soLuongTrongGio++;// tăng số lượng
                    }
                    else// số lượng đặt lớn hơn số lượng hiện có thì thông báo
                    {
                        TempData["Message"] = "Tour không đủ!!! số lượng hiện có " + sp.SoLuong;
                    }

            }
            Session["cart"] = cart;

            return RedirectToAction("Index");
        }

        // GET: GioHang/CapNhatGiam/{maSP}
        public ActionResult CapNhatGiam(int maSP)
        {
            List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.tour.ID == maSP && item.soLuongTrongGio > 1) // so luong >= 1 thì giảm
                    item.soLuongTrongGio--;
            }
            Session["cart"] = cart;

            return RedirectToAction("Index");
        }

        // GET: GioHang/XoaKhoiGio/{maSP}
        public ActionResult XoaKhoiGio(int maSP)
        {
            List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
            int index = isExist(maSP);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].tour.ID.Equals(id))
                    return i;
            return -1;
        }
    }
}