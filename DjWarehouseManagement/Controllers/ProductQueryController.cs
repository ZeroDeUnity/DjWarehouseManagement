using DjBLL;
using DjDAL.Utility;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DjDAL.Utility.TableData_Utility;

namespace DjWarehouseManagement.Controllers
{
    public class ProductQueryController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductQueryController));

        // GET: ProductQuery
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询库存数据
        /// </summary>
        /// <param name="selectInput"></param>
        /// <param name="selectType"></param>
        /// <param name="selectColor"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryInventoryData(string selectInput, string selectType, string selectColor)
        {
            string retStr = "";
            try
            {
                InventoryQueryBLL inventoryQueryBLL = new InventoryQueryBLL();

                TableData tableData = new TableData();

                tableData = inventoryQueryBLL.QueryInventoryTableData(selectInput, selectType, selectColor);

                // 直接访问静态类的属性来进行序列化
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("查询库存数据", ex);
                return retStr;
            }

            return retStr;
        }



    }
}