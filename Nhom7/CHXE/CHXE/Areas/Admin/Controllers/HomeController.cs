using CHXE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHXE.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
		private CHXEEntities db = new CHXEEntities();

		public ActionResult Index()
		{
			return View();
		}

		// GET: Home/Unauthorized
		public ActionResult Unauthorized()
		{
			return View();
		}

		// GET: NhanVien/Logout
		public ActionResult Logout()
		{
			// Xóa SESSION
			Session.RemoveAll();

			// Quay về trang chủ
			return RedirectToAction("Index", "Home");
		}

		public ActionResult BanChayNhat()
		{
			List<ChiTietDonHang> dh = db.ChiTietDonHang.ToList();
			return View(dh);
		}


		public ActionResult DoiMatKhau([Bind(Include = "MatKhau,MatKhauMoi,XacNhanMatKhau")] DoiMatKhauModel doiMatKhauModel)
		{
			if (ModelState.IsValid)
			{
				int manv = Convert.ToInt32(Session["MaNhanVien"]);
				NhanVien nhanVien = db.NhanVien.Find(manv);
				if (nhanVien == null)
				{
					return HttpNotFound();
				}
				doiMatKhauModel.MatKhau = Libs.SHA1.ComputeHash(doiMatKhauModel.MatKhau);
				if (nhanVien.MatKhau != doiMatKhauModel.MatKhau)
				{
					ModelState.AddModelError("MatKhau", "Mật khẩu cũ không chính xác!");
					return View(doiMatKhauModel);
				}
				doiMatKhauModel.MatKhauMoi = Libs.SHA1.ComputeHash(doiMatKhauModel.MatKhauMoi);
				doiMatKhauModel.XacNhanMatKhau = doiMatKhauModel.MatKhauMoi;
				nhanVien.MatKhau = doiMatKhauModel.MatKhauMoi;
				nhanVien.XacNhanMatKhau = doiMatKhauModel.MatKhauMoi;
				db.Entry(nhanVien).State = EntityState.Modified;
				db.SaveChanges();
				SetAlert("Đổi Mật Khẩu Thành Công", "success");
				return RedirectToAction("Index", "Home");
			}
			return View(doiMatKhauModel);
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
		public ActionResult Login(NhanVienLogin nhanVien)
		{
			if (ModelState.IsValid)
			{
				string matKhauMaHoa = Libs.SHA1.ComputeHash(nhanVien.MatKhau);
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
					Session["HoTenNhanVien"] = taiKhoan.HovaTen;
					Session["Quyen"] = taiKhoan.Quyen;
					SetAlert("Đăng Nhập Thành Công", "success");
					// Quay về trang chủ
					return RedirectToAction("Index", "Home");
				}
			}

			return View(nhanVien);
		}


		protected void SetAlert(string message, string type)
		{
			TempData["AlertMessage"] = message;
			if (type == "success")
			{
				TempData["AlertType"] = "alert-success";
			}
			else if (type == "warning")
			{
				TempData["AlertType"] = "alert-warning";
			}
			else if (type == "error")
			{
				TempData["AlertType"] = "alert-error";
			}
		}
	}
}