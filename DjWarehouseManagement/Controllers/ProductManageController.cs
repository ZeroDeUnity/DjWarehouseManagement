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
    public class ProductManageController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductManageController));
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Index_Add
        public ActionResult Index_Add()
        {
            // 获取URL参数
            string Id = Request.QueryString["Id"];
            string productType = Request.QueryString["productType"];
            string color = Request.QueryString["color"];
            string colorCode = Request.QueryString["colorCode"];
            string process = Request.QueryString["process"];
            string specification = Request.QueryString["specification"];
            string size = Request.QueryString["size"];

            // 将参数传递给视图
            ViewBag.Id = Id;
            ViewBag.productType = productType;
            ViewBag.color = color;
            ViewBag.colorCode = colorCode;
            ViewBag.process = process;
            ViewBag.specification = specification;
            ViewBag.size = size;
            return View();
        }

        public ActionResult Index_SetType()
        {
            // 获取URL参数
            string Id = Request.QueryString["Id"];
            string productTypeId = Request.QueryString["productTypeId"];
            string typeName = Request.QueryString["typeName"];
            string color = Request.QueryString["color"];
            string colorCode = Request.QueryString["colorCode"];
            string process = Request.QueryString["process"];
            string specification = Request.QueryString["specification"];

            // 将参数传递给视图
            ViewBag.Id = Id;
            ViewBag.productTypeId = productTypeId;
            ViewBag.typeName = typeName;
            ViewBag.color = color;
            ViewBag.colorCode = colorCode;
            ViewBag.process = process;
            ViewBag.specification = specification;
            return View();
        }

        /// <summary>
        /// 查询产品数据
        /// </summary>
        /// <param name="selectInput"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryProductData(string selectInput)
        {
            string retStr = "";
            try
            {
                InventoryQueryBLL inventoryQueryBLL = new InventoryQueryBLL();

                TableData tableData = new TableData();

                tableData = inventoryQueryBLL.QueryProductTableData(selectInput);

                // 直接访问静态类的属性来进行序列化
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("查询产品数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 创建产品数据
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="color"></param>
        /// <param name="colorCode"></param>
        /// <param name="process"></param>
        /// <param name="printCode"></param>
        /// <param name="specification"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpPost]
        public string CreateProductData(string productType, string color, string colorCode, string process, string printCode, string specification, string size)
        {

            string retStr = "";

            try
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

                retStr = result.ToString();
            }
            catch (Exception ex)
            {
                log.Error("创建产品数据", ex);
                return retStr;
            }
            
            return retStr;

        }

        //关联产品类型数据,根据输入的产品类型ID,结合产品ID,把产品类型ID更新到产品表中
        [HttpPost]
        public string UpdateProductTypeData(string dataJson)
        {
            string retStr = "";

            try
            {
                //解析dataJson,获取productTypeId和productId
                JObject data = (JObject)JsonConvert.DeserializeObject(dataJson);

                string productTypeId = data["productTypeId"].ToString();
                string productId = data["productId"].ToString();

                InventoryQueryBLL inventoryQueryBLL = new InventoryQueryBLL();

                int updateCount = inventoryQueryBLL.UpdateProductTypeData(productTypeId, productId);

                //创建json对象,返回处理信息
                JObject result = new JObject();

                if (updateCount > 0)
                {
                    result["updateCount"] = updateCount;
                    result["Status"] = "成功";
                }
                else
                {
                    result["updateCount"] = updateCount;
                    result["Status"] = "失败";
                }

                retStr = result.ToString();
            }
            catch (Exception ex)
            {
                log.Error("关联产品类型数据", ex);
                return retStr;
            }

            return retStr;
        }

    }
}