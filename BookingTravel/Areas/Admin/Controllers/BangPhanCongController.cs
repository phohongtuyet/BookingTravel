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
    public class BangPhanCongController : AuthController
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: BangPhanCong
        public ActionResult Index()
        {
            var bangPhanCong = db.BangPhanCong.Include(b => b.NhanVien).Include(b => b.Tour);
            return View(bangPhanCong.ToList());
        }

        // GET: BangPhanCong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangPhanCong bangPhanCong = db.BangPhanCong.Find(id);
            if (bangPhanCong == null)
            {
                return HttpNotFound();
            }
            return View(bangPhanCong);
        }

        // GET: BangPhanCong/Create
        public ActionResult Create()
        {

            ViewBag.NhanVien_ID = new SelectList(db.NhanVien.Select(x =>
                    new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.HoVaTen
                    }),
                "Value",
                "Text");


            //ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen");
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour");

            ModelState.AddModelError("UploadError", "");
            return View();
        }

        // POST: BangPhanCong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NhanVien_ID,Tour_ID,NgayBD,NgayKT,NhanVien_IDs")] BangPhanCong bangPhanCong)
        {
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen", bangPhanCong.NhanVien_ID);
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", bangPhanCong.Tour_ID);

            if (ModelState.IsValid)
            {
                // lọc tour theo id từ tour được chọn
                var tour = db.Tour.Include(b => b.BangPhanCong).Where(t => t.ID == bangPhanCong.Tour_ID);

                foreach (var item in tour)
                {
                    var tontai = db.BangPhanCong.Where(r => r.NhanVien_ID == bangPhanCong.NhanVien_ID && r.NgayKT > item.NgayBD).Any();

                    //kiểm tra tồn tại nhân viên đã có lịch chưa
                    if (tontai)
                    {
                        ModelState.AddModelError("UploadError", "Nhân viên không thể trùng thời gian hướng dẫn ở tour khác. Vui lòng chọn nhân viên khác để hướng dẫn Tour!!!");
                        return View(bangPhanCong);
                    }
                    else
                    {
                        //lưu ngày bắt đầu và kết thúc của tour vào bảng phân công
                        bangPhanCong.NgayBD = item.NgayBD;
                        bangPhanCong.NgayKT = item.NgayKT;

                        db.BangPhanCong.Add(bangPhanCong);
                    }
                }
                SetAlert("Thêm mới thành công", "success");
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bangPhanCong);
        }

        // GET: BangPhanCong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangPhanCong bangPhanCong = db.BangPhanCong.Find(id);
            if (bangPhanCong == null)
            {
                return HttpNotFound();
            }
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen", bangPhanCong.NhanVien_ID);
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", bangPhanCong.Tour_ID);
            ModelState.AddModelError("UploadError", "");

            return View(bangPhanCong);
        }

        // POST: BangPhanCong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NhanVien_ID,Tour_ID,NgayBD,NgayKT")] BangPhanCong bangPhanCong)
        {
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTen", bangPhanCong.NhanVien_ID);
            ViewBag.Tour_ID = new SelectList(db.Tour, "ID", "TenTour", bangPhanCong.Tour_ID);
            if (ModelState.IsValid)
            {
                // lọc tour theo id từ tour được chọn
                var tour = db.Tour.Include(b => b.BangPhanCong).Where(t => t.ID == bangPhanCong.Tour_ID);

                foreach (var item in tour)
                {
                    var tontai = db.BangPhanCong.Where(r => r.NhanVien_ID == bangPhanCong.NhanVien_ID && r.NgayBD >= item.NgayBD && r.NgayBD <= item.NgayBD).Any();

                    //kiểm tra tồn tại nhân viên đã có lịch chưa
                    if (tontai)
                    {
                        ModelState.AddModelError("UploadError", "Trùng thời gian hướng dẫn của nhân viên. Vui lòng chọn Tour khác!!!");
                        return View(bangPhanCong);
                    }
                    else
                    {
                        //lưu ngày bắt đầu và kết thúc của tour vào bảng phân công
                        bangPhanCong.NgayBD = item.NgayBD;
                        bangPhanCong.NgayKT = item.NgayKT;
                        db.Entry(bangPhanCong).State = EntityState.Modified;
                    }
                }
                SetAlert("Cập nhật thành công", "success");
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bangPhanCong);

        }

        // GET: BangPhanCong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangPhanCong bangPhanCong = db.BangPhanCong.Find(id);
            if (bangPhanCong == null)
            {
                return HttpNotFound();
            }
            return View(bangPhanCong);
        }

        // POST: BangPhanCong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BangPhanCong bangPhanCong = db.BangPhanCong.Find(id);
            db.BangPhanCong.Remove(bangPhanCong);
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