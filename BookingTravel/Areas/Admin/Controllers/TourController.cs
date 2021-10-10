using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingTravel.Models;

namespace BookingTravel.Areas.Admin.Controllers
{
    public class TourController : AuthController
    {
        private BookingTravelEntities db = new BookingTravelEntities();

        // GET: Tour
        public ActionResult Index()
        {
            var tour = db.Tour.Include(t => t.ChiTietPhuongTien).Include(h => h.HinhAnh).Include(ct => ct.ChiTietDiaDiemThamQuan);
            return View(tour.ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetLocation(int Id)
        {
            var diadanh = db.DiaDiemThamQuan.Where(d => d.Tinh == Id).Select(d => new { Id = d.ID, Name = d.TenDiaDanh }).ToList();
            return Json(new { diadanh = diadanh }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Approved(int id)
        {
            Tour t = db.Tour.Find(id);
            t.TrangThai = System.Convert.ToByte(1 - t.TrangThai); // 1 -> 0 và 0 -> 1
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Tour/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tour.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Tour/Create
        public ActionResult Create()
        {
            ViewBag.DiaDiemThamQuan_ID = new SelectList(db.DiaDiemThamQuan, "ID", "TenDiaDanh");
            ViewBag.PhuongTien_ID = new SelectList(db.PhuongTien, "ID", "TenPhuongTien");
            ModelState.AddModelError("UploadError", "");
            return View();
        }

        // POST: Tour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,TenTour,LoaiTour,NoiKhoiHanh,NgayBD,NgayKT,DonGia,SoLuong,TrangThai,DuLieuHinhAnh,selectedLocation,selectedTranpost")] Tour tour)

        {
            ViewBag.DiaDiemThamQuan_ID = new SelectList(db.DiaDiemThamQuan, "ID", "TenDiaDanh", tour.ChiTietDiaDiemThamQuan);
            ViewBag.PhuongTien_ID = new SelectList(db.PhuongTien, "ID", "TenPhuongTien", tour.ChiTietPhuongTien);

            if (ModelState.IsValid)
            {

                tour.TrangThai = 0;
                db.Tour.Add(tour);
                db.SaveChanges();

                // Upload ảnh 
                if (!Object.Equals(tour.DuLieuHinhAnh, null))
                {

                    string folder = "Storage/";
                    foreach (var n in tour.DuLieuHinhAnh)
                    { 
                        string fileName = DateTime.Now.ToFileTime() + "_" + n.FileName;
                        string fileExtension = Path.GetExtension(n.FileName).ToLower();

                        // Kiểm tra kiểu
                        var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        if (!fileTypeSupported.Contains(fileExtension))
                        {
                            ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                            return View(tour);
                        }
                        else if (n.ContentLength > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                            return View(tour);
                        }
                        else
                        {
                            string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
                            n.SaveAs(filePath);

                            // Cập nhật đường dẫn vào CSDL                         
                            var images = new HinhAnh
                            {
                                HinhAnh1 = folder + fileName,
                                Tour_ID = tour.ID
                            };
                            db.HinhAnh.Add(images);
                            db.SaveChanges();                           
                        }
                    }                 
                }
                else
                {
                    ModelState.AddModelError("UploadError", "Hình ảnh bìa không được bỏ trống!");
                    return View(tour);
                }

                // them chi tiet phuong tien
                foreach (var n in tour.selectedTranpost)
                {
                    if (n > 0)
                    {
                        var transpots = new ChiTietPhuongTien
                        {
                            PhuongTien_ID = n,
                            Tour_ID = tour.ID
                        };
                        db.ChiTietPhuongTien.Add(transpots);
                        db.SaveChanges();
                    }

                }
                // them chi tiet dia diem tham quan
                foreach (var tn in tour.selectedLocation)
                {
                    if (tn > 0)
                    {
                        var location = new ChiTietDiaDiemThamQuan
                        {
                            DiaDiemThamQuan_ID = tn,
                            Tour_ID = tour.ID
                        };
                        db.ChiTietDiaDiemThamQuan.Add(location);
                        db.SaveChanges();
                    }

                }

                SetAlert("Thêm mới thành công", "success");
                return RedirectToAction("Index");
            }
            return View(tour);
        }

        // GET: Tour/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tour.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhuongTien_ID = new SelectList(db.PhuongTien, "ID", "TenPhuongTien", tour.ChiTietPhuongTien);
            return View(tour);
        }

        // POST: Tour/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PhuongTien_ID,TenTour,LoaiTour,NoiKhoiHanh,NgayBD,NgayKT,DonGia,SoLuong,TrangThai,MoTa")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PhuongTien_ID = new SelectList(db.PhuongTien, "ID", "TenPhuongTien", tour.ChiTietPhuongTien);
            return View(tour);
        }

        // GET: Tour/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tour.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tour.Find(id);
            db.Tour.Remove(tour);
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
