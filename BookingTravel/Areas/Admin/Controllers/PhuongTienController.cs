using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingTravel.Models;

namespace BookingTravel.Areas.Admin.Controllers
{
    public class PhuongTienController : AuthController
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: PhuongTien
        public ActionResult Index()
        {
            return View(db.PhuongTien.ToList());
        }

        // GET: PhuongTien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhuongTien phuongTien = db.PhuongTien.Find(id);
            if (phuongTien == null)
            {
                return HttpNotFound();
            }
            return View(phuongTien);
        }

        // GET: PhuongTien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhuongTien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenPhuongTien,LoaiPhuongTien,SoCho")] PhuongTien phuongTien)
        {
            if (ModelState.IsValid)
            {
                db.PhuongTien.Add(phuongTien);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return RedirectToAction("Index");
            }

            return View(phuongTien);
        }

        // GET: PhuongTien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhuongTien phuongTien = db.PhuongTien.Find(id);
            if (phuongTien == null)
            {
                return HttpNotFound();
            }
            return View(phuongTien);
        }

        // POST: PhuongTien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenPhuongTien,LoaiPhuongTien,SoCho")] PhuongTien phuongTien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phuongTien).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }
            return View(phuongTien);
        }

        // GET: PhuongTien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhuongTien phuongTien = db.PhuongTien.Find(id);
            if (phuongTien == null)
            {
                return HttpNotFound();
            }
            return View(phuongTien);
        }

        // POST: PhuongTien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhuongTien phuongTien = db.PhuongTien.Find(id);
            db.PhuongTien.Remove(phuongTien);
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
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
