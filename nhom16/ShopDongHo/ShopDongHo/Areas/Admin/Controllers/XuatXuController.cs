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
    public class XuatXuController : AuthController
    {
        private ShopDongHoEntities db = new ShopDongHoEntities();

        // GET: XuatXu
        public ActionResult Index()
        {
            return View(db.XuatXu.ToList());
        } 

        // GET: XuatXu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: XuatXu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenQG")] XuatXu xuatXu)
        {
            if (ModelState.IsValid)
            {
                db.XuatXu.Add(xuatXu);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return RedirectToAction("Index");
            }

            return View(xuatXu);
        }

        // GET: XuatXu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatXu xuatXu = db.XuatXu.Find(id);
            if (xuatXu == null)
            {
                return HttpNotFound();
            }
            return View(xuatXu);
        }

        // POST: XuatXu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenQG")] XuatXu xuatXu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xuatXu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(xuatXu);
        }

        // GET: XuatXu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatXu xuatXu = db.XuatXu.Find(id);
            if (xuatXu == null)
            {
                return HttpNotFound();
            }
            return View(xuatXu);
        }

        // POST: XuatXu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            XuatXu xuatXu = db.XuatXu.Find(id);
            db.XuatXu.Remove(xuatXu);
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
