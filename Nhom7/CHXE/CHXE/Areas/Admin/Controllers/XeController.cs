using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CHXE.Models;

namespace CHXE.Areas.Admin.Controllers
{
    public class XeController : XacThucController
    {
        private CHXEEntities db = new CHXEEntities();

        // GET: Xe
        public ActionResult Index()
        {
            var xe = db.Xe.Include(x => x.LoaiXe).Include(x => x.NoiSanXuat).Include(x => x.ThuongHieu);
            return View(xe.ToList());
        }

        

        // GET: Xe/Create
        public ActionResult Create()
        {
            ViewBag.LoaiXe_ID = new SelectList(db.LoaiXe, "ID", "TenLoai");
            ViewBag.NoiSanXuat_ID = new SelectList(db.NoiSanXuat, "ID", "TenNoiSX");
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieu, "ID", "TenThuongHieu");
            ModelState.AddModelError("UploadError", "");
            return View();
        }

        // POST: Xe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,ThuongHieu_ID,NoiSanXuat_ID,LoaiXe_ID,TenXe,MauXe,SoLuong,DonGia,ThoiHanBaoHanh,DuLieuHinhAnhBia,MoTa")] Xe xe)
        {
            ViewBag.LoaiXe_ID = new SelectList(db.LoaiXe, "ID", "TenLoai", xe.LoaiXe_ID);
            ViewBag.NoiSanXuat_ID = new SelectList(db.NoiSanXuat, "ID", "TenNoiSX", xe.NoiSanXuat_ID);
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieu, "ID", "TenThuongHieu", xe.ThuongHieu_ID);
            if (ModelState.IsValid)
            {
                // Upload ảnh 
                if (xe.DuLieuHinhAnhBia != null)
                {

                    string folder = "Storage/";
                    string fileExtension = Path.GetExtension(xe.DuLieuHinhAnhBia.FileName).ToLower();

                    // Kiểm tra kiểu
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(xe);
                    }
                    else if (xe.DuLieuHinhAnhBia.ContentLength > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(xe);
                    }
                    else
                    {
                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
                        xe.DuLieuHinhAnhBia.SaveAs(filePath);

                        // Cập nhật đường dẫn vào CSDL
                        xe.HinhAnhBia = folder + fileName;

                        db.Xe.Add(xe);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("UploadError", "Hình ảnh bìa không được bỏ trống!");
                    return View(xe);
                }
            }

            return View(xe);
        }

        // GET: Xe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xe.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiXe_ID = new SelectList(db.LoaiXe, "ID", "TenLoai", xe.LoaiXe_ID);
            ViewBag.NoiSanXuat_ID = new SelectList(db.NoiSanXuat, "ID", "TenNoiSX", xe.NoiSanXuat_ID);
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieu, "ID", "TenThuongHieu", xe.ThuongHieu_ID);
            ModelState.AddModelError("UploadError", "");
            return View(xe);
        }

        // POST: Xe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,HinhAnhBia,ThuongHieu_ID,NoiSanXuat_ID,LoaiXe_ID,TenXe,MauXe,SoLuong,DonGia,ThoiHanBaoHanh,DuLieuHinhAnhBia,MoTa")] Xe xe)
        {
            ViewBag.LoaiXe_ID = new SelectList(db.LoaiXe, "ID", "TenLoai", xe.LoaiXe_ID);
            ViewBag.NoiSanXuat_ID = new SelectList(db.NoiSanXuat, "ID", "TenNoiSX", xe.NoiSanXuat_ID);
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieu, "ID", "TenThuongHieu", xe.ThuongHieu_ID);
            if (ModelState.IsValid)
            {
                // Upload ảnh mới
                if (xe.DuLieuHinhAnhBia != null)
                {
                    // Xóa ảnh cũ
                    string oldFilePath = Server.MapPath("~/" + xe.HinhAnhBia);
                    if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);

                    string folder = "Storage/";
                    string fileExtension = Path.GetExtension(xe.DuLieuHinhAnhBia.FileName).ToLower();

                    // Kiểm tra kiểu
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(xe);
                    }
                    else if (xe.DuLieuHinhAnhBia.ContentLength > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(xe);
                    }
                    else
                    {
                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
                        xe.DuLieuHinhAnhBia.SaveAs(filePath);

                        // Cập nhật đường dẫn vào CSDL
                        xe.HinhAnhBia = folder + fileName;

                        db.Entry(xe).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else // Giữ nguyên ảnh cũ
                {
                    db.Entry(xe).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(xe);
        }

        // GET: Xe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xe.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        // POST: Xe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Xe xe = db.Xe.Find(id);
            db.Xe.Remove(xe);
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
