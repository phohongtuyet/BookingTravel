﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingTravel.Models;
using BookingTravel.Libs;

namespace BookingTravel.Controllers
{
    public class HomeController : Controller
    {
        BookingTravelEntities db = new BookingTravelEntities();

        public ActionResult Index()
        {
            var tour = db.Tour.Include(t => t.ChiTietPhuongTien).Include(h => h.HinhAnh).Include(ct => ct.ChiTietDiaDiemThamQuan).Where(r => r.SoLuong > 0).ToList();
            return View(tour);
        }
        public ActionResult Details_Tour(int id)
        {
            var tour = db.Tour.Include(t => t.ChiTietPhuongTien).Include(h => h.HinhAnh).Include(ct => ct.ChiTietDiaDiemThamQuan).Where(r => r.ID == id).SingleOrDefault();
            return View(tour);
        }

        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {
            string diemden = collection["DiemDen"].ToString();
            //short tinh = collection["Tinh"];
            DateTime ngay = Convert.ToDateTime( collection["Ngay"].ToString());
            int noikhoihanh = Convert.ToInt32( collection["NoiKhoiHanh"]);

            var tour = db.Tour.Include(d => d.ChiTietDiaDiemThamQuan)
                                        .Where( r => r.NoiKhoiHanh ==  noikhoihanh && r.NgayBD >= ngay.Date && r.TenTour.Contains(diemden) && r.MoTa.Contains(diemden)
                                            || r.TenTour.Contains(diemden) && r.NgayBD >= ngay.Date && r.MoTa.Contains(diemden)
                                            || r.TenTour.Contains(diemden) && r.NoiKhoiHanh == noikhoihanh && r.MoTa.Contains(diemden)
                                            || r.MoTa.Contains(diemden)).ToList();             
            return View(tour);
        }

        public ActionResult success()
        {
            return View();
        }

        public ActionResult BestSale()
        {
            var tour = db.Tour.Include(t => t.ChiTietPhuongTien)
                                .Include(h => h.HinhAnh)
                                .Include(ct => ct.ChiTietDiaDiemThamQuan)
                                .Where(ct => ct.SoLuong > 0).ToList();


            return View(tour);



            /*  var bestSale = (from dh in db.Tour
                                join ct in db.DatTour_ChiTiet on dh.ID equals ct.Tour_ID
                                join ctdd in db.ChiTietDiaDiemThamQuan on dh.ID equals ctdd.ID
                                join dd in db.DiaDiemThamQuan on dh.ID equals dd.ID
                                join ctpt in db.ChiTietPhuongTien on dh.ID equals ctpt.ID
                                join pt in db.PhuongTien on dh.ID equals pt.ID
                                join ha in db.HinhAnh on dh.ID equals ha.ID
                                join dhang in db.DatTour on ct.DatTour_ID equals dhang.ID
                                where (dh.SoLuong > 0)
                                select new BestSaleModels()
                                {

                                    TenTour = dh.TenTour,
                                    HinhAnh1 = ha.HinhAnh1,
                                    Tinh = dd.Tinh,
                                    LoaiPhuongTien = pt.LoaiPhuongTien,
                                    TenDiaDanh = dd.TenDiaDanh,                             
                                    DonGia = dh.DonGia,
                                    ID = dh.ID,

                                    SoLuong = ct.SoLuong

                                }).OrderByDescending(ct => ct.SoLuong).Distinct();
                  return View(bestSale);*/

        }

        public ActionResult MyOrders()
        {
            /* int makh = Convert.ToInt32(Session["MaKhachHang"]);
             var tour = db.Tour.Include(h => h.HinhAnh)
                                .Include(ct => ct.DatTour_ChiTiet)
                                .Where(ct => ct.SoLuong > 0).ToList();


             return View(tour);*/

            int makh = Convert.ToInt32(Session["MaKhachHang"]);
            var Myorders = (from dh in db.Tour
                            join ct in db.DatTour_ChiTiet on dh.ID equals ct.Tour_ID
                            join ha in db.HinhAnh on dh.ID equals ha.Tour_ID
                            join dhang in db.DatTour on ct.DatTour_ID equals dhang.ID
                            join kh in db.KhachHang on dhang.KhachHang_ID equals kh.ID
                            where (kh.ID == makh)

                            select new Myorders()
                            {
                                TenTour = dh.TenTour,
                                //  HinhAnh1 = ha.HinhAnh1,
                                DonGia = ct.DonGia,
                                ID = kh.ID,
                                SoLuong = ct.SoLuong,
                                NgayDatHang = dhang.NgayDatHang

                            }).OrderByDescending(dhang => dhang.NgayDatHang).ToList();

            return View(Myorders);
        }

        public ActionResult Review()
        {
            var baiViet = db.BaiViet.Where(r => r.KiemDuyet == 1).ToList();
            return View(baiViet);
        }

