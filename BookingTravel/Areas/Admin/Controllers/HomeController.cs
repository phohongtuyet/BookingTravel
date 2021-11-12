using BookingTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BookingTravel.Libs;
using System.Data.Entity;

namespace BookingTravel.Areas.Admin.Controllers
{
	public class HomeController : Controller
	{
		private BookingTravelEntities db = new BookingTravelEntities();

		public ActionResult Index()
		{
			return View();
		}

		

		public ActionResult Tour()
		{
			var tour = db.Tour.Include(t => t.ChiTietPhuongTien).Include(h => h.HinhAnh).Include(ct => ct.ChiTietDiaDiemThamQuan).Where(r => r.SoLuong > 0).ToList();
			return View(tour);
		}
		public ActionResult Unauthorized()
		{
			return View();
		}
		// GET: Home/Login
		public ActionResult Login()
		{
			ModelState.AddModelError("LoginError", "");
			return View();
		}

		public ActionResult Logout()
		{
			// Xóa SESSION
			Session.RemoveAll();

			// Quay về trang chủ
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(NhanVienLogin nhanVien)
		{
			if (ModelState.IsValid)
			{
				string matKhauMaHoa = SHA1.ComputeHash(nhanVien.MatKhau);
				var taiKhoan = db.NhanVien.Where(r => r.TenDangNhap == nhanVien.TenDangNhap && r.MatKhau == matKhauMaHoa).SingleOrDefault();

				if (taiKhoan == null)
				{
					ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
					return View(nhanVien);
				}
				else
				{
					// Đăng ký SESSION
					Session["MaNhanVien"] = taiKhoan.ID;
					Session["HoTenNhanVien"] = taiKhoan.HoVaTen;
					Session["Quyen"] = taiKhoan.Quyen;
					Session["DiaChi"] = taiKhoan.DiaChi;
					Session["DienThoai"] = taiKhoan.DienThoai;
					// Quay về trang chủ
					return RedirectToAction("Index", "Home");
				}
			}

			return View(nhanVien);
		}

		/// GET: Home/ThanhToan
		public ActionResult ThanhToan()
		{

			return View();

		}

		// POST: Home/ThanhToan
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ThanhToan(DatTour datHang)
		{
			if (ModelState.IsValid)
			{

				// Lưu vào bảng khachHang
				KhachHang kh = new KhachHang();
				kh.HoVaten = datHang.HoVaTen;
				kh.DienThoai = datHang.DienThoaiDatTour;
				kh.DiaChi = datHang.DiaChi;
				db.KhachHang.Add(kh);
				db.SaveChanges();


				// Lưu vào bảng DatHang
				DatTour dh = new DatTour();
				dh.DienThoaiDatTour = datHang.DienThoaiDatTour;
				dh.NgayDatHang = DateTime.Now;
				dh.KhachHang_ID = kh.ID;
				dh.NhanVien_ID = Convert.ToInt32(Session["MaNhanVien"]);
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