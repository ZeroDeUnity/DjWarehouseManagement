using DjBLL;
using DjDAL;
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
    public class ProductExitController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductExitController));

        // GET: Index
        public ActionResult Index()
        {
            // 获取URL参数
            string dateInput = Request.QueryString["dateInput"];

            // 将参数传递给视图
            ViewBag.dateInput = dateInput;
            return View();
        }

        // GET: Exit_Index
        public ActionResult Exit_Index()
        {
            // 获取URL参数
            string BatchId = Request.QueryString["BatchId"];
            string destination = Request.QueryString["destination"];
            string dateInput = Request.QueryString["dateInput"];

            // 将参数传递给视图
            ViewBag.BatchId = BatchId;
            ViewBag.destination = destination;
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
            string destination = Request.QueryString["destination"];
            string orderId = Request.QueryString["orderId"];
            string dateInput = Request.QueryString["dateInput"];

            // 将参数传递给视图
            ViewBag.BatchId = BatchId;
            ViewBag.destination = destination;
            ViewBag.orderId = orderId;
            ViewBag.dateInput = dateInput;
            return View();
        }
        // GET: Exit_Index_Add
        public ActionResult Exit_Index_Add()
        {
            // 获取URL参数
            string BatchId = Request.QueryString["BatchId"];
            string destination = Request.QueryString["destination"];
            string dateInput = Request.QueryString["dateInput"];

            // 将参数传递给视图
            ViewBag.BatchId = BatchId;
            ViewBag.destination = destination;
            ViewBag.dateInput = dateInput;
            return View();
        }

        // GET: Exit_Index_Update
        public ActionResult Exit_Index_Update()
        {
            // 获取URL参数
            string BatchId = Request.QueryString["BatchId"];
            string destination = Request.QueryString["destination"];
            string dateInput = Request.QueryString["dateInput"];
            string stockId = Request.QueryString["stockId"];

            // 将参数传递给视图
            ViewBag.BatchId = BatchId;
            ViewBag.destination = destination;
            ViewBag.dateInput = dateInput;
            ViewBag.stockId = stockId;
            return View();
        }

        /// <summary>
        /// 查询出库批次数据
        /// </summary>
        /// <param name="exitTime"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryStockExitBatchData(string exitTime)
        {
            string startTime = "";
            string endTime = "";
            string retStr = "";
            try
            {
                if (exitTime != "")
                {
                    startTime = exitTime + " 00:00:00";
                    endTime = exitTime + " 23:59:59";
                }

                StockExitBLL stockExitBLL = new StockExitBLL();

                TableData tableData = new TableData();

                tableData = stockExitBLL.QueryStockExitBatchTableData(startTime, endTime);

                // 直接访问静态类的属性来进行序列化
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);

            }
            catch (Exception ex)
            {
                log.Error("查询出库批次数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 新建出库批次数据
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="orderId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpPost]
        public string AddStockExitBatchData(string destination, string orderId, string date)
        {
            string retStr = "";
            try
            {
                StockExitBLL stockExitBLL = new StockExitBLL();
                retStr = stockExitBLL.AddStockExitBatchData(destination, orderId, date).ToString();
            }
            catch (Exception ex)
            {
                log.Error("新建出库批次数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 修改入库批次数据
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="destination"></param>
        /// <param name="orderId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateStockExitBatchData(string batchId, string destination, string orderId, string date)
        {
            string retStr = "";
            try
            {
                StockExitBLL stockExitBLL = new StockExitBLL();

                retStr = stockExitBLL.UpdateStockExitBatchData(batchId, destination, orderId, date).ToString();
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
        public string QueryStockExitDataByBatchId(string batchId)
        {
            string retStr = "";
            try
            {
                StockExitBLL stockExitBLL = new StockExitBLL();
                TableData tableData = new TableData();

                tableData = stockExitBLL.QueryStockExitTableDataByBatchId(batchId);

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
        /// 查询出库数据,根据出库Id查询
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryStockExitDataByStockId(string stockId)
        {
            string retStr = "";
            try
            {
                StockExitBLL stockExitBLL = new StockExitBLL();

                retStr = stockExitBLL.QueryStockExitDataByStockId(stockId);
            }
            catch (Exception ex)
            {
                log.Error("查询出库数据,根据出库Id查询", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 删除出库数据
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteStockExitData(string stockId)
        {
            string retStr = "";
            try
            {
                StockExitBLL stockExitBLL = new StockExitBLL();

                retStr = stockExitBLL.DeleteStockExitDataById(int.Parse(stockId)).ToString();
            }
            catch (Exception ex)
            {
                log.Error("删除出库数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 新建出库数据
        /// </summary>
        /// <param name="dataJson"></param>
        /// <returns></returns>
        [HttpPost]
        public string AddStockExitData(string dataJson)
        {
            string retStr = "";
            try
            {
                JObject data = JObject.Parse(dataJson);

                string stockId = data["stockId"]?.ToString();
                string batchId = data["batchId"]?.ToString();
                string selectedProductType = data["selectedProductType"]?.ToString();
                string selectedProduct = data["selectedProduct"]?.ToString();
                string actualSize = data["actualSize"]?.ToString();
                string quantityOut = data["quantityOut"]?.ToString();
                string now_productQuantity = data["now_productQuantity"]?.ToString();

                //下面可以继续对数据进行初步处理,处理完再交给BLL层处理

                //组装tb_stock_exit数据,把上面的数据都添加到jsonobject中
                JObject stockExitData = new JObject();

                // 逐个添加字段
                stockExitData["stockId"] = stockId;
                stockExitData["batchId"] = batchId;
                stockExitData["selectedProductType"] = selectedProductType;
                stockExitData["selectedProduct"] = selectedProduct;
                stockExitData["actualSize"] = actualSize;
                stockExitData["quantityOut"] = quantityOut;
                stockExitData["now_productQuantity"] = now_productQuantity;

                // 将 JObject 转换为字符串
                string stockExitDataJson = stockExitData.ToString();

                StockExitBLL stockExitBLL = new StockExitBLL();

                int insertCount = stockExitBLL.AddStockExitData(stockExitDataJson);

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
                log.Error("新建出库数据", ex);
                return retStr;
            }


            return retStr;
        }

        /// <summary>
        /// 根据输入信息查询产品信息
        /// </summary>
        /// <param name="selectProductType"></param>
        /// <param name="now_stockViewStatus"></param>
        /// <param name="now_mappingId"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryProductInfoByInput(string selectProductType, string now_stockViewStatus, string now_mappingId)
        {
            string retStr = "";
            try
            {
                StockExitBLL stockExitBLL = new StockExitBLL();

                retStr = stockExitBLL.QueryProductInfoByInput(selectProductType, now_stockViewStatus, now_mappingId);
            }
            catch (Exception ex)
            {
                log.Error("根据输入信息查询产品信息", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 查询产品状态
        /// </summary>
        /// <param name="dataJson"></param>
        /// <returns></returns>
        [HttpPost]
        public string queryProductStatus(string dataJson)
        {
            string retStr = "";
            try
            {
                StockExitBLL stockExitBLL = new StockExitBLL();

                retStr = stockExitBLL.queryProductStatus(dataJson);
            }
            catch (Exception ex)
            {
                log.Error("查询产品状态", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 更新出库数据
        /// </summary>
        /// <param name="dataJson"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateStockExitData(string dataJson)
        {
            string retStr = "";
            try
            {

                JObject data = JObject.Parse(dataJson);

                string stockExitId = data["stockExitId"]?.ToString();
                string stockId = data["stockId"]?.ToString();
                string batchId = data["batchId"]?.ToString();
                string selectedProductType = data["selectedProductType"]?.ToString();
                string selectedProduct = data["selectedProduct"]?.ToString();
                string actualSize = data["actualSize"]?.ToString();
                string quantityOut = data["quantityOut"]?.ToString();
                string now_productQuantity = data["now_productQuantity"]?.ToString();

                //下面可以继续对数据进行初步处理,处理完再交给BLL层处理

                //组装tb_stock_exit数据,把上面的数据都添加到jsonobject中
                JObject stockExitData = new JObject();

                // 逐个添加字段
                stockExitData["stockExitId"] = stockExitId;
                stockExitData["stockId"] = stockId;
                stockExitData["batchId"] = batchId;
                stockExitData["selectedProductType"] = selectedProductType;
                stockExitData["selectedProduct"] = selectedProduct;
                stockExitData["actualSize"] = actualSize;
                stockExitData["quantityOut"] = quantityOut;
                stockExitData["now_productQuantity"] = now_productQuantity;

                // 将 JObject 转换为字符串
                string stockExitDataJson = stockExitData.ToString();

                StockExitBLL stockExitBLL = new StockExitBLL();

                int updateCount = stockExitBLL.UpdateStockExitData(stockExitDataJson);

                //创建json对象,返回处理信息
                JObject result = new JObject();

                if (updateCount > 0)
                {
                    result["updateCount"] = updateCount;
                    result["status"] = "success";
                }
                else
                {
                    result["updateCount"] = updateCount;
                    result["status"] = "fail";
                }

                retStr = result.ToString();
            }
            catch (Exception ex)
            {
                log.Error("更新出库数据", ex);
                return retStr;
            }

            return retStr;
        }
    }
}