        public ActionResult Detail(int id)
        {
            // Lấy thông tin bài viết
            var baiViet = db.BaiViet.Where(r => r.KiemDuyet == 1 && r.ID == id).SingleOrDefault();

            // Tăng lượt xem
            // Chính sách: Mỗi máy chỉ tăng 1 lượt xem
            if (Object.Equals(Session["DaXem" + baiViet.ID], null))
            {
                BaiViet bv = db.BaiViet.Find(id);
                bv.LuotXem = bv.LuotXem + 1;
                db.Entry(bv).State = EntityState.Modified;
                db.SaveChanges();

                // Đánh dấu là đã xem
                Session["DaXem" + bv.ID] = 1;
            }

            // Hiển thị bài viết ra View
            return View(baiViet);
        }

        // GET: Home/Login
        public ActionResult Login()
        {
            ModelState.AddModelError("LoginError", "");
            return View();
        }

        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHangLogin khachHang)
        {
            if (ModelState.IsValid)
            {
                string matKhauMaHoa = SHA1.ComputeHash(khachHang.MatKhau);
                var taiKhoan = db.KhachHang.Where(r => r.TenDangNhap == khachHang.TenDangNhap && r.MatKhau == matKhauMaHoa).SingleOrDefault();

                if (taiKhoan == null)
                {
                    ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
                    return View(khachHang);
                }
                else
                {
                    // Đăng ký SESSION
                    Session["MaKhachHang"] = taiKhoan.ID;
                    Session["HoTenKhachHang"] = taiKhoan.HoVaten;

                    // Quay về trang chủ
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(khachHang);
        }

        //GET: Register
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                var check = db.KhachHang.FirstOrDefault(r => r.TenDangNhap == khachHang.TenDangNhap);
                if (check == null)
                {
                    khachHang.MatKhau = SHA1.ComputeHash(khachHang.MatKhau);
                    khachHang.XacNhanMatKhau = SHA1.ComputeHash(khachHang.XacNhanMatKhau);

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KhachHang.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("success");
                }
                else
                {
                    ViewBag.error = "Tên đăng nhập đã tồn tại !!!";
                    return View();
                }
            }
            return View();
        }


        public ActionResult Logout()
        {
            // Xóa SESSION
            Session.RemoveAll();

            // Quay về trang chủ
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            //ModelState.AddModelError("ErrorChangePassword", "");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "MatKhau,MatKhauMoi,XacNhanMatKhauMoi")] KhachHangChangePassword khachHangChangePassword)
        {
            if (ModelState.IsValid)
            {
                int makh = Convert.ToInt32(Session["MaKhachHang"]);
                KhachHang khachHang = db.KhachHang.Find(makh);
                if (khachHang == null)
                {
                    return HttpNotFound();
                }
                khachHangChangePassword.MatKhau = SHA1.ComputeHash(khachHangChangePassword.MatKhau);
                if (khachHang.MatKhau == khachHangChangePassword.MatKhau)
                {
                    khachHangChangePassword.MatKhauMoi = SHA1.ComputeHash(khachHangChangePassword.MatKhauMoi);
                    khachHangChangePassword.XacNhanMatKhauMoi = khachHangChangePassword.MatKhauMoi;

                    khachHang.MatKhau = khachHangChangePassword.MatKhauMoi;
                    khachHang.XacNhanMatKhau = khachHangChangePassword.MatKhauMoi;

                    db.Entry(khachHang).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Mật khẩu cũ không đúng !!!";
                    return View();
                }


            }
            return View(khachHangChangePassword);
        }

        // GET: Home/ThanhToan
        public ActionResult ThanhToan()
        {
            if (Session["MaKhachHang"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        // POST: Home/ThanhToan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThanhToan(DatTour datHang)
        {
            if (ModelState.IsValid)
            {
                // Lưu vào bảng DatHang
                DatTour dh = new DatTour();
                dh.HoVaTen = datHang.HoVaTen;
                dh.DienThoaiDatTour = datHang.DienThoaiDatTour;
                dh.NgayDatHang = DateTime.Now;
                dh.KhachHang_ID = Convert.ToInt32(Session["MaKhachHang"]);
                dh.TinhTrang = 0;
                db.DatTour.Add(dh);
                db.SaveChanges();

                // Lưu vào bảng DatHang_ChiTiet
                List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
                foreach (var item in cart)
                {
                    DatTour_ChiTiet ct = new DatTour_ChiTiet();
                    ct.DatTour_ID = dh.ID;
                    ct.Tour_ID = item.tour.ID;
                    ct.SoLuong = Convert.ToInt16(item.soLuongTrongGio);
                    ct.DonGia = item.tour.DonGia;
                    db.DatTour_ChiTiet.Add(ct);
                    var dongho = db.Tour.Find(item.tour.ID);
                    dongho.SoLuong -= item.soLuongTrongGio;
                    db.SaveChanges();
                }

                // Xóa giỏ hàng
                cart.Clear();

                // Quay về trang chủ
                return RedirectToAction("Index", "Home");
            }

            return View(datHang);
        }

    }
}