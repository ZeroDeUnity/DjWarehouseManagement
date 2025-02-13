using DjDAL.DBHelper;
using DjDAL.Utility;
using DjModels.Entity;
using log4net;
using Newtonsoft.Json;
using NHibernate.Engine;
using NHibernate.Hql.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using static DjDAL.Utility.TableData_Utility;

namespace DjDAL
{
    public class PriceManageDAL
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PriceManageDAL));

        /// <summary>
        /// 查询产品价格数据表
        /// </summary>
        /// <param name="productTypeName"></param>
        /// <returns></returns>
        public TableData QueryProductPriceTableData(string productTypeName) { 
            TableData tableData = new TableData();

            try
            {
                string SqlStr = "";
                string SqlWhere = "";

                SqlStr = " SELECT ";
                SqlStr += " t1.Id AS productTypeId,t1.typeName,t1.description, ";
                SqlStr += " t2.Id,t2.offerPrice,t2.salePrice,t2.createTime ";
                SqlStr += " FROM tb_product_type AS t1 ";
                SqlStr += " LEFT JOIN tb_product_price AS t2 ON t1.Id = t2.productTypeId AND t2.`status`='1' ";
                SqlStr += " WHERE 1=1 ";
                SqlStr += " AND t1.`status`='1' ";
                SqlStr += " AND t1.typeName LIKE '%" + productTypeName + "%' ";
                SqlStr += " ORDER BY t1.typeName DESC, t2.createTime DESC ";

                var db = MysqlDBHelper.GetNewDb();

                var dt2 = db.Ado.GetDataTable(SqlStr);

                //返回json
                tableData.Total = dt2.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt2);
            }
            catch (Exception ex)
            {
                log.Error("查询产品价格数据表 error: ",ex);
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
                string SqlStr = "";
                string SqlWhere = "";

                SqlStr = " SELECT ";
                SqlStr += " t1.Id,t1.typeName,t1.createTime ";
                SqlStr += " FROM tb_product_type AS t1 ";
                SqlStr += " LEFT JOIN tb_product_price t2 ON t2.productTypeId=t1.Id AND t2.`status`='1' ";
                SqlStr += " WHERE 1=1 ";
                SqlStr += " AND t1.`status`='1' AND t2.productTypeId IS NULL ";
                SqlStr += " GROUP BY t1.typeName ";
                SqlStr += " ORDER BY t1.typeName ";
                SqlStr += " DESC ";

                var db = MysqlDBHelper.GetNewDb();

                var dt2 = db.Ado.GetDataTable(SqlStr);

                //返回json
                tableData.Total = dt2.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt2);
            }
            catch (Exception ex)
            {
                log.Error("查询产品类型数据 error: ", ex);
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
                string SqlStr = "";
                string SqlWhere = "";

                SqlStr = " SELECT ";
                SqlStr += " t1.Id,t1.typeName,t1.createTime ";
                SqlStr += " FROM tb_product_type AS t1 ";
                SqlStr += " LEFT JOIN tb_product_price t2 ON t2.productTypeId=t1.Id AND t2.`status`='1' ";
                SqlStr += " WHERE 1=1 ";
                SqlStr += " AND t1.`status`='1' ";
                SqlStr += " GROUP BY t1.typeName ";
                SqlStr += " ORDER BY t1.typeName ";
                SqlStr += " DESC ";

                var db = MysqlDBHelper.GetNewDb();

                var dt2 = db.Ado.GetDataTable(SqlStr);

                //返回json
                tableData.Total = dt2.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt2);
            }
            catch (Exception ex)
            {
                log.Error("查询所有产品类型数据 error: ", ex);
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
                string SqlStr = "";
                string SqlWhere = "";

                SqlStr = " SELECT ";
                SqlStr += " t1.Id AS productTypeId,t1.typeName, ";
                SqlStr += " t2.Id,t2.offerPrice,t2.salePrice, ";
                SqlStr += " t3.offerPrice AS offerPriceHistory,t3.salePrice AS salePriceHistory, ";
                SqlStr += " t3.startDate,t3.endDate ";
                SqlStr += " FROM tb_product_type AS t1 ";
                SqlStr += " LEFT JOIN tb_product_price AS t2 ON t1.Id = t2.productTypeId AND t2.`status`= '1' ";
                SqlStr += " LEFT JOIN tb_product_price_history AS t3 ON t1.Id = t3.productTypeId AND t3.`status`= '1' ";
                SqlStr += " WHERE ";
                SqlStr += " t1.`status`= '1' ";
                SqlStr += " AND t1.Id = '" + productTypeId + "' ";
                SqlStr += " ORDER BY t3.createTime ";
                SqlStr += " DESC ";

                var db = MysqlDBHelper.GetNewDb();

                var dt2 = db.Ado.GetDataTable(SqlStr);

                //返回json
                tableData.Total = dt2.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt2);
            }
            catch (Exception ex)
            {
                log.Error("查询历史价格列表 error: ", ex);
                return tableData;
            }
            return tableData;
        }

        /// <summary>
        /// 新建产品类型数据
        /// </summary>
        /// <param name="tb_Product_Type_Entitys"></param>
        /// <returns></returns>
        public int AddProductTypeData(List<tb_product_type_Entity> tb_Product_Type_Entitys)
        {
            int insertCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                insertCount = db.Insertable<tb_product_type_Entity>(tb_Product_Type_Entitys).ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("新建产品类型数据 error: ", ex);
                return insertCount;
            }

            //返回插入数量
            return insertCount;
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
                var db = MysqlDBHelper.GetNewDb();

                updateCount = db.Updateable<tb_product_type_Entity>(productType)
                    .IgnoreColumns(it => new { it.createTime, it.createUser }).ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("修改产品类型数据 error: ", ex);
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
        public int AddProductPriceData(List<tb_product_price_Entity> tb_product_price_Entitys)
        {
            int insertCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                insertCount = db.Insertable<tb_product_price_Entity>(tb_product_price_Entitys).ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("新建产品价格数据 error: ", ex);
                return insertCount;
            }

            //返回插入数量
            return insertCount;
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
                var db = MysqlDBHelper.GetNewDb();

                updateCount = db.Updateable<tb_product_price_Entity>(productPrice)
                   .IgnoreColumns(it => new { it.createTime,it.createUser})
                    .ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("修改产品价格数据 error: ", ex);
                return updateCount;
            }

            //返回插入数量
            return updateCount;
        }

        /// <summary>
        /// 新建产品价格历史数据
        /// </summary>
        /// <param name="productPriceHistory"></param>
        /// <returns></returns>
        public int AddProductPriceHistoryData(List<tb_product_price_history_Entity> tb_product_price_history_Entitys)
        {
            int insertCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                insertCount = db.Insertable<tb_product_price_history_Entity>(tb_product_price_history_Entitys).ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("新建产品价格历史数据 error: ", ex);
                return insertCount;
            }

            //返回插入数量
            return insertCount;
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
                var db = MysqlDBHelper.GetNewDb();

                updateCount = db.Updateable<tb_product_price_history_Entity>(productPriceHistory).ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("修改产品价格历史数据 error: ", ex);
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
                var db = MysqlDBHelper.GetNewDb();

                //修改产品类型数据状态
                int deleteCount = db.Updateable<tb_product_type_Entity>().SetColumns(it => new tb_product_type_Entity() { status = 0 }).Where(it => it.Id == Convert.ToInt32(productTypeId)).ExecuteCommand();

                //修改产品价格数据状态
                int updateCount = db.Updateable<tb_product_price_Entity>().SetColumns(it => new tb_product_price_Entity() { status = 0 }).Where(it => it.productTypeId == Convert.ToInt32(productTypeId)).ExecuteCommand();

                //修改产品价格历史数据状态
                int updateCount2 = db.Updateable<tb_product_price_history_Entity>().SetColumns(it => new tb_product_price_history_Entity() { status = 0 }).Where(it => it.productTypeId == Convert.ToInt32(productTypeId)).ExecuteCommand();

                //返回json
                tableData.Total = deleteCount;
                tableData.Rows = JsonConvert.SerializeObject(deleteCount);
            }
            catch (Exception ex)
            {
                log.Error("删除产品价格数据 error: ", ex);
                return tableData;
            }
            return tableData;
        }


    }
}
