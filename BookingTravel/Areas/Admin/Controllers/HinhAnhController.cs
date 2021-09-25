using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingTravel.Models;

namespace BookingTravel.Areas.Admin.Controllers
{
    public class HinhAnhController : AuthController
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: HinhAnh
        public ActionResult Index()
        {
            var hinhAnh = db.HinhAnh.Include(h => h.Tour);
            return View(hinhAnh.ToList());
        }

        // GET: HinhAnh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhAnh = db.HinhAnh.Find(id);
            if (hinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnh);
        }

        // GET: HinhAnh/Create
        public ActionResult Create()
        {
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour");
            ModelState.AddModelError("UploadError", "");
            return View();
        }

        // POST: HinhAnh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tour_ID,DuLieuHinhAnh")] HinhAnh hinhAnh)
        {
            if (ModelState.IsValid)
            {
                // Upload              
                if (!Object.Equals(hinhAnh.DuLieuHinhAnh, null))
                {
                    string folder = "Storage/";
                    string fileName = DateTime.Now.ToFileTime() + "_" + hinhAnh.DuLieuHinhAnh.FileName;
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    // Kiểm tra kiểu
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(hinhAnh);
                    }
                    else
                    {
                        
                        string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
                        hinhAnh.DuLieuHinhAnh.SaveAs(filePath);

                        // Cập nhật đường dẫn vào CSDL
                        hinhAnh.Tour_ID = hinhAnh.Tour_ID;
                        hinhAnh.HinhAnh1 = folder + fileName;

                      




                        db.HinhAnh.Add(hinhAnh);
                        db.SaveChanges();
                        //ViewBag.UploadStatus = hinhAnh.Count().ToString() + " files uploaded successfully.";
                        return RedirectToAction("Index","Tour");
                    }
                }
                else
                {
                    ModelState.AddModelError("UploadError", "Hình ảnh bìa không được bỏ trống!");
                    return View(hinhAnh);
                }
            }
            return View(hinhAnh);
        }

        // GET: HinhAnh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhAnh = db.HinhAnh.Find(id);
            if (hinhAnh == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", hinhAnh.Tour_ID);
            return View(hinhAnh);
        }

        // POST: HinhAnh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tour_ID,HinhAnh1")] HinhAnh hinhAnh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhAnh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", hinhAnh.Tour_ID);
            return View(hinhAnh);
        }

        // GET: HinhAnh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhAnh = db.HinhAnh.Find(id);
            if (hinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnh);
        }

        // POST: HinhAnh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HinhAnh hinhAnh = db.HinhAnh.Find(id);
            db.HinhAnh.Remove(hinhAnh);
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
