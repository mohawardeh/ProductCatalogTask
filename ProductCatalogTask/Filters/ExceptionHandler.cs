using ProductCatalogTask.Models.Core;
using ProductCatalogTask.Models.Core.Domain;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProductCatalogTask.Filters
{
    public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        private readonly IUnitOfWork unitOfWork;

        public ExceptionHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void OnException(ExceptionContext filterContext)
        {
            try
            {
                ExceptionRecord exr = new ExceptionRecord();
                if(ClaimsPrincipal.Current.Identity.IsAuthenticated)
                    exr.UserName = ClaimsPrincipal.Current.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name).Value;
                else
                    exr.UserName = "Guest";
                exr.ControllerName = (string)filterContext.RouteData.Values["controller"];
                exr.ActionName = (string)filterContext.RouteData.Values["action"];
                exr.ExceptionMessage = filterContext.Exception.Message;
                exr.ExceptionDate =  DateTime.Now;
                string param = "";
                if (filterContext.HttpContext.Request.HttpMethod == "POST")
                {
                    foreach (var key in filterContext.HttpContext.Request.Form.AllKeys)
                    {
                        if (key != "__RequestVerificationToken" && key != "form")
                        {
                            param += key;
                            param += "=";
                            param += filterContext.HttpContext.Request.Form[key];
                            param += ",";
                        }
                    }
                }
                else if (filterContext.HttpContext.Request.HttpMethod == "GET")
                {
                    foreach (var key in filterContext.RouteData.Values.Keys)
                    {
                        if (key != "controller" && key != "action")
                        {
                            param += key;
                            param += "=";
                            param += filterContext.RouteData.Values[key];
                            param += ",";
                        }
                    }
                }
                unitOfWork.Exceptions.Add(exr);
                unitOfWork.Complete();
                filterContext.ExceptionHandled = true;
                if (filterContext.HttpContext.Request.Headers.AllKeys.ToList().Exists(c => c == "X-Requested-With"))
                {
                    if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                        filterContext.Result = new JsonResult
                        {
                            Data = new { success = false, error = filterContext.Exception.ToString() },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                }
                else
                    filterContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary(new { action = "ViewException", controller = "Home" }));
            }
            catch (Exception e)
            {
                ExceptionRecord exr = new ExceptionRecord();
                exr.ExceptionMessage = "exception in exception handler:" + e.Message + " StackTrace: " + e.StackTrace;
                exr.ControllerName = (string)filterContext.RouteData.Values["controller"];
                exr.ActionName = (string)filterContext.RouteData.Values["action"];
                exr.ExceptionDate = DateTime.Now;
                unitOfWork.Exceptions.Add(exr);
                unitOfWork.Complete();
            }
        }

    }
}