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
    public class ThuongHieuController : AuthController
    {
        private ShopDongHoEntities db = new ShopDongHoEntities();

        // GET: ThuongHieu
        public ActionResult Index()
        {
            return View(db.ThuongHieu.ToList());
        }

        // GET: ThuongHieu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThuongHieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenThuongHieu")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                db.ThuongHieu.Add(thuongHieu);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return RedirectToAction("Index");
            }

            return View(thuongHieu);
        }

        // GET: ThuongHieu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieu.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // POST: ThuongHieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenThuongHieu")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thuongHieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thuongHieu);
        }

        // GET: ThuongHieu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieu.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // POST: ThuongHieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThuongHieu thuongHieu = db.ThuongHieu.Find(id);
            db.ThuongHieu.Remove(thuongHieu);
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
