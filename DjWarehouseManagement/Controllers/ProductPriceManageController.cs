using DjBLL;
using DjDAL.Utility;
using DjModels.Entity;
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
    public class ProductPriceManageController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductPriceManageController));

        // GET: ProductPriceManage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index_Add()
        {
            // 获取URL参数
            string Id = Request.QueryString["Id"];
            string productTypeId = Request.QueryString["productTypeId"];
            string typeName = Request.QueryString["typeName"];
            string description = Request.QueryString["description"];
            string salePrice = Request.QueryString["salePrice"];
            string offerPrice = Request.QueryString["offerPrice"];

            // 将参数传递给视图
            ViewBag.Id = Id;
            ViewBag.productTypeId = productTypeId;
            ViewBag.typeName = typeName;
            ViewBag.description = description;
            ViewBag.salePrice = salePrice;
            ViewBag.offerPrice = offerPrice;
            return View();
        }

        public ActionResult Index_Update()
        {
            // 获取URL参数
            string Id = Request.QueryString["Id"];
            string productTypeId = Request.QueryString["productTypeId"];
            string typeName = Request.QueryString["typeName"];
            string description = Request.QueryString["description"];
            string salePrice = Request.QueryString["salePrice"];
            string offerPrice = Request.QueryString["offerPrice"];
            string updateTime = Request.QueryString["updateTime"];

            // 将参数传递给视图
            ViewBag.Id = Id;
            ViewBag.productTypeId = productTypeId;
            ViewBag.typeName = typeName;
            ViewBag.description = description;
            ViewBag.salePrice = salePrice;
            ViewBag.offerPrice = offerPrice;
            ViewBag.updateTime = updateTime;
            return View();
        }

        public ActionResult Index_Add_ProductType()
        {
            return View();
        }

        public ActionResult Index_Update_ProductType()
        {
            // 获取URL参数
            string Id = Request.QueryString["Id"];
            string productTypeId = Request.QueryString["productTypeId"];
            string typeName = Request.QueryString["typeName"];
            string description = Request.QueryString["description"];
            string salePrice = Request.QueryString["salePrice"];
            string offerPrice = Request.QueryString["offerPrice"];

            // 将参数传递给视图
            ViewBag.Id = Id;
            ViewBag.productTypeId = productTypeId;
            ViewBag.typeName = typeName;
            ViewBag.description = description;
            ViewBag.salePrice = salePrice;
            ViewBag.offerPrice = offerPrice;
            return View();
        }

        public ActionResult Price_History_Index()
        {
            // 获取URL参数
            string productTypeId = Request.QueryString["productTypeId"];

            // 将参数传递给视图
            ViewBag.productTypeId = productTypeId;
            return View();
        }

        //QueryProductPriceData
        /// <summary>
        /// 查询产品价格数据
        /// </summary>
        /// <param name="selectInput"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryProductPriceTableData(string selectInput)
        {
            string retStr = "";
            try
            {
                PriceManageBLL priceManageBLL = new PriceManageBLL();
                TableData tableData = new TableData();
                tableData = priceManageBLL.QueryProductPriceTableData(selectInput);
                retStr = JsonConvert.SerializeObject(tableData,Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("查询产品价格数据 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 查询产品类型数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string QueryProductTypeData()
        {
            string retStr = "";
            try
            {
                PriceManageBLL priceManageBLL = new PriceManageBLL();
                TableData tableData = new TableData();
                tableData = priceManageBLL.QueryProductTypeData();
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("查询产品类型数据 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 查询所有产品类型数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string QueryAllProductTypeData()
        {
            string retStr = "";
            try
            {
                PriceManageBLL priceManageBLL = new PriceManageBLL();
                TableData tableData = new TableData();
                tableData = priceManageBLL.QueryAllProductTypeData();
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("查询所有产品类型数据 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 查询历史价格列表
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryProductPriceHistoryTableData(string productTypeId)
        {
            string retStr = "";
            try
            {
                PriceManageBLL priceManageBLL = new PriceManageBLL();
                TableData tableData = new TableData();
                tableData = priceManageBLL.QueryProductPriceHistoryTableData(productTypeId);
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("查询历史价格列表 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 新建产品类型数据
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpPost]
        public string AddProductTypeData(string typeName, string description)
        {
            string retStr = "";
            try
            {
                List<tb_product_type_Entity> tb_Product_Type_Entitys = new List<tb_product_type_Entity>();

                tb_product_type_Entity tb_Product_Type_Entity = new tb_product_type_Entity();
                tb_Product_Type_Entity.typeName = typeName;
                tb_Product_Type_Entity.description = description;
                tb_Product_Type_Entity.status = 1;
                tb_Product_Type_Entity.createUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                tb_Product_Type_Entity.createTime = DateTime.Now;
                tb_Product_Type_Entity.updateUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                tb_Product_Type_Entity.updateTime = DateTime.Now;

                tb_Product_Type_Entitys.Add(tb_Product_Type_Entity);

                PriceManageBLL priceManageBLL = new PriceManageBLL();

                TableData tableData = new TableData();

                tableData = priceManageBLL.AddProductTypeData(tb_Product_Type_Entitys);

                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("新建产品类型数据 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 修改产品类型数据
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateProductTypeData(string Id, string typeName, string description)
        {
            string retStr = "";
            try
            {
                tb_product_type_Entity tb_Product_Type_Entity = new tb_product_type_Entity();
                tb_Product_Type_Entity.Id = Convert.ToInt32(Id);
                tb_Product_Type_Entity.typeName = typeName;
                tb_Product_Type_Entity.description = description;
                tb_Product_Type_Entity.status = 1;
                //tb_Product_Type_Entity.createUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                //tb_Product_Type_Entity.createTime = DateTime.Now;
                tb_Product_Type_Entity.updateUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                tb_Product_Type_Entity.updateTime = DateTime.Now;

                PriceManageBLL priceManageBLL = new PriceManageBLL();

                int updateCount = priceManageBLL.UpdateProductTypeData(tb_Product_Type_Entity);

                TableData tableData = new TableData();

                if (updateCount > 0)
                {
                    tableData.Status = "成功";
                    tableData.ReturnMsg = updateCount.ToString();
                }
                else
                {
                    tableData.Status = "失败";
                    tableData.ReturnMsg = updateCount.ToString();
                }

                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("修改产品类型数据 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 新建产品价格数据
        /// </summary>
        /// <param name="dataJson"></param>
        /// <returns></returns>
        [HttpPost]
        public string AddProductPriceData(string dataJson) {
            string retStr = "";
            try
            {
                JObject data = JObject.Parse(dataJson);

                string productTypeId = data["productTypeId"]?.ToString();
                string salePrice = data["salePrice"]?.ToString();
                string offerPrice = data["offerPrice"]?.ToString();

                List<tb_product_price_Entity> tb_product_price_Entitys = new List<tb_product_price_Entity>();

                tb_product_price_Entity tb_product_price_Entity = new tb_product_price_Entity();
                tb_product_price_Entity.productTypeId = int.Parse(productTypeId);
                tb_product_price_Entity.salePrice = decimal.Parse( salePrice);
                tb_product_price_Entity.offerPrice = decimal.Parse(offerPrice);
                tb_product_price_Entity.status = 1;
                tb_product_price_Entity.createUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                tb_product_price_Entity.createTime = DateTime.Now;
                tb_product_price_Entity.updateUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                tb_product_price_Entity.updateTime = DateTime.Now;

                tb_product_price_Entitys.Add(tb_product_price_Entity);

                PriceManageBLL priceManageBLL = new PriceManageBLL();

                TableData tableData = new TableData();

                tableData = priceManageBLL.AddProductPriceData(tb_product_price_Entitys);

                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("新建产品价格数据 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 修改产品价格数据
        /// </summary>
        /// <param name="dataJson"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateProductPriceData(string dataJson)
        {
            string retStr = "";
            try
            {
                JObject data = JObject.Parse(dataJson);

                string Id = data["Id"]?.ToString();
                string productTypeId = data["productTypeId"]?.ToString();
                string Old_salePrice = data["Old_salePrice"]?.ToString();
                string Old_offerPrice = data["Old_offerPrice"]?.ToString();
                string updateTime = data["updateTime"]?.ToString();
                string salePrice = data["salePrice"]?.ToString();
                string offerPrice = data["offerPrice"]?.ToString();

                tb_product_price_Entity tb_product_price_Entity = new tb_product_price_Entity();
                tb_product_price_Entity.Id = int.Parse(Id);
                tb_product_price_Entity.productTypeId = int.Parse(productTypeId);
                tb_product_price_Entity.salePrice = decimal.Parse(salePrice);
                tb_product_price_Entity.offerPrice = decimal.Parse(offerPrice);
                tb_product_price_Entity.status = 1;
                //tb_product_price_Entity.createUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                //tb_product_price_Entity.createTime = DateTime.Now;
                tb_product_price_Entity.updateUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                tb_product_price_Entity.updateTime = DateTime.Now;

                PriceManageBLL priceManageBLL = new PriceManageBLL();

                //根据修改前的2个价格和修改后的2个价格，判断是否需要修改历史价格表
                if (Old_salePrice != salePrice || Old_offerPrice != offerPrice)
                {
                    DateTime startDate = DateTime.Parse(updateTime);
                   

                    TableData tableData2 = new TableData();

                    //根据产品类型ID查询价格历史表
                    tableData2 = priceManageBLL.QueryProductPriceHistoryTableData(productTypeId);
                    JArray data2 = JArray.Parse(tableData2.Rows.ToString());
                    //如果data2[0]["endDate"]不为空或者空字符串,则为startDate赋值
                    if (data2[0]["endDate"] != null && data2[0]["endDate"].ToString() != "")
                    {
                        startDate = DateTime.Parse(data2[0]["endDate"].ToString());
                    }

                    //修改历史价格表
                    tb_product_price_history_Entity tb_product_price_history_Entity = new tb_product_price_history_Entity();
                    tb_product_price_history_Entity.productTypeId = int.Parse(productTypeId);
                    tb_product_price_history_Entity.salePrice = decimal.Parse(Old_salePrice);
                    tb_product_price_history_Entity.offerPrice = decimal.Parse(Old_offerPrice);
                    tb_product_price_history_Entity.startDate = startDate;
                    tb_product_price_history_Entity.endDate = DateTime.Now;
                    tb_product_price_history_Entity.status = 1;
                    tb_product_price_history_Entity.createUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                    tb_product_price_history_Entity.createTime = DateTime.Now;

                    List<tb_product_price_history_Entity> tb_product_price_history_Entitys = new List<tb_product_price_history_Entity>();
                    tb_product_price_history_Entitys.Add(tb_product_price_history_Entity);

                    priceManageBLL.AddProductPriceHistoryData(tb_product_price_history_Entitys);
                }


                int updateCount = priceManageBLL.UpdateProductPriceData(tb_product_price_Entity);

                TableData tableData = new TableData();

                if (updateCount > 0)
                {
                    tableData.Status = "成功";
                    tableData.ReturnMsg = updateCount.ToString();
                }
                else
                {
                    tableData.Status = "失败";
                    tableData.ReturnMsg = updateCount.ToString();
                }

                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("修改产品价格数据 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 新建产品价格历史数据
        /// </summary>
        /// <param name="dataJson"></param>
        /// <returns></returns>
        [HttpPost]
        public string AddProductPriceHistoryData(string dataJson)
        {
            string retStr = "";
            try
            {
                JObject data = JObject.Parse(dataJson);

                string productTypeId = data["productTypeId"]?.ToString();
                string salePrice = data["salePrice"]?.ToString();
                string offerPrice = data["offerPrice"]?.ToString();
                string startDate = data["startDate"]?.ToString();
                string endDate = data["endDate"]?.ToString();

                List<tb_product_price_history_Entity> tb_product_price_history_Entitys = new List<tb_product_price_history_Entity>();

                tb_product_price_history_Entity tb_product_price_history_Entity = new tb_product_price_history_Entity();
                tb_product_price_history_Entity.productTypeId = int.Parse(productTypeId);
                tb_product_price_history_Entity.salePrice = decimal.Parse(salePrice);
                tb_product_price_history_Entity.offerPrice = decimal.Parse(offerPrice);
                tb_product_price_history_Entity.startDate = DateTime.Parse(startDate);
                tb_product_price_history_Entity.endDate = DateTime.Parse(endDate);
                tb_product_price_history_Entity.status = 1;
                tb_product_price_history_Entity.createUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                tb_product_price_history_Entity.createTime = DateTime.Now;

                tb_product_price_history_Entitys.Add(tb_product_price_history_Entity);

                PriceManageBLL priceManageBLL = new PriceManageBLL();

                TableData tableData = new TableData();

                tableData = priceManageBLL.AddProductPriceHistoryData(tb_product_price_history_Entitys);

                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("新建产品价格历史数据 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 修改产品价格历史数据
        /// </summary>
        /// <param name="dataJson"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateProductPriceHistoryData(string dataJson)
        {
            string retStr = "";
            try
            {
                JObject data = JObject.Parse(dataJson);

                string Id = data["Id"]?.ToString();
                string productTypeId = data["productTypeId"]?.ToString();
                string salePrice = data["salePrice"]?.ToString();
                string offerPrice = data["offerPrice"]?.ToString();
                string startDate = data["startDate"]?.ToString();
                string endDate = data["endDate"]?.ToString();

                tb_product_price_history_Entity tb_product_price_history_Entity = new tb_product_price_history_Entity();
                tb_product_price_history_Entity.Id = int.Parse(Id);
                tb_product_price_history_Entity.productTypeId = int.Parse(productTypeId);
                tb_product_price_history_Entity.salePrice = decimal.Parse(salePrice);
                tb_product_price_history_Entity.offerPrice = decimal.Parse(offerPrice);
                tb_product_price_history_Entity.startDate = DateTime.Parse(startDate);
                tb_product_price_history_Entity.endDate = DateTime.Parse(endDate);
                tb_product_price_history_Entity.status = 1;
                //tb_product_price_history_Entity.createUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
                //tb_product_price_history_Entity.createTime = DateTime.Now;

                PriceManageBLL priceManageBLL = new PriceManageBLL();

                int updateCount = priceManageBLL.UpdateProductPriceHistoryData(tb_product_price_history_Entity);

                TableData tableData = new TableData();

                if (updateCount > 0)
                {
                    tableData.Status = "成功";
                    tableData.ReturnMsg = updateCount.ToString();
                }
                else
                {
                    tableData.Status = "失败";
                    tableData.ReturnMsg = updateCount.ToString();
                }

                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("修改产品价格历史数据 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 删除产品价格数据
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteProductPriceData(string productTypeId)
        {
            string retStr = "";
            try
            {
                PriceManageBLL priceManageBLL = new PriceManageBLL();
                TableData tableData = new TableData();
                tableData = priceManageBLL.DeleteProductPriceData(productTypeId);

                

                if (tableData.Total>0)
                {
                    tableData.Status = "成功";
                }
                else
                {
                    tableData.Status = "失败";
                }

                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("删除产品价格数据 error: " + ex.Message);
            }
            return retStr;
        }

    }
}