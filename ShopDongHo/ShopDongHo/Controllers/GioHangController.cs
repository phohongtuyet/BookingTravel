using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDongHo.Models;

namespace ShopDongHo.Controllers
{
    public class GioHangController: Controller
    {
        private ShopDongHoEntities db = new ShopDongHoEntities();
        // GET: GioHang
       
        public ActionResult Index()
        {        
            return View();
        }

        // GET: GioHang/ThemVaoGio/{maSP}
        public ActionResult ThemVaoGio(int maSP)
        {
            if (Session["cart"] == null)
            {
                var sp = db.DongHo.Find(maSP);
                List<SanPhamTrongGio> cart = new List<SanPhamTrongGio>();
                cart.Add(new SanPhamTrongGio { dongho = sp, soLuongTrongGio = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
                int index = isExist(maSP);
                if (index != -1)
                {
                    cart[index].soLuongTrongGio++;
                }
                else
                {
                    var sp = db.DongHo.Find(maSP);
                    cart.Add(new SanPhamTrongGio { dongho = sp, soLuongTrongGio = 1 });
                }
                Session["cart"] = cart;
            }

            return RedirectToAction("Index");
        }

        // GET: GioHang/CapNhatTang/{maSP}
        public ActionResult CapNhatTang(int maSP)
        {
            var sp = db.DongHo.Find(maSP);
            List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.dongho.ID == maSP && item.soLuongTrongGio <= 10)
                    if(item.soLuongTrongGio < sp.SoLuong)
                    {
                        item.soLuongTrongGio++;
                    }
                    else
                    {
                        TempData["Message"] = "Sản phẩm không đủ số lượng hiện có " + sp.SoLuong;
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
                if (item.dongho.ID == maSP && item.soLuongTrongGio >= 1)
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
                if (cart[i].dongho.ID.Equals(id))
                    return i;
            return -1;
        }
    }
}