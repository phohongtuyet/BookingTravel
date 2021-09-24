using BookingTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BookingTravel.Libs;

namespace BookingTravel.Areas.Admin.Controllers
{
	public class HomeController : Controller
	{
		private BookingTravelEntities db = new BookingTravelEntities();

		public ActionResult Index()
		{
			return View();
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

					// Quay về trang chủ
					return RedirectToAction("Index", "Home");
				}
			}

			return View(nhanVien);
		}

	}
}