using DjDAL;
using DjDAL.Utility;
using DjModels.Entity;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DjDAL.Utility.TableData_Utility;

namespace DjBLL
{
    public class PriceManageBLL
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PriceManageBLL));

        /// <summary>
        /// 查询产品价格数据
        /// </summary>
        /// <param name="productTypeName"></param>
        /// <returns></returns>
        public TableData QueryProductPriceTableData(string productTypeName)
        {
            TableData tableData = new TableData();
            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                tableData = priceManageDAL.QueryProductPriceTableData(productTypeName);
            }
            catch (Exception ex)
            {
                log.Error("查询产品价格数据 error: " + ex.Message);
                return tableData;
            }
            return tableData;
        }

        /// <summary>
        /// 查询产品类型数据
        /// </summary>
        /// <returns></returns>
        public TableData QueryProductTypeData()
        {
            TableData tableData = new TableData();
            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                tableData = priceManageDAL.QueryProductTypeData();
            }
            catch (Exception ex)
            {
                log.Error("查询产品类型数据 error: " + ex.Message);
                return tableData;
            }
            return tableData;
        }

        /// <summary>
        /// 查询所有产品类型数据
        /// </summary>
        /// <returns></returns>
        public TableData QueryAllProductTypeData()
        {
            TableData tableData = new TableData();
            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                tableData = priceManageDAL.QueryAllProductTypeData();
            }
            catch (Exception ex)
            {
                log.Error("查询所有产品类型数据 error: " + ex.Message);
                return tableData;
            }
            return tableData;
        }

        /// <summary>
        /// 查询历史价格列表
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public TableData QueryProductPriceHistoryTableData(string productTypeId)
        {
            TableData tableData = new TableData();
            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                tableData = priceManageDAL.QueryProductPriceHistoryTableData(productTypeId);
            }
            catch (Exception ex)
            {
                log.Error("查询历史价格列表 error: " + ex.Message);
                return tableData;
            }
            return tableData;
        }

        /// <summary>
        /// 新建产品类型数据
        /// </summary>
        /// <param name="tb_Product_Type_Entitys"></param>
        /// <returns></returns>
        public TableData AddProductTypeData(List<tb_product_type_Entity> tb_Product_Type_Entitys)
        {
            TableData tableData = new TableData();

            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                int insertCount = priceManageDAL.AddProductTypeData(tb_Product_Type_Entitys);

                tableData.Status = "成功";
                tableData.ReturnMsg = insertCount.ToString();
            }
            catch (Exception ex)
            {
                log.Error("新建产品类型数据 error: " + ex.Message);
                return tableData;
            }

            //返回插入数量
            return tableData;
        }

        /// <summary>
        /// 修改产品类型数据
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        public int UpdateProductTypeData(tb_product_type_Entity productType)
        {
            int updateCount = 0;

            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                updateCount = priceManageDAL.UpdateProductTypeData(productType);
            }
            catch (Exception ex)
            {
                log.Error("修改产品类型数据 error: " + ex.Message);
                return updateCount;
            }

            //返回插入数量
            return updateCount;
        }

        /// <summary>
        /// 新建产品价格数据
        /// </summary>
        /// <param name="tb_product_price_Entitys"></param>
        /// <returns></returns>
        public TableData AddProductPriceData(List<tb_product_price_Entity> tb_product_price_Entitys)
        {
            TableData tableData = new TableData();

            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                int insertCount = priceManageDAL.AddProductPriceData(tb_product_price_Entitys);

                tableData.Status = "成功";
                tableData.ReturnMsg = insertCount.ToString();
            }
            catch (Exception ex)
            {
                log.Error("新建产品价格数据 error: " + ex.Message);
                return tableData;
            }

            //返回插入数量
            return tableData;
        }

        /// <summary>
        /// 修改产品价格数据
        /// </summary>
        /// <param name="productPrice"></param>
        /// <returns></returns>
        public int UpdateProductPriceData(tb_product_price_Entity productPrice)
        {
            int updateCount = 0;

            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                updateCount = priceManageDAL.UpdateProductPriceData(productPrice);
            }
            catch (Exception ex)
            {
                log.Error("修改产品价格数据 error: " + ex.Message);
                return updateCount;
            }

            //返回插入数量
            return updateCount;
        }

        /// <summary>
        /// 新建产品价格历史数据
        /// </summary>
        /// <param name="tb_product_price_history_Entitys"></param>
        /// <returns></returns>
        public TableData AddProductPriceHistoryData(List<tb_product_price_history_Entity> tb_product_price_history_Entitys)
        {
            TableData tableData = new TableData();

            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                int insertCount = priceManageDAL.AddProductPriceHistoryData(tb_product_price_history_Entitys);

                tableData.Status = "成功";
                tableData.ReturnMsg = insertCount.ToString();
            }
            catch (Exception ex)
            {
                log.Error("新建产品价格历史数据 error: " + ex.Message);
                return tableData;
            }

            return tableData;
        }

        /// <summary>
        /// 修改产品价格历史数据
        /// </summary>
        /// <param name="productPriceHistory"></param>
        /// <returns></returns>
        public int UpdateProductPriceHistoryData(tb_product_price_history_Entity productPriceHistory)
        {
            int updateCount = 0;

            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                updateCount = priceManageDAL.UpdateProductPriceHistoryData(productPriceHistory);
            }
            catch (Exception ex)
            {
                log.Error("修改产品价格历史数据 error: " + ex.Message);
                return updateCount;
            }

            //返回插入数量
            return updateCount;
        }

        /// <summary>
        /// 删除产品价格数据
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public TableData DeleteProductPriceData(string productTypeId)
        {
            TableData tableData = new TableData();

            try
            {
                PriceManageDAL priceManageDAL = new PriceManageDAL();

                tableData = priceManageDAL.DeleteProductPriceData(productTypeId);

            }
            catch (Exception ex)
            {
                log.Error("删除产品价格数据 error: " + ex.Message);
                return tableData;
            }

            return tableData;
        }


    }
}
