using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDongHo.Models;
using ShopDongHo.Libs;
using System.Data.Entity;

namespace ShopDongHo.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
		private ShopDongHoEntities db = new ShopDongHoEntities();

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Unauthorized()
		{
			return View();
		}

		public ActionResult ChangePassword()
		{
			//ModelState.AddModelError("ErrorChangePassword", "");
			return View();

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ChangePassword([Bind(Include = "MatKhau,MatKhauMoi,XacNhanMatKhauMoi")] NhanVienChangePassword nhanVienChangePassword)
		{
			if (ModelState.IsValid)
			{
				int manv = Convert.ToInt32(Session["MaNhanVien"]);
				NhanVien nhanVien = db.NhanVien.Find(manv);
				if (nhanVien == null)
				{
					return HttpNotFound();
				}
				nhanVienChangePassword.MatKhau = SHA1.ComputeHash(nhanVienChangePassword.MatKhau);
				if (nhanVien.MatKhau == nhanVienChangePassword.MatKhau)
				{
					nhanVienChangePassword.MatKhauMoi = SHA1.ComputeHash(nhanVienChangePassword.MatKhauMoi);
					nhanVienChangePassword.XacNhanMatKhauMoi = nhanVienChangePassword.MatKhauMoi;

					nhanVien.MatKhau = nhanVienChangePassword.MatKhauMoi;
					nhanVien.XacNhanMatKhau = nhanVienChangePassword.MatKhauMoi;

					db.Entry(nhanVien).State = EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Logout");
				}
				else
				{
					ViewBag.error = "Mật khẩu cũ không đúng !!!";
					return View();
				}

			}
			return View(nhanVienChangePassword);
		}

		public ActionResult Logout()
		{
			// Xóa SESSION
			Session.RemoveAll();

			// Quay về trang chủ
			return RedirectToAction("Index", "Home");
		}

		// GET: Home/Login
		public ActionResult Login()
		{
			ModelState.AddModelError("LoginError", "");
			return View();
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