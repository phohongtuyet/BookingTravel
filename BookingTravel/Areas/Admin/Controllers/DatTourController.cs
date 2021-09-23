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
    public class DatTourController : AuthController
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: DatTour
        public ActionResult Index()
        {
            var datTour = db.DatTour.Include(d => d.KhachHang).Include(d => d.NhanVien);
            return View(datTour.ToList());
        }

        // GET: DatTour/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatTour datTour = db.DatTour.Find(id);
            if (datTour == null)
            {
                return HttpNotFound();
            }
            return View(datTour);
        }

        // GET: DatTour/Create
        public ActionResult Create()
        {
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaten");
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen");
            return View();
        }

        // POST: DatTour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NhanVien_ID,KhachHang_ID,DienThoaiDatTour,HoVaTen,NgayDatHang,TinhTrang")] DatTour datTour)
        {
            if (ModelState.IsValid)
            {
                db.DatTour.Add(datTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaten", datTour.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen", datTour.NhanVien_ID);
            return View(datTour);
        }

        // GET: DatTour/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatTour datTour = db.DatTour.Find(id);
            if (datTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaten", datTour.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen", datTour.NhanVien_ID);
            return View(datTour);
        }

        // POST: DatTour/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NhanVien_ID,KhachHang_ID,DienThoaiDatTour,HoVaTen,NgayDatHang,TinhTrang")] DatTour datTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaten", datTour.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen", datTour.NhanVien_ID);
            return View(datTour);
        }

        // GET: DatTour/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatTour datTour = db.DatTour.Find(id);
            if (datTour == null)
            {
                return HttpNotFound();
            }
            return View(datTour);
        }

        // POST: DatTour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatTour datTour = db.DatTour.Find(id);
            db.DatTour.Remove(datTour);
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
