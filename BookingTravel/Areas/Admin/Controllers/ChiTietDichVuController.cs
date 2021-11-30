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
    public class ChiTietDichVuController : AuthController
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: ChiTietDichVu
        public ActionResult Index()
        {
            var chiTietDichVu = db.ChiTietDichVu.Include(c => c.Tour);
            return View(chiTietDichVu.ToList());
        }

        // GET: ChiTietDichVu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVu.Find(id);
            if (chiTietDichVu == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDichVu);
        }

        // GET: ChiTietDichVu/Create
        public ActionResult Create()
        {
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour");
            return View();
        }

        // POST: ChiTietDichVu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChiTietDichVu ct, int id)//truyền id
        {
            foreach (var n in ct.selectedServe) // tìm dịch vụ trong mảng
            {
                var service = new ChiTietDichVu
                {
                    DichVu = n, // lưu dịch vụ
                    Tour_ID = id // lưu id của tour truyền vào
                };
                db.ChiTietDichVu.Add(service);
                db.SaveChanges();
            }
            SetAlert("Cập nhật thành công dịch vụ của Tour", "success");
            return RedirectToAction("Details", "Tour", new { id = id });
        }

        // GET: ChiTietDichVu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVu.Find(id);
            if (chiTietDichVu == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", chiTietDichVu.Tour_ID);
            return View(chiTietDichVu);
        }

        // POST: ChiTietDichVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tour_ID,DichVu")] ChiTietDichVu chiTietDichVu, int idtour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDichVu).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công dịch vụ của Tour", "success");
                return RedirectToAction("Details", "Tour", new { id = idtour });
            }
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", chiTietDichVu.Tour_ID);
            return View(chiTietDichVu);
        }

        // GET: ChiTietDichVu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVu.Find(id);
            if (chiTietDichVu == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDichVu);
        }

        // POST: ChiTietDichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idtour)
        {
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVu.Find(id);
            db.ChiTietDichVu.Remove(chiTietDichVu);
            db.SaveChanges();
            SetAlert("Xóa thành công dịch vụ của Tour", "success");
            return RedirectToAction("Details", "Tour", new { id = idtour });
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