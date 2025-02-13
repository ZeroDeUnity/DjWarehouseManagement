using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DjWarehouseManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 加载 log4net 配置
            var log4netConfig = new System.IO.FileInfo(Server.MapPath("~/log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(log4netConfig);

            // 获取配置的日志仓库
            ILoggerRepository repository = LogManager.GetRepository();
            foreach (var appender in repository.GetAppenders())
            {
                if (appender is AdoNetAppender adoNetAppender)
                {
                    // 从 web.config 中读取连接字符串
                    var connectionString = ConfigurationManager.ConnectionStrings["DjMySqlconnString"].ConnectionString;
                    adoNetAppender.ConnectionString = connectionString;

                    // 激活配置
                    adoNetAppender.ActivateOptions();
                }
            }

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
