using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingTravel.Models;

namespace BookingTravel.Controllers
{
    public class HomeController : Controller
    {
        BookingTravelEntities db = new BookingTravelEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Review()
        {
            var baiViet = db.BaiViet.Where(r => r.KiemDuyet == 1).ToList();
            return View(baiViet);
        }

        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {
            string tukhoa = collection["Tukhoa"].ToString();
            var baiViet = db.BaiViet.Where(r => r.KiemDuyet == 1 && (r.TieuDe.Contains(tukhoa) || r.NoiDung.Contains(tukhoa))).ToList();
            return View(baiViet);
        }
        public ActionResult New()
        {
            var baiViet = db.BaiViet.Where(r => r.KiemDuyet == 1).OrderByDescending(r => r.NgayDang).ToList();
            return View(baiViet);
        }

        public ActionResult Detail(int id)
        {
            // Lấy thông tin bài viết
            var baiViet = db.BaiViet.Where(r => r.KiemDuyet == 1 && r.ID == id).SingleOrDefault();

            // Tăng lượt xem
            // Chính sách: Mỗi máy chỉ tăng 1 lượt xem
            if (Object.Equals(Session["DaXem" + baiViet.ID], null))
            {
                BaiViet bv = db.BaiViet.Find(id);
                bv.LuotXem = bv.LuotXem + 1;
                db.Entry(bv).State = EntityState.Modified;
                db.SaveChanges();

                // Đánh dấu là đã xem
                Session["DaXem" + bv.ID] = 1;
            }

            // Hiển thị bài viết ra View
            return View(baiViet);
        }

    }
}