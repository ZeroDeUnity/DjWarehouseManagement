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
    public class HomeController : Controller
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult old_Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult query()
        {
            ViewBag.Message = "Your query page.";

            return View();
        }

        public ActionResult product_Index()
        {
            ViewBag.Message = "Your product_Index page.";
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
        public string QueryInventoryData(string selectInput,string selectType, string selectColor)
        {
            string retStr = "";
            try
            {
                InventoryQueryBLL inventoryQueryBLL = new InventoryQueryBLL();

                retStr = inventoryQueryBLL.QueryInventoryData(selectInput, selectType, selectColor);
            }
            catch (Exception ex)
            {
                log.Error("查询库存数据", ex);
                return retStr;
            }

            return retStr;
        }

        public ActionResult exit_Index()
        {
            ViewBag.Message = "Your exit_Index page.";

            return View();
        }

        public ActionResult entry_Index()
        {
            ViewBag.Message = "Your entry_Index page.";

            return View();
        }

        public ActionResult detail()
        {
            ViewBag.Message = "Your detail page.";

            return View();
        }

        /// <summary>
        /// 查询入库批次数据
        /// </summary>
        /// <param name="entryTime"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryStockEntryBatchData(string entryTime)
        {
            string startTime = "";
            string endTime = "";
            string retStr = "";
            try
            {
                if (entryTime != "")
                {
                    startTime = entryTime + " 00:00:00";
                    endTime = entryTime + " 23:59:59";
                }

                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                retStr = stockEntryBLL.QueryStockEntryBatchData(startTime, endTime);
            }
            catch (Exception ex)
            {
                log.Error("查询入库批次数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 查询入库数据,根据入库批次Id查询
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryStockEntryDataByBatchId(string batchId)
        {
            string retStr = "";
            try
            {
                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                retStr = stockEntryBLL.QueryStockEntryDataByBatchId(batchId);
            }
            catch (Exception ex)
            {
                log.Error("查询入库数据,根据入库批次Id查询", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 新建入库批次数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpPost]
        public string AddStockEntryBatchData(string source,string date,string orderId)
        {
            string retStr = "";
            try
            {
                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                retStr = stockEntryBLL.AddStockEntryBatchData(source, date , orderId).ToString();
            }
            catch (Exception ex)
            {
                log.Error("新建入库批次数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 修改入库批次数据
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateStockEntryBatchData(string batchId,string source, string orderId, string date)
        {
            string retStr = "";
            try
            {
                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                retStr = stockEntryBLL.UpdateStockEntryBatchData(batchId, source,orderId, date).ToString();
            }
            catch (Exception ex)
            {
                log.Error("修改入库批次数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 根据输入信息查询产品信息
        /// </summary>
        /// <param name="selectProductType"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryProductInfoByInput(string selectProductType)
        {
            string retStr = "";
            try
            {
                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                retStr = stockEntryBLL.QueryProductInfoByInput(selectProductType);
            }
            catch (Exception ex)
            {
                log.Error("根据输入信息查询产品信息", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 根据输入信息查询货架信息,输入信息是货架名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryShelfInfoByInput(string input)
        {
            string retStr = "";
            try
            {
                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                retStr = stockEntryBLL.QueryShelfInfoByInput(input);
            }
            catch (Exception ex)
            {
                log.Error("根据输入信息查询货架信息,输入信息是货架名称", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 查询货架数据
        /// </summary>
        /// <param name="dataJson"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryShelfProductMappingData(string dataJson)
        {
            string retStr = "";
            try
            {
                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                retStr = stockEntryBLL.QueryShelfProductMappingData(dataJson);
            }
            catch (Exception ex)
            {

                log.Error("查询货架数据", ex);
                return retStr;
            }

            return retStr;
        }

        //QueryStockEntryDataByStockId
        [HttpPost]
        public string QueryStockEntryDataByStockId(string stockId)
        {
            StockEntryBLL stockEntryBLL = new StockEntryBLL();

            return stockEntryBLL.QueryStockEntryDataByStockId(stockId);
        }

        /// <summary>
        /// 新建入库数据
        /// </summary>
        /// <param name="dataJson"></param>
        /// <returns></returns>
        [HttpPost]
        public string AddStockEntryData(string dataJson)
        {
            string retStr = "";
            try
            {
                JObject data = JObject.Parse(dataJson);

                string stockId = data["stockId"]?.ToString();
                string batchId = data["batchId"]?.ToString();
                string productType = data["productType"]?.ToString();
                string productId = data["productId"]?.ToString();
                string productText = data["productText"]?.ToString();
                string actualSize = data["actualSize"]?.ToString();
                string quantity = data["quantity"]?.ToString();
                string explain = data["explain"]?.ToString();
                string shelfId = data["shelfId"]?.ToString();
                string shelf = data["shelf"]?.ToString();
                string shelfLayer = data["shelfLayer"]?.ToString();
                string nearShelf1 = data["nearShelf1"]?.ToString();
                string nearShelf1Id = data["nearShelf1Id"]?.ToString();
                string nearShelf2 = data["nearShelf2"]?.ToString();
                string nearShelf2Id = data["nearShelf2Id"]?.ToString();

                //下面可以继续对数据进行初步处理,处理完再交给BLL层处理

                //组装tb_stock_entry数据,把上面的数据都添加到jsonobject中
                JObject stockEntryData = new JObject();

                // 逐个添加字段
                stockEntryData["stockId"] = stockId;
                stockEntryData["batchId"] = batchId;
                stockEntryData["productType"] = productType;
                stockEntryData["productId"] = productId;
                stockEntryData["productText"] = productText;
                stockEntryData["actualSize"] = actualSize;
                stockEntryData["quantity"] = quantity;
                stockEntryData["explain"] = explain;
                stockEntryData["shelfId"] = shelfId;
                stockEntryData["shelf"] = shelf;
                stockEntryData["shelfLayer"] = shelfLayer;
                stockEntryData["nearShelf1"] = nearShelf1;
                stockEntryData["nearShelf1Id"] = nearShelf1Id;
                stockEntryData["nearShelf2"] = nearShelf2;
                stockEntryData["nearShelf2Id"] = nearShelf2Id;

                // 将 JObject 转换为字符串
                string stockEntryDataJson = stockEntryData.ToString();

                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                int insertCount = stockEntryBLL.AddStockEntryData(stockEntryDataJson);

                //创建json对象,返回处理信息
                JObject result = new JObject();

                if (insertCount > 0)
                {
                    result["insertCount"] = insertCount;
                    result["status"] = "success";
                }
                else
                {
                    result["insertCount"] = insertCount;
                    result["status"] = "fail";
                }
                retStr = result.ToString();
            }
            catch (Exception ex)
            {

                log.Error("删除入库数据", ex);
                return retStr;
            }

            return retStr;
        }

        //修改入库数据
        /// <summary>
        /// 修改入库数据
        /// </summary>
        /// <param name="dataJson"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateStockEntryData(string dataJson)
        {
            string retStr = "";
            try
            {

                JObject data = JObject.Parse(dataJson);

                string stockId = data["stockId"]?.ToString();
                string batchId = data["batchId"]?.ToString();
                string productType = data["productType"]?.ToString();
                string productId = data["productId"]?.ToString();
                string productText = data["productText"]?.ToString();
                string actualSize = data["actualSize"]?.ToString();
                string quantity = data["quantity"]?.ToString();
                string explain = data["explain"]?.ToString();
                string shelfId = data["shelfId"]?.ToString();
                string shelf = data["shelf"]?.ToString();
                string shelfLayer = data["shelfLayer"]?.ToString();
                string nearShelf1 = data["nearShelf1"]?.ToString();
                string nearShelf1Id = data["nearShelf1Id"]?.ToString();
                string nearShelf2 = data["nearShelf2"]?.ToString();
                string nearShelf2Id = data["nearShelf2Id"]?.ToString();

                //下面可以继续对数据进行初步处理,处理完再交给BLL层处理

                //组装tb_stock_entry数据,把上面的数据都添加到jsonobject中
                JObject stockEntryData = new JObject();

                // 逐个添加字段
                stockEntryData["stockId"] = stockId;
                stockEntryData["batchId"] = batchId;
                stockEntryData["productType"] = productType;
                stockEntryData["productId"] = productId;
                stockEntryData["productText"] = productText;
                stockEntryData["actualSize"] = actualSize;
                stockEntryData["quantity"] = quantity;
                stockEntryData["explain"] = explain;
                stockEntryData["shelfId"] = shelfId;
                stockEntryData["shelf"] = shelf;
                stockEntryData["shelfLayer"] = shelfLayer;
                stockEntryData["nearShelf1"] = nearShelf1;
                stockEntryData["nearShelf1Id"] = nearShelf1Id;
                stockEntryData["nearShelf2"] = nearShelf2;
                stockEntryData["nearShelf2Id"] = nearShelf2Id;

                // 将 JObject 转换为字符串
                string stockEntryDataJson = stockEntryData.ToString();

                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                int insertCount = stockEntryBLL.UpdateStockEntryData(stockEntryDataJson);

                //创建json对象,返回处理信息
                JObject result = new JObject();

                if (insertCount > 0)
                {
                    result["insertCount"] = insertCount;
                    result["status"] = "success";
                }
                else
                {
                    result["insertCount"] = insertCount;
                    result["status"] = "fail";
                }

                retStr = result.ToString();
            }
            catch (Exception ex)
            {
                log.Error("修改入库数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 删除入库数据
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteStockEntryData(string stockId)
        {
            StockEntryBLL stockEntryBLL = new StockEntryBLL();

            string retStr = "";
            try
            {
                retStr = stockEntryBLL.DeleteStockEntryDataById(int.Parse(stockId)).ToString();
            }
            catch (Exception ex)
            {
                log.Error("删除入库数据", ex);
                return retStr;
            }
            return retStr;
        }

        [HttpPost]
        public string QueryProductData(string selectInput)
        {
            InventoryQueryBLL inventoryQueryBLL = new InventoryQueryBLL();

            return inventoryQueryBLL.QueryProductData(selectInput);
        }

        //CreateProductData
        [HttpPost]
        public string CreateProductData(string productType,string color, string colorCode, string process, string printCode,string specification, string size)
        {
            InventoryQueryBLL inventoryQueryBLL = new InventoryQueryBLL();

            int insertCount = inventoryQueryBLL.CreateProductData(productType, color, colorCode, process, printCode, specification, size);

            //创建json对象,返回处理信息
            JObject result = new JObject();

            if (insertCount > 0)
            {
                result["insertCount"] = insertCount;
                result["status"] = "success";
            }
            else
            {
                result["insertCount"] = insertCount;
                result["status"] = "fail";
            }

            return result.ToString();

        }

    }
}