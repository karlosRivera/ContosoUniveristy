using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Contoso.Web.Log;
using Microsoft.Practices.ServiceLocation;
using System.Text;

namespace MyFinance.Web.Helpers
{
public class UserTrackerLogAttribute : ActionFilterAttribute, IActionFilter
{

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (!filterContext.Controller.ViewData.ModelState.IsValid)
            return;

        var model = filterContext.ActionParameters.SingleOrDefault(ap => ap.Key == "model").Value;
        if (model != null)
        {
            // Found the model - add it to tempdata
            //filterContext.Controller.TempData.Add(TempDataKey, model);
        }
        base.OnActionExecuting(filterContext);
    }
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        var parametrs = filterContext.ActionDescriptor.GetParameters();
        var sel = filterContext.ActionDescriptor.GetSelectors();
        foreach (var p in parametrs)
        {
            //var d = p.ParameterName.SingleOrDefault();

          
            
        }
        if (filterContext.Controller.ViewData.Model != null)
        {
            var data = filterContext.Controller.ViewData.Model;
        }
        var actionDescriptor= filterContext.ActionDescriptor;
        string controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
        string actionName = actionDescriptor.ActionName;
        string userName = filterContext.HttpContext.User.Identity.Name.ToString();
        DateTime timeStamp = filterContext.HttpContext.Timestamp;
        string Id = string.Empty;
        if (filterContext.RouteData.Values["id"] != null)
        {
            Id = filterContext.RouteData.Values["id"].ToString();
        }      
        StringBuilder message = new StringBuilder();
        message.Append("UserName=");
        message.Append(userName + "|");
        message.Append("Controller=");
        message.Append(controllerName+"|");
        message.Append("Action=");
        message.Append(actionName + "|");
        message.Append("TimeStamp=");
        message.Append(timeStamp.ToString() + "|");
        if (!string.IsNullOrEmpty(Id))
        {
            message.Append("RouteId=");
            message.Append(Id);
        }
        var log=ServiceLocator.Current.GetInstance<ILoggingService>();
        log.Log(message.ToString());
        base.OnActionExecuted(filterContext);
    }
}
}