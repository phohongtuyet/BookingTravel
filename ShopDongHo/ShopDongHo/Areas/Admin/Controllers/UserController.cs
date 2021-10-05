using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDongHo.Areas.Admin.Controllers
{
    public class UserController : AuthController
    {
       
        public ActionResult DongHo()
        {
            return RedirectToAction("Index","DongHo");
        }

        public ActionResult KhachHang()
        {
            return RedirectToAction("Index", "KhachHang");
        }

        public ActionResult DatHang()
        {
            return RedirectToAction("Index", "DatHang");
        }

        public ActionResult DatHang_ChiTiet()
        {
            return RedirectToAction("Index", "DatHang_ChiTiet");
        }

        public ActionResult ChangePassword()
        {
            return RedirectToAction("ChangePassword", "NhanVien");
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}