﻿using System;
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
    public class BaiVietController : Controller
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: BaiViet
        public ActionResult Index()
        {
            var baiViet = db.BaiViet.Include(b => b.NhanVien);
            return View(baiViet.ToList());
        }

        // GET: BaiViet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViet.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // GET: BaiViet/Create
        public ActionResult Create()
        {
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen");
            return View();
        }

        // POST: BaiViet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NhanVien_ID,TieuDe,TomTat,NoiDung,NgayDang,LuotXem,KiemDuyet,TrangThaiBinhLuan")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                db.BaiViet.Add(baiViet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen", baiViet.NhanVien_ID);
            return View(baiViet);
        }

        // GET: BaiViet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViet.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen", baiViet.NhanVien_ID);
            return View(baiViet);
        }

        // POST: BaiViet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NhanVien_ID,TieuDe,TomTat,NoiDung,NgayDang,LuotXem,KiemDuyet,TrangThaiBinhLuan")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baiViet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen", baiViet.NhanVien_ID);
            return View(baiViet);
        }

        // GET: BaiViet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViet.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // POST: BaiViet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiViet baiViet = db.BaiViet.Find(id);
            db.BaiViet.Remove(baiViet);
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