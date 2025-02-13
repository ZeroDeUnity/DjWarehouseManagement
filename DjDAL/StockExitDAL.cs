using DjDAL.DBHelper;
using DjModels.Entity;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DjDAL.Utility.TableData_Utility;

namespace DjDAL
{
    public class StockExitDAL
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(StockExitDAL));

        //查询出库批次数据,根据入库时间查询
        public string QueryStockExitBatchData(string startTime, string endTime)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT a.Id,a.batchCode,a.destination,a.orderId,SUM(b.quantityOut) as totalQuantity   ";
            SqlStr += " from tb_stock_exit_batch AS a ";
            SqlStr += " LEFT JOIN tb_stock_exit AS b ON a.Id=b.batchId ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += " AND a.exitTime >= '" + startTime + "' AND a.exitTime <= '" + endTime + "' ";
            SqlStr += " GROUP BY a.Id ";
            SqlStr += " ORDER BY a.orderId DESC, " +
                      " CASE WHEN a.orderId IS NULL THEN a.createTime END DESC ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        /// <summary>
        /// 查询出库批次数据,根据入库时间查询
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public TableData QueryStockExitBatchTableData(string startTime, string endTime)
        {
            string SqlStr = "";
            string SqlWhere = "";

            TableData tableData = new TableData();

            try
            {
                SqlStr = " SELECT a.Id,a.batchCode,a.destination,a.orderId,SUM(b.quantityOut) as totalQuantity   ";
                SqlStr += " from tb_stock_exit_batch AS a ";
                SqlStr += " LEFT JOIN tb_stock_exit AS b ON a.Id=b.batchId ";
                SqlStr += " WHERE 1=1 ";
                SqlStr += SqlWhere;
                SqlStr += " AND a.exitTime >= '" + startTime + "' AND a.exitTime <= '" + endTime + "' ";
                SqlStr += " GROUP BY a.Id ";
                SqlStr += " ORDER BY a.orderId DESC, " +
                          " CASE WHEN a.orderId IS NULL THEN a.createTime END DESC ";

                var db = MysqlDBHelper.GetNewDb();

                var dt2 = db.Ado.GetDataTable(SqlStr);

                //返回json
                tableData.Total = dt2.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt2);

            }
            catch (Exception ex)
            {
                log.Error("查询出库批次数据,根据入库时间查询", ex);
                return tableData;
            }

            //返回json
            return tableData;
        }

        //新建出库批次数据
        public int AddStockExitBatchData(List<tb_stock_exit_batch_Entity> stock_exit_batch_List)
        {
            int insertCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                insertCount = db.Insertable<tb_stock_exit_batch_Entity>(stock_exit_batch_List).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回插入数量
            return insertCount;
        }

        //查询出库数据,根据出库批次Id查询
        public string QueryStockExitDataByBatchId(string batchId)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = @"SELECT
                            a.Id,
                            a.productId,
                            a.productName,
                            a.quantityOut,
                            a.storageLoc,
                            a.storageLocId,
                            a.actualSize,
                            b.color,
                            b.colorCode,
                            b.printCode,
                            b.productType,
                            b.process,
                            b.specification,
                            c.shelfId,
                            c.nearbyShelf,
                            c.explain
                            FROM tb_stock_exit AS a
                            LEFT JOIN tb_product AS b ON a.productId = b.Id
                            LEFT JOIN tb_shelf_product_mapping AS c ON a.storageLocId = c.Id
                            WHERE 
                            	1=1 ";
            SqlWhere += " AND a.batchId = '" + batchId + "' ";
            SqlStr += SqlWhere;

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        /// <summary>
        /// 查询出库数据,根据出库批次Id查询
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public TableData QueryStockExitTableDataByBatchId(string batchId)
        {
            string SqlStr = "";
            string SqlWhere = "";

            TableData tableData = new TableData();

            try
            {
                SqlStr = @"SELECT
                            a.Id,
                            a.productId,
                            a.productName,
                            a.quantityOut,
                            a.storageLoc,
                            a.storageLocId,
                            a.actualSize,
                            b.color,
                            b.colorCode,
                            b.printCode,
                            b.productType,
                            b.process,
                            b.specification,
                            c.shelfId,
                            c.nearbyShelf,
                            c.explain
                            FROM tb_stock_exit AS a
                            LEFT JOIN tb_product AS b ON a.productId = b.Id
                            LEFT JOIN tb_shelf_product_mapping AS c ON a.storageLocId = c.Id
                            WHERE 
                            	1=1 ";
                SqlWhere += " AND a.batchId = '" + batchId + "' ";
                SqlStr += SqlWhere;

                var db = MysqlDBHelper.GetNewDb();

                var dt2 = db.Ado.GetDataTable(SqlStr);

                //返回json
                tableData.Total = dt2.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt2);
            }
            catch (Exception ex)
            {
                log.Error("查询出库数据,根据出库批次Id查询", ex);
                return tableData;
            }
            
            //返回json
            return tableData;
        }

        //修改出库批次数据
        public int UpdateStockExitBatchData(tb_stock_exit_batch_Entity stock_exit_batchModel)
        {
            int updateCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                updateCount = db.Updateable<tb_stock_exit_batch_Entity>(stock_exit_batchModel).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回插入数量
            return updateCount;
        }

        //根据出库批次ID,查询出库批次信息
        public string QueryStockExitBatchDataById(string batchId)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT * from tb_stock_exit_batch AS a ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += " AND a.Id = '" + batchId + "' ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据输入信息查询产品信息,输入信息可能是颜色或者颜色代码,模糊查询
        public string QueryProductInfoByInput(string SqlWhere)
        {
            string SqlStr = "";

            SqlStr += @"
                SELECT
	                MAX(a.Id) AS mapping_Id,
	                b.Id AS product_Id,
	                a.`explain`,
	                a.productQuantity,
                    a.remainingQuantity,
	                b.color,
	                b.colorCode,
	                b.printCode,
	                b.specification,
	                b.process,
	                b.size,
	                b.productType,
                    c.actualSize
                FROM
	            tb_shelf_product_mapping AS a
                LEFT JOIN tb_product AS b ON a.productId = b.Id
                LEFT JOIN tb_stock_entry AS c ON a.Id = c.storageLocId
                WHERE
	                a.`status` = '1'
                    AND b.`status` = '1'

            ";
            SqlStr += SqlWhere;
            SqlStr += @"
                    GROUP BY
                    	a.productId,
                    	a.`explain`,
						c.actualSize
            ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //查询产品数据
        public string queryProductStatus(string SqlStr)
        {
            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //新建出库数据
        public int AddStockExitData(List<tb_stock_exit_Entity> stock_exit_List)
        {
            int insertCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                insertCount = db.Insertable<tb_stock_exit_Entity>(stock_exit_List).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回插入数量
            return insertCount;
        }

        //修改出库数据
        public int UpdateStockExitData(tb_stock_exit_Entity stock_exit_Data)
        {
            int updateCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                updateCount = db.Updateable<tb_stock_exit_Entity>(stock_exit_Data).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回更新数量
            return updateCount;
        }

        //根据产品ID和货架关联ID查询产品信息货架产品关联信息
        public string QueryProductInfoByProductIdAndMappingId(string SqlStr)
        {
            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据storageLocIdList,修改tb_shelf_product_mapping表中的productQuantity
        public int UpdateProductQuantityById(string mapping_Id, string outboundQuantity, string remainingQuantity)
        {
            int updateCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                updateCount = db.Updateable<tb_shelf_product_mapping_Entity>()
                    .SetColumns(it => new tb_shelf_product_mapping_Entity() { outboundQuantity = int.Parse(outboundQuantity), remainingQuantity=int.Parse(remainingQuantity) })
                    .Where(it => it.Id == int.Parse(mapping_Id))
                    .ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回插入数量
            return updateCount;
        }

        //QueryStockExitDataByStockId
        public string QueryStockExitDataByStockId(string SqlStr)
        {
            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据出库数据ID,查询出库数据
        public string QueryStockExitDataById(string Id)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT * from tb_stock_exit AS a ";
            SqlStr += " WHERE 1=1 AND a.`status` = '1' ";
            SqlStr += SqlWhere;
            SqlStr += " AND a.Id = '" + Id + "' ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //删除出库数据,根据ID
        public int DeleteStockExitDataById(int Id)
        {
            int deleteCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                deleteCount = db.Deleteable<tb_stock_exit_Entity>().Where(new tb_stock_exit_Entity() { Id = Id }).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回删除数量
            return deleteCount;
        }

    }
}
