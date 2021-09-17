using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingTravel.Models;

namespace BookingTravel.Controllers
{
    public class DatTour_ChiTietController : Controller
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: DatTour_ChiTiet
        public ActionResult Index()
        {
            var datTour_ChiTiet = db.DatTour_ChiTiet.Include(d => d.DatTour).Include(d => d.Tour);
            return View(datTour_ChiTiet.ToList());
        }

        // GET: DatTour_ChiTiet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatTour_ChiTiet datTour_ChiTiet = db.DatTour_ChiTiet.Find(id);
            if (datTour_ChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(datTour_ChiTiet);
        }

        // GET: DatTour_ChiTiet/Create
        public ActionResult Create()
        {
            ViewBag.DatTour_ID = new SelectList(db.DatTour, "ID", "DienThoaiDatTour");
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour");
            return View();
        }

        // POST: DatTour_ChiTiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DatTour_ID,Tour_ID,SoLuong,DonGia")] DatTour_ChiTiet datTour_ChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.DatTour_ChiTiet.Add(datTour_ChiTiet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DatTour_ID = new SelectList(db.DatTour, "ID", "DienThoaiDatTour", datTour_ChiTiet.DatTour_ID);
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", datTour_ChiTiet.Tour_ID);
            return View(datTour_ChiTiet);
        }

        // GET: DatTour_ChiTiet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatTour_ChiTiet datTour_ChiTiet = db.DatTour_ChiTiet.Find(id);
            if (datTour_ChiTiet == null)
            {
                return HttpNotFound();
            }
            ViewBag.DatTour_ID = new SelectList(db.DatTour, "ID", "DienThoaiDatTour", datTour_ChiTiet.DatTour_ID);
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", datTour_ChiTiet.Tour_ID);
            return View(datTour_ChiTiet);
        }

        // POST: DatTour_ChiTiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DatTour_ID,Tour_ID,SoLuong,DonGia")] DatTour_ChiTiet datTour_ChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datTour_ChiTiet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DatTour_ID = new SelectList(db.DatTour, "ID", "DienThoaiDatTour", datTour_ChiTiet.DatTour_ID);
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", datTour_ChiTiet.Tour_ID);
            return View(datTour_ChiTiet);
        }

        // GET: DatTour_ChiTiet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatTour_ChiTiet datTour_ChiTiet = db.DatTour_ChiTiet.Find(id);
            if (datTour_ChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(datTour_ChiTiet);
        }

        // POST: DatTour_ChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatTour_ChiTiet datTour_ChiTiet = db.DatTour_ChiTiet.Find(id);
            db.DatTour_ChiTiet.Remove(datTour_ChiTiet);
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
