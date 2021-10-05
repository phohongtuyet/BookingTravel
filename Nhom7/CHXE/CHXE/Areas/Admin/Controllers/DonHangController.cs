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
    public class DonHangController : XacThucController
    {
        private CHXEEntities db = new CHXEEntities();

        // GET: DonHang
        public ActionResult Index()
        {
            var donHang = db.DonHang.Include(d => d.KhachHang).Include(d => d.NhanVien);
            return View(donHang.ToList());
        }

        
        // GET: DonHang/Create
        public ActionResult Create()
        {
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen");
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HovaTen");
            return View();
        }

        // POST: DonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NhanVien_ID,KhachHang_ID,SDTGiaoHang,NgayDatHang,DiaChiGiaoHang,TinhTrang")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHang.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen", donHang.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HovaTen", donHang.NhanVien_ID);
            return View(donHang);
        }

        // GET: DonHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHang.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen", donHang.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HovaTen", donHang.NhanVien_ID);
            return View(donHang);
        }

        // POST: DonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NhanVien_ID,KhachHang_ID,SDTGiaoHang,NgayDatHang,DiaChiGiaoHang,TinhTrang")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen", donHang.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HovaTen", donHang.NhanVien_ID);
            return View(donHang);
        }

        // GET: DonHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHang.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHang.Find(id);
            db.DonHang.Remove(donHang);
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
