using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace CHXE.Areas.Admin.Controllers
{
    public class XacThucController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (Session["MaNhanVien"] == null )
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Home" }));
            else
            {
                if (Session["Quyen"].ToString().ToLower() == "false")
                {
                    if (controllername != "KhachHang" && controllername != "Xe" && controllername != "DonHang" && controllername != "ChiTietDonHang" )
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Unauthorized", Controller = "Home" }));
                    }

                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}