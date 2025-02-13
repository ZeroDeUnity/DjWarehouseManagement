using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DjWarehouseManagement.Filters
{
    public class LoginFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            // 检查当前Action方法是否有AllowAnonymous特性
            bool allowAnonymous = filterContext.ActionDescriptor
               .GetCustomAttributes(typeof(AllowAnonymousAttribute), true)
               .Length > 0;
            if (allowAnonymous)
            {
                // 如果有AllowAnonymous特性，直接返回，不进行验证
                base.OnActionExecuting(filterContext);
                return;
            }

            // 尝试获取存储用户Id的Cookie
            HttpCookie userIdCookie = HttpContext.Current.Request.Cookies["Id"];
            // 尝试获取存储用户名的Cookie
            HttpCookie userNameCookie = HttpContext.Current.Request.Cookies["userName"];

            if (userIdCookie == null || userNameCookie == null)
            {
                // 如果Cookie不存在，重定向到登录页面（这里假设登录页面的路由名为"Login"，你需要根据实际情况修改）
                filterContext.Result = new RedirectResult("~/Login/Login_res");

                return;
            }

            // 可以在这里进行更多验证，比如验证Cookie中的数据格式等（可选操作）

            base.OnActionExecuting(filterContext);

        }
    }
}