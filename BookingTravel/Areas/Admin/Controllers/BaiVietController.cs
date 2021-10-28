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
    public class BaiVietController : AuthController
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: BaiViet
        public ActionResult Index()
        {
            var baiViet = db.BaiViet.Include(b => b.NhanVien);
            return View(baiViet.ToList());
        }


        // GET: Admin/BaiViet/Approved/1
        public ActionResult Approved(int id)
        {
            BaiViet bv = db.BaiViet.Find(id);
            bv.KiemDuyet = System.Convert.ToByte(1 - bv.KiemDuyet); // 1 -> 0 và 0 -> 1
            db.Entry(bv).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/BaiViet/CommentStatus/1
        public ActionResult CommentStatus(int id)
        {
            BaiViet bv = db.BaiViet.Find(id);
            bv.TrangThaiBinhLuan = System.Convert.ToByte(1 - bv.TrangThaiBinhLuan); // 1 -> 0 và 0 -> 1
            db.Entry(bv).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
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
        [ValidateInput(false)]

        public ActionResult Create([Bind(Include = "ID,NhanVien_ID,TieuDe,TomTat,NoiDung,NgayDang,LuotXem,KiemDuyet,TrangThaiBinhLuan")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                baiViet.NhanVien_ID = Convert.ToInt32(Session["MaNhanVien"]);
                baiViet.NgayDang = DateTime.Now;
                baiViet.LuotXem = 0;

                if (Convert.ToBoolean(Session["Quyen"]) == true)
                    baiViet.KiemDuyet = 1;
                else
                    baiViet.KiemDuyet = 0;

                if (baiViet.TrangThaiBinhLuan.HasValue)
                    baiViet.TrangThaiBinhLuan = 1;
                else
                    baiViet.TrangThaiBinhLuan = 0;

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
        [ValidateInput(false)]

        public ActionResult Edit([Bind(Include = "ID,NhanVien_ID,TieuDe,TomTat,NoiDung,NgayDang,LuotXem,KiemDuyet,TrangThaiBinhLuan")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                BaiViet bv = db.BaiViet.Find(baiViet.ID);

                bv.NhanVien_ID = baiViet.NhanVien_ID;
                bv.TieuDe = baiViet.TieuDe;
                bv.TomTat = baiViet.TomTat;
                bv.NoiDung = baiViet.NoiDung;
                bv.NgayDang = DateTime.Now;
                bv.LuotXem = baiViet.LuotXem;


                bv.NhanVien_ID = bv.NhanVien_ID;

                if (Session["Quyen"].ToString().ToLower() == "admin")
                    bv.KiemDuyet = 1;
                else
                    bv.KiemDuyet = 0;

                if (baiViet.TrangThaiBinhLuan.HasValue)
                    bv.TrangThaiBinhLuan = 1;
                else
                    bv.TrangThaiBinhLuan = 0;


                db.Entry(bv).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");

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
            SetAlert("Xóa thành công", "success");

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
