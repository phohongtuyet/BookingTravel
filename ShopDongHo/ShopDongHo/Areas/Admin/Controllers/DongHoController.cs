using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopDongHo.Models;

namespace ShopDongHo.Areas.Admin.Controllers
{
    public class DongHoController : AuthController
    {
        private ShopDongHoEntities db = new ShopDongHoEntities();

        // GET: DongHo
        public ActionResult Index()
        {
            var dongHo = db.DongHo.Include(d => d.ChatLieu).Include(d => d.LoaiDH).Include(d => d.ThuongHieu).Include(d => d.XuatXu);
            return View(dongHo.ToList());
        }


        // GET: DongHo/Create
        public ActionResult Create()
        {
            ViewBag.ChatLieu_ID = new SelectList(db.ChatLieu, "ID", "TenChatLieu");
            ViewBag.TenLoai_ID = new SelectList(db.LoaiDH, "ID", "TenLoai");
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieu, "ID", "TenThuongHieu");
            ViewBag.XuatXu_ID = new SelectList(db.XuatXu, "ID", "TenQG");
            ModelState.AddModelError("UploadError", "");
            return View();
        }

        // POST: DongHo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,ThuongHieu_ID,TenLoai_ID,ChatLieu_ID,XuatXu_ID,TenDongHo,MauSac,HanBaoHanh,DonGia,SoLuong,DuLieuHinhAnhDH,MoTa")] DongHo dongHo)
        {
           
            ViewBag.ChatLieu_ID = new SelectList(db.ChatLieu, "ID", "TenChatLieu", dongHo.ChatLieu_ID);
            ViewBag.TenLoai_ID = new SelectList(db.LoaiDH, "ID", "TenLoai", dongHo.TenLoai_ID);
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieu, "ID", "TenThuongHieu", dongHo.ThuongHieu_ID);
            ViewBag.XuatXu_ID = new SelectList(db.XuatXu, "ID", "TenQG", dongHo.XuatXu_ID);
            if (ModelState.IsValid)
            {
                // Upload
                if (!Object.Equals(dongHo.DuLieuHinhAnhDH, null))
                {
                    string folder = "Storage/";
                    string fileName = DateTime.Now.ToFileTime() + "_" + dongHo.DuLieuHinhAnhDH.FileName;
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    // Kiểm tra kiểu
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(dongHo);
                    }
                    else if (dongHo.DuLieuHinhAnhDH.ContentLength > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(dongHo);
                    }
                    else
                    {
                        string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
                        dongHo.DuLieuHinhAnhDH.SaveAs(filePath);

                        // Cập nhật đường dẫn vào CSDL
                        dongHo.HinhAnhDH = folder + fileName;

                        db.DongHo.Add(dongHo);
                        db.SaveChanges();
                        SetAlert("Thêm mới thành công", "success");
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("UploadError", "Hình ảnh bìa không được bỏ trống!");
                    return View(dongHo);
                }
            }


            return View(dongHo);
        }
       
        // GET: Admin/DienThoaiDD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongHo dongHo = db.DongHo.Find(id);
            if (dongHo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChatLieu_ID = new SelectList(db.ChatLieu, "ID", "TenChatLieu", dongHo.ChatLieu_ID);
            ViewBag.TenLoai_ID = new SelectList(db.LoaiDH, "ID", "TenLoai", dongHo.TenLoai_ID);
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieu, "ID", "TenThuongHieu", dongHo.ThuongHieu_ID);
            ViewBag.XuatXu_ID = new SelectList(db.XuatXu, "ID", "TenQG", dongHo.XuatXu_ID);
            ModelState.AddModelError("UploadError", "");
            return View(dongHo);
        }

        // POST: Admin/DongHo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,HinhAnhDH,ThuongHieu_ID,TenLoai_ID,ChatLieu_ID,XuatXu_ID,TenDongHo,MauSac,HanBaoHanh,DonGia,SoLuong,DuLieuHinhAnhDH,MoTa")] DongHo dongHo)
        {
            
            ViewBag.ChatLieu_ID = new SelectList(db.ChatLieu, "ID", "TenChatLieu", dongHo.ChatLieu_ID);
            ViewBag.TenLoai_ID = new SelectList(db.LoaiDH, "ID", "TenLoai", dongHo.TenLoai_ID);
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieu, "ID", "TenThuongHieu", dongHo.ThuongHieu_ID);
            ViewBag.XuatXu_ID = new SelectList(db.XuatXu, "ID", "TenQG", dongHo.XuatXu_ID);

            if (ModelState.IsValid)
            {
                // upload ảnh mới
                if (!Object.Equals(dongHo.DuLieuHinhAnhDH, null))
                {
                    // Xóa ảnh cũ
                    string oldFilePath = Server.MapPath("~/" + dongHo.HinhAnhDH);
                    if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);

                    string folder = "Storage/";
                    string fileName = DateTime.Now.ToFileTime() + "_" + dongHo.DuLieuHinhAnhDH.FileName;
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    // Kiểm tra kiểu
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(dongHo);
                    }
                    else if (dongHo.DuLieuHinhAnhDH.ContentLength > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(dongHo);
                    }
                    else
                    {
                        string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
                        dongHo.DuLieuHinhAnhDH.SaveAs(filePath);

                        // Cập nhật đường dẫn vào CSDL
                        dongHo.HinhAnhDH = folder + fileName;

                        db.Entry(dongHo).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else // Giữ nguyên ảnh cũ
                {
                    db.Entry(dongHo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(dongHo);
        }

        // GET: DongHo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongHo dongHo = db.DongHo.Find(id);
            if (dongHo == null)
            {
                return HttpNotFound();
            }
            return View(dongHo);
        }

        // POST: DongHo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DongHo dongHo = db.DongHo.Find(id);
            db.DongHo.Remove(dongHo);
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
