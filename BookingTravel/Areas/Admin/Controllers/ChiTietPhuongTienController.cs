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
    public class ChiTietPhuongTienController : AuthController
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: ChiTietPhuongTien
        public ActionResult Index()
        {
            var chiTietPhuongTien = db.ChiTietPhuongTien.Include(c => c.PhuongTien).Include(c => c.Tour);
            return View(chiTietPhuongTien.ToList());
        }

        // GET: ChiTietPhuongTien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhuongTien chiTietPhuongTien = db.ChiTietPhuongTien.Find(id);
            if (chiTietPhuongTien == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhuongTien);
        }

        // GET: ChiTietPhuongTien/Create
        public ActionResult Create()
        {
            ViewBag.PhuongTien_ID = new SelectList(db.PhuongTien, "ID", "TenPhuongTien");
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour");
            return View();
        }

        // POST: ChiTietPhuongTien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChiTietPhuongTien ct, int id)
        {
            foreach (var n in ct.selectedTranpost)
            {
                if (n != null && n > 0)
                {
                    var transpost = new ChiTietPhuongTien
                    {
                        PhuongTien_ID = n,
                        Tour_ID = id

                    };

                    db.ChiTietPhuongTien.Add(transpost);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Details", "Tour", new { id = id});
                    
        }

        // GET: ChiTietPhuongTien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhuongTien chiTietPhuongTien = db.ChiTietPhuongTien.Find(id);
            if (chiTietPhuongTien == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhuongTien_ID = new SelectList(db.PhuongTien, "ID", "TenPhuongTien", chiTietPhuongTien.PhuongTien_ID);
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", chiTietPhuongTien.Tour_ID);
            return View(chiTietPhuongTien);
        }

        // POST: ChiTietPhuongTien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tour_ID,PhuongTien_ID")] ChiTietPhuongTien chiTietPhuongTien,int id, int idtour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietPhuongTien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Tour", new { id = idtour });
            }
            ViewBag.PhuongTien_ID = new SelectList(db.PhuongTien, "ID", "TenPhuongTien", chiTietPhuongTien.PhuongTien_ID);
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", chiTietPhuongTien.Tour_ID);
            return View(chiTietPhuongTien);
        }

        // GET: ChiTietPhuongTien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhuongTien chiTietPhuongTien = db.ChiTietPhuongTien.Find(id);
            if (chiTietPhuongTien == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhuongTien);
        }

        // POST: ChiTietPhuongTien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idtour)
        {
            ChiTietPhuongTien chiTietPhuongTien = db.ChiTietPhuongTien.Find(id);
            db.ChiTietPhuongTien.Remove(chiTietPhuongTien);
            db.SaveChanges();
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
