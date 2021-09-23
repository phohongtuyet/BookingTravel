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
    public class DiaDiemThamQuanController : AuthController
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: DiaDiemThamQuan
        public ActionResult Index()
        {
            var diaDiemThamQuan = db.DiaDiemThamQuan.Include(d => d.KhachSan);
            return View(diaDiemThamQuan.ToList());
        }

        // GET: DiaDiemThamQuan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaDiemThamQuan diaDiemThamQuan = db.DiaDiemThamQuan.Find(id);
            if (diaDiemThamQuan == null)
            {
                return HttpNotFound();
            }
            return View(diaDiemThamQuan);
        }

        // GET: DiaDiemThamQuan/Create
        public ActionResult Create()
        {
            ViewBag.KhachSan_ID = new SelectList(db.KhachSan, "ID", "TenKhachSan");
            return View();
        }

        // POST: DiaDiemThamQuan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenDiaDanh,Tinh,KhachSan_ID")] DiaDiemThamQuan diaDiemThamQuan)
        {
            if (ModelState.IsValid)
            {
                db.DiaDiemThamQuan.Add(diaDiemThamQuan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhachSan_ID = new SelectList(db.KhachSan, "ID", "TenKhachSan", diaDiemThamQuan.KhachSan_ID);
            return View(diaDiemThamQuan);
        }

        // GET: DiaDiemThamQuan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaDiemThamQuan diaDiemThamQuan = db.DiaDiemThamQuan.Find(id);
            if (diaDiemThamQuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhachSan_ID = new SelectList(db.KhachSan, "ID", "TenKhachSan", diaDiemThamQuan.KhachSan_ID);
            return View(diaDiemThamQuan);
        }

        // POST: DiaDiemThamQuan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenDiaDanh,Tinh,KhachSan_ID")] DiaDiemThamQuan diaDiemThamQuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diaDiemThamQuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhachSan_ID = new SelectList(db.KhachSan, "ID", "TenKhachSan", diaDiemThamQuan.KhachSan_ID);
            return View(diaDiemThamQuan);
        }

        // GET: DiaDiemThamQuan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaDiemThamQuan diaDiemThamQuan = db.DiaDiemThamQuan.Find(id);
            if (diaDiemThamQuan == null)
            {
                return HttpNotFound();
            }
            return View(diaDiemThamQuan);
        }

        // POST: DiaDiemThamQuan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiaDiemThamQuan diaDiemThamQuan = db.DiaDiemThamQuan.Find(id);
            db.DiaDiemThamQuan.Remove(diaDiemThamQuan);
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
