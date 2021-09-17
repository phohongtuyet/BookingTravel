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
    public class BinhLuanController : Controller
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: BinhLuan
        public ActionResult Index()
        {
            var binhLuan = db.BinhLuan.Include(b => b.BaiViet).Include(b => b.KhachHang);
            return View(binhLuan.ToList());
        }

        // GET: BinhLuan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // GET: BinhLuan/Create
        public ActionResult Create()
        {
            ViewBag.BaiViet_ID = new SelectList(db.BaiViet, "ID", "TieuDe");
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaten");
            return View();
        }

        // POST: BinhLuan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BaiViet_ID,KhachHang_ID,NoiDung,NgayDang,KiemDuyet")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.BinhLuan.Add(binhLuan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BaiViet_ID = new SelectList(db.BaiViet, "ID", "TieuDe", binhLuan.BaiViet_ID);
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaten", binhLuan.KhachHang_ID);
            return View(binhLuan);
        }

        // GET: BinhLuan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaiViet_ID = new SelectList(db.BaiViet, "ID", "TieuDe", binhLuan.BaiViet_ID);
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaten", binhLuan.KhachHang_ID);
            return View(binhLuan);
        }

        // POST: BinhLuan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BaiViet_ID,KhachHang_ID,NoiDung,NgayDang,KiemDuyet")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binhLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaiViet_ID = new SelectList(db.BaiViet, "ID", "TieuDe", binhLuan.BaiViet_ID);
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaten", binhLuan.KhachHang_ID);
            return View(binhLuan);
        }

        // GET: BinhLuan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // POST: BinhLuan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            db.BinhLuan.Remove(binhLuan);
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