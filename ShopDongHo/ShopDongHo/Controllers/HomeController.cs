using Microsoft.Azure.Documents;
using ShopDongHo.Libs;
using ShopDongHo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ShopDongHo.Controllers
{
    public class HomeController : Controller
	{
		private ShopDongHoEntities db = new ShopDongHoEntities();

		// GET: Home
		public ActionResult Index()
		{
			var dongho = db.DongHo.Where(r => r.SoLuong > 0).ToList();
			return View(dongho);
		}

		public ActionResult _Catelogies()
		{
			var loai = db.ThuongHieu.OrderBy(r => r.TenThuongHieu).ToList();
			return PartialView(loai);
		}

		public ActionResult SubPosts(int id)
		{
			var baiviet = db.DongHo.Where(r => r.ThuongHieu_ID == id ).ToList();
			return View(baiviet);
		}

		[HttpPost]
		public ActionResult Search(FormCollection collection)
		{
			string tukhoa = collection["Tukhoa"].ToString();
			var baiViet = db.DongHo.Where(r=>r.TenDongHo.Contains(tukhoa) || r.MoTa.Contains(tukhoa)).ToList();
			return View(baiViet);
		}

		public ActionResult Details(int maSP)
		{
			var dongho = db.DongHo.Where(r => r.ID == maSP ).SingleOrDefault();
			return View(dongho);
		}

		public ActionResult BuyMost()
		{
			var bestSale = (from dh in db.DongHo
							join ct in db.DatHang_ChiTiet on dh.ID equals ct.DongHo_ID
							join dhang in db.DatHang on ct.DatHang_ID equals dhang.ID
							where (ct.SoLuong > 0)							
							select new BestSaleModels(){ 
								TenDongHo =dh.TenDongHo,
								HinhAnhDH=dh.HinhAnhDH,
								DonGia =dh.DonGia,
								ID=dh.ID,
								SoLuong=ct.SoLuong
							}).OrderByDescending(ct =>ct.SoLuong).Distinct().ToList();
			
			return View(bestSale);
		}

		public ActionResult MyOrders()
		{
			int makh = Convert.ToInt32(Session["MaKhachHang"]);
			var Myorders = (from dh in db.DongHo
							join ct in db.DatHang_ChiTiet on dh.ID equals ct.DongHo_ID
							join dhang in db.DatHang on ct.DatHang_ID equals dhang.ID 
							join kh in db.KhachHang on  dhang.KhachHang_ID equals kh.ID 
							where(kh.ID == makh)

							select new Myorders()
							{
								TenDongHo = dh.TenDongHo,
								HinhAnhDH = dh.HinhAnhDH,
								DonGia = ct.DonGia,
								ID = kh.ID,
								SoLuong = ct.SoLuong,
								NgayDatHang = dhang.NgayDatHang

							}).OrderByDescending(dhang => dhang.NgayDatHang).ToList();

			return View(Myorders);
		}

		public ActionResult ChangePassword()
		{
			//ModelState.AddModelError("ErrorChangePassword", "");
			return View();
			
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ChangePassword([Bind(Include = "MatKhau,MatKhauMoi,XacNhanMatKhauMoi")] KhachHangChangePassword khachHangChangePassword )
		{	
			if (ModelState.IsValid)
			{
				int makh = Convert.ToInt32(Session["MaKhachHang"]);
				KhachHang khachHang = db.KhachHang.Find(makh);				
				if(khachHang == null)
                {
					return HttpNotFound();
                }
				khachHangChangePassword.MatKhau = SHA1.ComputeHash(khachHangChangePassword.MatKhau);
				if(khachHang.MatKhau == khachHangChangePassword.MatKhau)
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

		public ActionResult success()
		{
			return View();
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
		public ActionResult ThanhToan(DatHang datHang)
		{
			if (ModelState.IsValid)
			{
				// Lưu vào bảng DatHang
				DatHang dh = new DatHang();
				dh.DiaChiGiaoHang = datHang.DiaChiGiaoHang;
				dh.DienThoaiGiaoHang = datHang.DienThoaiGiaoHang;
				dh.NgayDatHang = DateTime.Now;
				dh.KhachHang_ID = Convert.ToInt32(Session["MaKhachHang"]);
				dh.TinhTrang = 0;
				db.DatHang.Add(dh);
				db.SaveChanges();

				// Lưu vào bảng DatHang_ChiTiet
				List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
				foreach (var item in cart)
				{
					DatHang_ChiTiet ct = new DatHang_ChiTiet();
					ct.DatHang_ID = dh.ID;
					ct.DongHo_ID = item.dongho.ID;
					ct.SoLuong = Convert.ToInt16(item.soLuongTrongGio);
					ct.DonGia = item.dongho.DonGia;
					db.DatHang_ChiTiet.Add(ct);
					var dongho = db.DongHo.Find(item.dongho.ID);
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
		public ActionResult Logout()
		{
			// Xóa SESSION
			Session.RemoveAll();

			// Quay về trang chủ
			return RedirectToAction("Index", "Home");
		}
	}
}