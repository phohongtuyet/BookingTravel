using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopDongHo.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
       
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (Session["MaNhanVien"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Home" }));
            }
            else
            {
                if (Session["Quyen"].ToString().ToLower() == "false")
                {
                    if (controllerName != "KhachHang" && controllerName != "DongHo" && controllerName != "DatHang" && controllerName != "DatHang_ChiTiet")
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Unauthorized", controller = "Home" }));
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if(type=="success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}