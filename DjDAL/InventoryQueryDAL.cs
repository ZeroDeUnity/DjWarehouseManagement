using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using DjDAL.DBHelper;
using DjModels.Entity;
using log4net;
using DjDAL.Utility;
using static DjDAL.Utility.TableData_Utility;

namespace DjDAL
{
    public class InventoryQueryDAL
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(InventoryQueryDAL));

        //查询库存数据,返回json格式数据
        public string QueryInventoryData(string selectInput, string selectType, string selectColor)
        {
            string SqlStr = "";
            string SqlWhere = "";

            //处理selectInput的空格隔开数据
            if (!string.IsNullOrEmpty(selectInput))
            {
                string[] selectInputArr = selectInput.Split(' ');
                foreach (string item in selectInputArr)
                {
                    SqlWhere += " AND (t2.color like '%" + item + "%' OR t2.colorCode like '%" + item + "%' OR t2.process like '%" + item + "%' OR t2.specification like '%" + item + "%' OR t4.actualSize like '%" + item + "%' OR t1.`explain` like '%" + item + "%' OR t1.storageStatus like '%" + item + "%') ";
                }
            }


            if (!string.IsNullOrEmpty(selectType))
            {
                SqlWhere += " AND t2.process = '" + selectType + "' ";
            }

            if (!string.IsNullOrEmpty(selectColor))
            {
                SqlWhere += " AND (t2.color = '" + selectColor + "' OR t2.colorCode = '" + selectColor + "') ";
            }

            SqlStr = " SELECT t2.Id,t2.color,t2.colorCode,t2.process,t2.printCode,t2.specification,t2.size,t1.shelfId,t1.nearbyShelf,t1.remainingQuantity,t1.storageStatus,t1.`explain`,t4.actualSize ";
            SqlStr += " FROM tb_shelf_product_mapping AS t1 ";
            SqlStr += " LEFT JOIN tb_product AS t2 ON t1.productId = t2.Id ";
            SqlStr += " LEFT JOIN tb_stock_entry AS t4 ON t1.Id=t4.storageLocId ";
            SqlStr += " WHERE 1=1 AND t1.`status`='1' AND t2.`status`='1' AND t4.`status`='1' AND t1.remainingQuantity > 0 ";
            SqlStr += SqlWhere;
            SqlStr += " GROUP BY t1.`explain`,t1.productId,t4.actualSize ";
            SqlStr += " ORDER BY t2.process ";
            
            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //
        /// <summary>
        /// 查询库存数据,返回数据
        /// </summary>
        /// <param name="selectInput"></param>
        /// <param name="selectType"></param>
        /// <param name="selectColor"></param>
        /// <returns></returns>
        public TableData QueryInventoryTableData(string selectInput, string selectType, string selectColor)
        {
            string SqlStr = "";
            string SqlWhere = "";

            TableData tableData = new TableData();

            int total = 0;

            try
            {
                //处理selectInput的空格隔开数据
                if (!string.IsNullOrEmpty(selectInput))
                {
                    string[] selectInputArr = selectInput.Split(' ');
                    foreach (string item in selectInputArr)
                    {
                        SqlWhere += " AND (t2.color like '%" + item + "%' OR t2.colorCode like '%" + item + "%' OR t2.process like '%" + item + "%' OR t2.specification like '%" + item + "%' OR t4.actualSize like '%" + item + "%' OR t1.`explain` like '%" + item + "%' OR t1.storageStatus like '%" + item + "%') ";
                    }
                }


                if (!string.IsNullOrEmpty(selectType))
                {
                    SqlWhere += " AND t2.process = '" + selectType + "' ";
                }

                if (!string.IsNullOrEmpty(selectColor))
                {
                    SqlWhere += " AND (t2.color = '" + selectColor + "' OR t2.colorCode = '" + selectColor + "') ";
                }

                SqlStr = " SELECT t2.Id,t2.color,t2.colorCode,t2.process,t2.printCode,t2.specification,t2.size,t1.shelfId,t1.nearbyShelf,t1.remainingQuantity,t1.storageStatus,t1.`explain`,t4.actualSize ";
                SqlStr += " FROM tb_shelf_product_mapping AS t1 ";
                SqlStr += " LEFT JOIN tb_product AS t2 ON t1.productId = t2.Id ";
                SqlStr += " LEFT JOIN tb_stock_entry AS t4 ON t1.Id=t4.storageLocId ";
                SqlStr += " WHERE 1=1 AND t1.`status`='1' AND t2.`status`='1' AND t4.`status`='1' AND t1.remainingQuantity > 0 ";
                SqlStr += SqlWhere;
                SqlStr += " GROUP BY t1.`explain`,t1.productId,t4.actualSize ";
                SqlStr += " ORDER BY t2.process ";

                var db = MysqlDBHelper.GetNewDb();

                var dt2 = db.Ado.GetDataTable(SqlStr);

                tableData.Total= dt2.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt2);
            }
            catch (Exception ex)
            {
                log.Error("查询库存数据", ex);
                return tableData;
            }
            
            return tableData;
        }

        public string QueryProductData(string selectInput)
        {
            string SqlStr = "";
            string SqlWhere = "";
            //处理selectInput的空格隔开数据
            if (!string.IsNullOrEmpty(selectInput))
            {
                string[] selectInputArr = selectInput.Split(' ');
                foreach (string item in selectInputArr)
                {
                    SqlWhere += " AND (a.color like '%" + item + "%' OR a.colorCode like '%" + item + "%' OR a.process like '%" + item + "%' OR a.specification like '%" + item + "%' OR a.printCode like '%" + item + "%' OR a.size like '%" + item + "%'  ) ";
                }
            }

            SqlStr = " SELECT a.* FROM tb_product AS a WHERE a.`status`='1' ";
            SqlStr += SqlWhere;
            SqlStr += " ORDER BY a.createTime DESC ";
            //SqlStr += " GROUP BY t1.`explain`,t1.productId,t4.actualSize ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        /// <summary>
        /// 查询产品数据
        /// </summary>
        /// <param name="selectInput"></param>
        /// <returns></returns>
        public TableData QueryProductTableData(string selectInput)
        {
            TableData tableData = new TableData();

            try
            {
                string SqlStr = "";
                string SqlWhere = "";
                //处理selectInput的空格隔开数据
                if (!string.IsNullOrEmpty(selectInput))
                {
                    string[] selectInputArr = selectInput.Split(' ');
                    foreach (string item in selectInputArr)
                    {
                        SqlWhere += " AND (a.color like '%" + item + "%' OR a.colorCode like '%" + item + "%' OR a.process like '%" + item + "%' OR a.specification like '%" + item + "%' OR a.printCode like '%" + item + "%' OR a.size like '%" + item + "%'  ) ";
                    }
                }

                SqlStr += " SELECT a.Id,a.productType,a.color,a.colorCode,a.process,a.specification,a.size,a.productTypeId,b.typeName FROM tb_product AS a  ";
                SqlStr += " LEFT JOIN tb_product_type AS b ON a.productTypeId = b.Id AND b.`status`='1'  ";
                SqlStr += " WHERE a.`status`='1' ";
                SqlStr += SqlWhere;
                SqlStr += " ORDER BY a.createTime DESC ";
                //SqlStr += " GROUP BY t1.`explain`,t1.productId,t4.actualSize ";

                var db = MysqlDBHelper.GetNewDb();

                var dt2 = db.Ado.GetDataTable(SqlStr);

                //返回json
                tableData.Total = dt2.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt2);
                
            }
            catch (Exception ex)
            {
                log.Error("查询产品数据", ex);
                return tableData;
            }

            //返回json
            return tableData;
        }

        /// <summary>
        /// 新建产品数据
        /// </summary>
        /// <param name="product_entry_List"></param>
        /// <returns></returns>
        public int CreateProductData(List<tb_product_Entity> product_entry_List)
        {
            int insertCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                insertCount = db.Insertable<tb_product_Entity>(product_entry_List).IgnoreColumns(it => new { it.productTypeId }).ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("新建产品数据", ex);
                return insertCount;
            }

            //返回插入数量
            return insertCount;
        }

        /// <summary>
        /// 关联产品类型数据,根据输入的产品类型ID,结合产品ID,把产品类型ID更新到产品表中
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public int UpdateProductTypeData(string productTypeId, string productId)
        {
            int updateCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                updateCount = db.Updateable<tb_product_Entity>().SetColumns(it => new tb_product_Entity() { productTypeId = productTypeId }).Where(it => it.Id == Convert.ToInt32(productId)).ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("关联产品类型数据", ex);
                return updateCount;
            }

            //返回插入数量
            return updateCount;
        }



    }
}
