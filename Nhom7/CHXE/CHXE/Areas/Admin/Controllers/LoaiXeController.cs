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
    public class LoaiXeController : XacThucController
    {
        private CHXEEntities db = new CHXEEntities();

        // GET: LoaiXe
        public ActionResult Index()
        {
            return View(db.LoaiXe.ToList());
        }

      
        // GET: LoaiXe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiXe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenLoai")] LoaiXe loaiXe)
        {
            if (ModelState.IsValid)
            {
                db.LoaiXe.Add(loaiXe);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(loaiXe);
        }

        // GET: LoaiXe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiXe loaiXe = db.LoaiXe.Find(id);
            if (loaiXe == null)
            {
                return HttpNotFound();
            }
            return View(loaiXe);
        }

        // POST: LoaiXe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenLoai")] LoaiXe loaiXe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiXe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiXe);
        }

        // GET: LoaiXe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiXe loaiXe = db.LoaiXe.Find(id);
            if (loaiXe == null)
            {
                return HttpNotFound();
            }
            return View(loaiXe);
        }

        // POST: LoaiXe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiXe loaiXe = db.LoaiXe.Find(id);
            db.LoaiXe.Remove(loaiXe);
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
