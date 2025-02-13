using DjBLL;
using DjDAL;
using DjDAL.Utility;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DjWarehouseManagement.Controllers
{
    public class IndexController : Controller
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(IndexController));

        public ActionResult Index()
        {
            return View();
        }


    }
}