using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopDongHo.Models;
using ShopDongHo.Libs;

namespace ShopDongHo.Areas.Admin.Controllers
{
    public class NhanVienController : AuthController
    {
        private ShopDongHoEntities db = new ShopDongHoEntities();

        // GET: NhanVien
        public ActionResult Index()
        {
            return View(db.NhanVien.ToList());
        }
        

        

        // GET: NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoVaTen,DienThoai,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau,Quyen")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                nhanVien.MatKhau = SHA1.ComputeHash(nhanVien.MatKhau);
                nhanVien.XacNhanMatKhau = SHA1.ComputeHash(nhanVien.XacNhanMatKhau);
                db.NhanVien.Add(nhanVien);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return RedirectToAction("Index");
            }

            return View(nhanVien);
        }

        // GET: NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(new NhanVienEdit(nhanVien));
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoVaTen,DienThoai,DiaChi,TenDangNhap,MatKhau,Quyen")] NhanVienEdit nhanVien)
        {
            if (ModelState.IsValid)
            {
                NhanVien n = db.NhanVien.Find(nhanVien.ID);

                // Giữ nguyên mật khẩu cũ
                if (nhanVien.MatKhau == null)
                {
                    n.ID = nhanVien.ID;
                    n.HoVaTen = nhanVien.HoVaTen;
                    n.DienThoai = nhanVien.DienThoai;
                    n.DiaChi = nhanVien.DiaChi;
                    n.TenDangNhap = nhanVien.TenDangNhap;
                    n.XacNhanMatKhau = n.MatKhau;
                    n.Quyen = nhanVien.Quyen;
                }
                else // Cập nhật mật khẩu mới
                {
                    n.ID = nhanVien.ID;
                    n.HoVaTen = nhanVien.HoVaTen;
                    n.DienThoai = nhanVien.DienThoai;
                    n.DiaChi = nhanVien.DiaChi;
                    n.TenDangNhap = nhanVien.TenDangNhap;
                    n.MatKhau = SHA1.ComputeHash(nhanVien.MatKhau);
                    n.XacNhanMatKhau = SHA1.ComputeHash(nhanVien.XacNhanMatKhau);
                    n.Quyen = nhanVien.Quyen;
                }
                db.Entry(n).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanVien.Find(id);
            db.NhanVien.Remove(nhanVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
