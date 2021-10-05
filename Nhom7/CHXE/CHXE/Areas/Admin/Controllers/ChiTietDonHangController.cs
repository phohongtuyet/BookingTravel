using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CHXE.Models;

namespace CHXE.Areas.Admin.Controllers
{
    public class ChiTietDonHangController : XacThucController
    {
        private CHXEEntities db = new CHXEEntities();

        // GET: ChiTietDonHang
        public ActionResult Index()
        {
            var chiTietDonHang = db.ChiTietDonHang.Include(c => c.DonHang).Include(c => c.Xe);
            return View(chiTietDonHang.ToList());
        }

       

        // GET: ChiTietDonHang/Create
        public ActionResult Create()
        {
            ViewBag.DonHang_ID = new SelectList(db.DonHang, "ID", "SDTGiaoHang");
            ViewBag.Xe_ID = new SelectList(db.Xe, "ID", "TenXe");
            return View();
        }

        // POST: ChiTietDonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DonHang_ID,Xe_ID,SoLuong,DonGia")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDonHang.Add(chiTietDonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DonHang_ID = new SelectList(db.DonHang, "ID", "SDTGiaoHang", chiTietDonHang.DonHang_ID);
            ViewBag.Xe_ID = new SelectList(db.Xe, "ID", "TenXe", chiTietDonHang.Xe_ID);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHang.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.DonHang_ID = new SelectList(db.DonHang, "ID", "SDTGiaoHang", chiTietDonHang.DonHang_ID);
            ViewBag.Xe_ID = new SelectList(db.Xe, "ID", "TenXe", chiTietDonHang.Xe_ID);
            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DonHang_ID,Xe_ID,SoLuong,DonGia")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DonHang_ID = new SelectList(db.DonHang, "ID", "SDTGiaoHang", chiTietDonHang.DonHang_ID);
            ViewBag.Xe_ID = new SelectList(db.Xe, "ID", "TenXe", chiTietDonHang.Xe_ID);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHang.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHang.Find(id);
            db.ChiTietDonHang.Remove(chiTietDonHang);
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
