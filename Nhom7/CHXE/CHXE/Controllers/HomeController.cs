using CHXE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHXE.Controllers
{
    public class HomeController : Controller
    {
		private CHXEEntities db = new CHXEEntities();
		public ActionResult Index()
		{
			var xe = db.Xe.Where(r => r.SoLuong > 0).ToList();
			return View(xe);
		}

		public ActionResult _PhanThuongHieu()
        {
			var thuongHieu = db.ThuongHieu.OrderBy(r => r.TenThuongHieu).ToList();
			return PartialView(thuongHieu);
		}

		public ActionResult XeTheoThuongHieu(int id)
		{
			var xe = db.Xe.Where(r => r.SoLuong > 0 && r.ThuongHieu_ID == id).ToList();
			return View(xe);
		}

		public ActionResult _PhanKieuDang()
		{
			var loaiXe = db.LoaiXe.OrderBy(r => r.TenLoai).ToList();
			return PartialView(loaiXe);
		}

		public ActionResult XeTheoKieuDang(int id)
		{
			var xe = db.Xe.Where(r => r.SoLuong > 0 && r.LoaiXe_ID == id).ToList();
			return View(xe);
		}


		public ActionResult _PhanNoiSanXuat()
		{
			var nsx = db.NoiSanXuat.OrderBy(r => r.TenNoiSX).ToList();
			return PartialView(nsx);
		}

		public ActionResult XeTheoNoiSanXuat(int id)
		{
			var xe = db.Xe.Where(r => r.SoLuong > 0 && r.NoiSanXuat_ID == id).ToList();
			return View(xe);
		}

		[HttpPost]
		public ActionResult Search(FormCollection collection)
		{
			string tukhoa = collection["TuKhoa"].ToString();
			var xe = db.Xe.Where(r => r.SoLuong > 0 && r.TenXe.Contains(tukhoa) ).ToList();
			return View(xe);
		}
		public ActionResult DoiMatKhau([Bind(Include = "MatKhau,MatKhauMoi,XacNhanMatKhau")] DoiMatKhauKHModel doiMatKhauModel)
		{
			if (ModelState.IsValid)
			{
				int makh = Convert.ToInt32(Session["MaKhachHang"]);
				KhachHang khachHang = db.KhachHang.Find(makh);
				if (khachHang == null)
				{
					return HttpNotFound();
				}
				doiMatKhauModel.MatKhau = Libs.SHA1.ComputeHash(doiMatKhauModel.MatKhau);
				if (khachHang.MatKhau != doiMatKhauModel.MatKhau)
				{
					ModelState.AddModelError("MatKhau", "Mật khẩu cũ không chính xác!");
					return View(doiMatKhauModel);
				}
				doiMatKhauModel.MatKhauMoi = Libs.SHA1.ComputeHash(doiMatKhauModel.MatKhauMoi);
				doiMatKhauModel.XacNhanMatKhau = doiMatKhauModel.MatKhauMoi;
				khachHang.MatKhau = doiMatKhauModel.MatKhauMoi;
				khachHang.XacNhanMatKhau = doiMatKhauModel.MatKhauMoi;
				db.Entry(khachHang).State = EntityState.Modified;
				db.SaveChanges();
				SetAlert("Đổi Mật Khẩu Thành Công", "success");
				return RedirectToAction("Index", "Home");
			}
			return View(doiMatKhauModel);
		}

		public ActionResult ChiTiet(int maSP)
		{
			var xe = db.Xe.Where(r => r.ID == maSP).SingleOrDefault();
			return View(xe);
		}

		public ActionResult MuaNhieuNhat()
		{
			
			var muanhieu = (from dh in db.Xe
							join ct in db.ChiTietDonHang on dh.ID equals ct.Xe_ID
							where (ct.SoLuong > 0)
							select new MuaNhieuNhatModels()
							{
								TenXe = dh.TenXe,
								HinhAnhBia = dh.HinhAnhBia,
								DonGia =dh.DonGia,
								ID = dh.ID,
								SoLuong = ct.SoLuong
							}).Distinct().ToList();

			return View(muanhieu);
		}
		public ActionResult MyOrders()
		{
			int makh = Convert.ToInt32(Session["MaKhachHang"]);
			var Myorders = (from dh in db.Xe
							join ct in db.ChiTietDonHang on dh.ID equals ct.Xe_ID
							join dhang in db.DonHang on ct.DonHang_ID equals dhang.ID
							join kh in db.KhachHang on dhang.KhachHang_ID equals kh.ID
							where (kh.ID == makh)

							select new Myorders()
							{
								TenXe = dh.TenXe,
								HinhAnhBia = dh.HinhAnhBia,
								DonGia = ct.DonGia,
								ID = kh.ID,
								SoLuong = ct.SoLuong,
								NgayDatHang = dhang.NgayDatHang

							}).OrderByDescending(dhang => dhang.NgayDatHang).ToList();

			return View(Myorders);
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
		public ActionResult ThanhToan(DonHang donHang)
		{
			if (ModelState.IsValid)
			{
				// Lưu vào bảng DatHang
				DonHang dh = new DonHang();
				dh.DiaChiGiaoHang = donHang.DiaChiGiaoHang;
				dh.SDTGiaoHang = donHang.SDTGiaoHang;
				dh.NgayDatHang = DateTime.Now;
				dh.KhachHang_ID = Convert.ToInt32(Session["MaKhachHang"]);
				dh.TinhTrang = 0;
				db.DonHang.Add(dh);
				db.SaveChanges();

				// Lưu vào bảng DatHang_ChiTiet
				List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
				foreach (var item in cart)
				{
					ChiTietDonHang ct = new ChiTietDonHang();
					ct.DonHang_ID = dh.ID;
					ct.Xe_ID = item.xe.ID;
					ct.SoLuong = Convert.ToInt16(item.soLuongTrongGio);
					ct.DonGia = item.xe.DonGia;
					db.ChiTietDonHang.Add(ct);
					var xe = db.Xe.Find(item.xe.ID);
					xe.SoLuong -= item.soLuongTrongGio;
					db.SaveChanges();
				}



				// Xóa giỏ hàng
				cart.Clear();
				SetAlert("Thanh Toán Sản Phẩm Thành Công", "success");
				// Quay về trang chủ
				return RedirectToAction("Index", "Home");
			}

			return View(donHang);
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
				string matKhauMaHoa =Libs.SHA1.ComputeHash(khachHang.MatKhau);
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
					Session["HoTenKhachHang"] = taiKhoan.HoVaTen;

					SetAlert("Đăng Nhập Thành Công", "success");

					// Quay về trang chủ
					return RedirectToAction("Index", "Home");
					
				}
			}

			return View(khachHang);
		}
		// GET: NhanVien/Logout
		public ActionResult Logout()
		{
			// Xóa SESSION
			Session.RemoveAll();

			// Quay về trang chủ
			return RedirectToAction("Index", "Home");
		}

		//GET: Register

		public ActionResult DangKy()
		{
			return View();
		}

		//POST: Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DangKy(KhachHang kh)
		{
			if (ModelState.IsValid)
			{
				var check = db.KhachHang.FirstOrDefault(r => r.TenDangNhap == kh.TenDangNhap);
				if (check == null)
				{
					kh.MatKhau =Libs.SHA1.ComputeHash(kh.MatKhau);
					kh.XacNhanMatKhau = Libs.SHA1.ComputeHash(kh.XacNhanMatKhau);
					db.Configuration.ValidateOnSaveEnabled = false;
					db.KhachHang.Add(kh);
					db.SaveChanges();
				
					return RedirectToAction("Login");
				}
				else
				{
					ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại !!!");
					
					return View();
				}
			}
			return View();
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