using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ArasMVC01.ActionFilter
{
    public class ShareActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session["innovator"] == null)
            {
                filterContext.Result = new RedirectResult("/");
            }
            base.OnActionExecuted(filterContext);
        }
    }
}