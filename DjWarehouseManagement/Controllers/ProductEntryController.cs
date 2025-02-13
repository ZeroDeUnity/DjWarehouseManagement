using DjBLL;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DjDAL.Utility.TableData_Utility;

namespace DjWarehouseManagement.Controllers
{
    public class ProductEntryController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductEntryController));

        // GET: Index
        public ActionResult Index()
        {
            // 获取URL参数
            string dateInput = Request.QueryString["dateInput"];

            // 将参数传递给视图
            ViewBag.dateInput = dateInput;
            return View();
        }

        // GET: Entry_Index
        public ActionResult Entry_Index()
        {
            // 获取URL参数
            string BatchId = Request.QueryString["BatchId"];
            string source = Request.QueryString["source"];
            string dateInput = Request.QueryString["dateInput"];

            // 将参数传递给视图
            ViewBag.BatchId = BatchId;
            ViewBag.source = source;
            ViewBag.dateInput = dateInput;
            return View();
        }

        // GET: Index_Add
        public ActionResult Index_Add()
        {
            // 获取URL参数
            string dateInput = Request.QueryString["dateInput"];

            // 将参数传递给视图
            ViewBag.dateInput = dateInput;
            return View();
        }

        // GET: Index_Update
        public ActionResult Index_Update()
        {
            // 获取URL参数
            string BatchId = Request.QueryString["BatchId"];
            string source = Request.QueryString["source"];
            string orderId = Request.QueryString["orderId"];
            string dateInput = Request.QueryString["dateInput"];

            // 将参数传递给视图
            ViewBag.BatchId = BatchId;
            ViewBag.source = source;
            ViewBag.orderId = orderId;
            ViewBag.dateInput = dateInput;
            return View();
        }
        // GET: Entry_Index_Add
        public ActionResult Entry_Index_Add()
        {
            // 获取URL参数
            string BatchId = Request.QueryString["BatchId"];
            string source = Request.QueryString["source"];
            string dateInput = Request.QueryString["dateInput"];

            // 将参数传递给视图
            ViewBag.BatchId = BatchId;
            ViewBag.source = source;
            ViewBag.dateInput = dateInput;
            return View();
        }

        // GET: Entry_Index_Update
        public ActionResult Entry_Index_Update()
        {
            // 获取URL参数
            string BatchId = Request.QueryString["BatchId"];
            string source = Request.QueryString["source"];
            string dateInput = Request.QueryString["dateInput"];
            string stockId = Request.QueryString["stockId"];

            // 将参数传递给视图
            ViewBag.BatchId = BatchId;
            ViewBag.source = source;
            ViewBag.dateInput = dateInput;
            ViewBag.stockId = stockId;
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

                TableData tableData = new TableData();

                tableData = stockEntryBLL.QueryStockEntryBatchTableData(startTime, endTime);

                // 直接访问静态类的属性来进行序列化
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("查询入库批次数据", ex);
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
        public string AddStockEntryBatchData(string source, string date, string orderId)
        {
            string retStr = "";
            try
            {
                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                retStr = stockEntryBLL.AddStockEntryBatchData(source, date, orderId).ToString();
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
        public string UpdateStockEntryBatchData(string batchId, string source, string orderId, string date)
        {
            string retStr = "";
            try
            {
                StockEntryBLL stockEntryBLL = new StockEntryBLL();

                retStr = stockEntryBLL.UpdateStockEntryBatchData(batchId, source, orderId, date).ToString();
            }
            catch (Exception ex)
            {
                log.Error("修改入库批次数据", ex);
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

                TableData tableData = new TableData();

                tableData = stockEntryBLL.QueryStockEntryTableDataByBatchId(batchId);

                // 直接访问静态类的属性来进行序列化
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("查询入库数据,根据入库批次Id查询", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 查询入库数据,根据入库数据ID
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryStockEntryDataByStockId(string stockId)
        {
            StockEntryBLL stockEntryBLL = new StockEntryBLL();

            string retStr = "";
            try
            {
                retStr = stockEntryBLL.QueryStockEntryDataByStockId(stockId);
            }
            catch (Exception ex)
            {
                log.Error("查询入库数据,根据入库数据ID", ex);
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

    }
}