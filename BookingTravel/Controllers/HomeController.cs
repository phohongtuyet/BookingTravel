using System;
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

        public ActionResult success()
        {
            return View();
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
    }
}