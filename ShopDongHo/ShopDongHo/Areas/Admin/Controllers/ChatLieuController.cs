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
    public class ChatLieuController : AuthController
    {
        private ShopDongHoEntities db = new ShopDongHoEntities();

        // GET: ChatLieu
        public ActionResult Index()
        {
            return View(db.ChatLieu.ToList());
        }

        // GET: ChatLieu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChatLieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenChatLieu")] ChatLieu chatLieu)
        {
            if (ModelState.IsValid)
            {
                db.ChatLieu.Add(chatLieu);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return RedirectToAction("Index");
            }

            return View(chatLieu);
        }

        // GET: ChatLieu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieu.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // POST: ChatLieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenChatLieu")] ChatLieu chatLieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatLieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chatLieu);
        }

        // GET: ChatLieu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieu.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // POST: ChatLieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChatLieu chatLieu = db.ChatLieu.Find(id);
            db.ChatLieu.Remove(chatLieu);
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
