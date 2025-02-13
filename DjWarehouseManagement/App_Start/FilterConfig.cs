using System.Web;
using System.Web.Mvc;

namespace DjWarehouseManagement
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // 注册自定义的登录验证过滤器
            filters.Add(new Filters.LoginFilter());
        }
    }
}
