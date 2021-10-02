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
    public class ChiTietDiaDiemThamQuanController : AuthController
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: ChiTietDiaDiemThamQuan
        public ActionResult Index()
        {
            var chiTietDiaDiemThamQuan = db.ChiTietDiaDiemThamQuan.Include(c => c.DiaDiemThamQuan).Include(c => c.Tour);
            return View(chiTietDiaDiemThamQuan.ToList());
        }

        // GET: ChiTietDiaDiemThamQuan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDiaDiemThamQuan chiTietDiaDiemThamQuan = db.ChiTietDiaDiemThamQuan.Find(id);
            if (chiTietDiaDiemThamQuan == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDiaDiemThamQuan);
        }

        // GET: ChiTietDiaDiemThamQuan/Create
        public ActionResult Create()
        {
            ViewBag.DiaDiemThamQuan_ID = new SelectList(db.DiaDiemThamQuan, "ID", "TenDiaDanh");
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour");
            return View();
        }

        // POST: ChiTietDiaDiemThamQuan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChiTietDiaDiemThamQuan ct, int id)
        {

            foreach (var n in ct.selectedLocation)
            {
                if (n != null && n > 0)
                {
                    var location = new ChiTietDiaDiemThamQuan
                    {
                        DiaDiemThamQuan_ID = n,
                        Tour_ID = id

                    };

                    db.ChiTietDiaDiemThamQuan.Add(location);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Tour");

        }

        // GET: ChiTietDiaDiemThamQuan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDiaDiemThamQuan chiTietDiaDiemThamQuan = db.ChiTietDiaDiemThamQuan.Find(id);
            if (chiTietDiaDiemThamQuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiaDiemThamQuan_ID = new SelectList(db.DiaDiemThamQuan, "ID", "TenDiaDanh", chiTietDiaDiemThamQuan.DiaDiemThamQuan_ID);
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", chiTietDiaDiemThamQuan.Tour_ID);
            return View(chiTietDiaDiemThamQuan);
        }

        // POST: ChiTietDiaDiemThamQuan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tour_ID,DiaDiemThamQuan_ID")] ChiTietDiaDiemThamQuan chiTietDiaDiemThamQuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDiaDiemThamQuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiaDiemThamQuan_ID = new SelectList(db.DiaDiemThamQuan, "ID", "TenDiaDanh", chiTietDiaDiemThamQuan.DiaDiemThamQuan_ID);
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", chiTietDiaDiemThamQuan.Tour_ID);
            return View(chiTietDiaDiemThamQuan);
        }

        // GET: ChiTietDiaDiemThamQuan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDiaDiemThamQuan chiTietDiaDiemThamQuan = db.ChiTietDiaDiemThamQuan.Find(id);
            if (chiTietDiaDiemThamQuan == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDiaDiemThamQuan);
        }

        // POST: ChiTietDiaDiemThamQuan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDiaDiemThamQuan chiTietDiaDiemThamQuan = db.ChiTietDiaDiemThamQuan.Find(id);
            db.ChiTietDiaDiemThamQuan.Remove(chiTietDiaDiemThamQuan);
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
