using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopDongHo.Models;

namespace ShopDongHo.Areas.Admin.Controllers
{
    public class LoaiDHController : AuthController
    {
        private ShopDongHoEntities db = new ShopDongHoEntities();

        // GET: LoaiDH
        public ActionResult Index()
        {
            return View(db.LoaiDH.ToList());
        }
    
        // GET: LoaiDH/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiDH/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenLoai")] LoaiDH loaiDH)
        {
            if (ModelState.IsValid)
            {
                db.LoaiDH.Add(loaiDH);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return RedirectToAction("Index");
            }

            return View(loaiDH);
        }

        // GET: LoaiDH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDH loaiDH = db.LoaiDH.Find(id);
            if (loaiDH == null)
            {
                return HttpNotFound();
            }
            return View(loaiDH);
        }

        // POST: LoaiDH/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenLoai")] LoaiDH loaiDH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiDH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiDH);
        }

        // GET: LoaiDH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDH loaiDH = db.LoaiDH.Find(id);
            if (loaiDH == null)
            {
                return HttpNotFound();
            }
            return View(loaiDH);
        }

        // POST: LoaiDH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiDH loaiDH = db.LoaiDH.Find(id);
            db.LoaiDH.Remove(loaiDH);
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
