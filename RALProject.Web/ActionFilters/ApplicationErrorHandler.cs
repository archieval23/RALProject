using RALProject.Common.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RALProject.Web.ActionFilters
{
    public class ApplicationErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var customException = ExceptionMapping.Map(exception);

                if (customException.ExceptionHandled)
                {
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 500;
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                    filterContext.HttpContext.Response.Write(customException.Message.Replace("\r\n", " "));
                    filterContext.ExceptionHandled = true;
                }
                else
                {
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 200;
                    filterContext.ExceptionHandled = true;
                }

                return;
            }

            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
                return;

            string viewName = string.Empty;
            int httpCode = new HttpException(null, exception).GetHttpCode();
            if (httpCode == 500)
            {
                viewName = "ErrorPage";
            }
            else if (httpCode == 404)
            {
                viewName = "ErrorPage";
            }
            else if (httpCode == 401)
            {
                viewName = "ErrorPage";
            }

            //string controllerName = (string)filterContext.RouteData.Values["controller"];
            //string actionName = (string)filterContext.RouteData.Values["action"];
            filterContext.Result = new ViewResult
            {
                ViewName = viewName,
                MasterName = Master,
                TempData = filterContext.Controller.TempData,
            };

            filterContext.Controller.TempData["ErrorResult.Exception"] = CreateErrorTrace(exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = httpCode;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        public string CreateErrorTrace(Exception exception)
        {
            var message = string.Empty;

            message = "<p>" + exception.Message + "</p>";

            Exception innerException = exception.InnerException;

            while (innerException != null)
            {
                message = message + "<p>" + innerException.Message + "</p>";
                innerException = innerException.InnerException;
            }

            return message;
        }
    }
}
