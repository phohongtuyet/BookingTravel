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
    public class NoiSanXuatController : XacThucController
    {
        private CHXEEntities db = new CHXEEntities();

        // GET: NoiSanXuat
        public ActionResult Index()
        {
            return View(db.NoiSanXuat.ToList());
        }

        // GET: NoiSanXuat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoiSanXuat noiSanXuat = db.NoiSanXuat.Find(id);
            if (noiSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(noiSanXuat);
        }

        // GET: NoiSanXuat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NoiSanXuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenNoiSX")] NoiSanXuat noiSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.NoiSanXuat.Add(noiSanXuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noiSanXuat);
        }

        // GET: NoiSanXuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoiSanXuat noiSanXuat = db.NoiSanXuat.Find(id);
            if (noiSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(noiSanXuat);
        }

        // POST: NoiSanXuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenNoiSX")] NoiSanXuat noiSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noiSanXuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noiSanXuat);
        }

        // GET: NoiSanXuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoiSanXuat noiSanXuat = db.NoiSanXuat.Find(id);
            if (noiSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(noiSanXuat);
        }

        // POST: NoiSanXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NoiSanXuat noiSanXuat = db.NoiSanXuat.Find(id);
            db.NoiSanXuat.Remove(noiSanXuat);
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
