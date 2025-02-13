using DjDAL.DBHelper;
using DjDAL.Utility;
using DjModels.Entity;
using log4net;
using Mysqlx.Crud;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DjDAL.Utility.TableData_Utility;

namespace DjDAL
{
    public class StockEntryDAL
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(StockEntryDAL));

        //查询入库批次数据,根据入库时间查询
        public string QueryStockEntryBatchData(string startTime, string endTime)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT a.Id,a.batchCode,a.source,a.orderId,SUM(b.quantityIn) as totalQuantity   ";
            SqlStr += " from tb_stock_entry_batch AS a ";
            SqlStr += " LEFT JOIN tb_stock_entry AS b ON a.Id=b.batchId ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += " AND a.entryTime >= '"+ startTime + "' AND a.entryTime <= '"+ endTime + "' ";
            SqlStr += " GROUP BY a.Id ";
            SqlStr += " ORDER BY a.orderId DESC, " +
                      " CASE WHEN a.orderId IS NULL THEN a.createTime END DESC ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        /// <summary>
        /// 查询入库批次数据,根据入库时间查询
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public TableData QueryStockEntryBatchTableData(string startTime, string endTime)
        {
            string SqlStr = "";
            string SqlWhere = "";

            TableData tableData = new TableData();

            try
            {

                SqlStr = " SELECT a.Id,a.batchCode,a.source,a.orderId,SUM(b.quantityIn) as totalQuantity   ";
                SqlStr += " from tb_stock_entry_batch AS a ";
                SqlStr += " LEFT JOIN tb_stock_entry AS b ON a.Id=b.batchId ";
                SqlStr += " WHERE 1=1 ";
                SqlStr += SqlWhere;
                SqlStr += " AND a.entryTime >= '" + startTime + "' AND a.entryTime <= '" + endTime + "' ";
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
                log.Error("查询入库批次数据,根据入库时间查询", ex);
                return tableData;
            }

            return tableData;

        }

        //新建入库批次数据
        public int AddStockEntryBatchData(List<tb_stock_entry_batch_Entity> stock_entry_batch_List)
        {
            int insertCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                insertCount = db.Insertable<tb_stock_entry_batch_Entity>(stock_entry_batch_List).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回插入数量
            return insertCount;
        }

        //修改入库批次数据
        public int UpdateStockEntryBatchData(tb_stock_entry_batch_Entity stock_entry_batchModel)
        {
            int updateCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                updateCount = db.Updateable<tb_stock_entry_batch_Entity>(stock_entry_batchModel).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回插入数量
            return updateCount;
        }

        //新建入库数据
        public int AddStockEntryData(List<tb_stock_entry_Entity> stock_entry_List)
        {
            int insertCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                insertCount = db.Insertable<tb_stock_entry_Entity>(stock_entry_List).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回插入数量
            return insertCount;
        }

        //更新入库数据
        public int UpdateStockEntryData(tb_stock_entry_Entity stock_entryModel)
        {
            int updateCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                updateCount = db.Updateable<tb_stock_entry_Entity>(stock_entryModel).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回插入数量
            return updateCount;
        }

        //查询入库数据,根据入库批次Id查询
        public string QueryStockEntryDataByBatchId(string batchId)
        {
            string SqlStr = "";
            string SqlWhere = "";
            
            SqlStr = @"SELECT
                            a.Id,
                            a.productId,
                            a.productName,
                            a.quantityIn,
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
                            FROM tb_stock_entry AS a
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
        /// 查询入库数据,根据入库批次Id查询
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public TableData QueryStockEntryTableDataByBatchId(string batchId)
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
                            a.quantityIn,
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
                            FROM tb_stock_entry AS a
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
                log.Error("查询入库数据,根据入库批次Id查询", ex);
                return tableData;
            }

            return tableData;

        }


        //根据输入信息查询产品信息,输入信息可能是颜色或者颜色代码,模糊查询
        public string QueryProductInfoByInput(string SqlWhere)
        {
            string SqlStr = "";

            SqlStr = " SELECT * from tb_product AS a ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            //SqlStr += " AND (a.color like '%" + input + "%' OR a.colorCode like '%" + input + "%') ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据输入信息查询货架信息,输入信息是货架名称
        public string QueryShelfInfoByInput(string input)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT * from tb_shelf AS a ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += "  AND a.shelfLevel='1' AND a.shelfName like '%" + input + "%' AND a.shelfName REGEXP '^[A-N]区$' ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据输入信息查询货架信息,输入信息是货架名称和层级
        public string QueryShelfInfoByInput(string shelfName,string shelfLevel)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT * from tb_shelf AS a ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += "  AND a.shelfLevel='"+ shelfLevel + "' AND a.shelfName like '" + shelfName + "' ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据入库批次ID,查询入库批次信息
        public string QueryStockEntryBatchDataById(string batchId)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT * from tb_stock_entry_batch AS a ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += " AND a.Id = '" + batchId + "' ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据产品ID,查询产品信息
        public string QueryProductInfoById(string productId)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT * from tb_product AS a ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += " AND a.Id = '" + productId + "' ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据货架ID,查询货架信息
        public string QueryShelfInfoById(string shelfId)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT * from tb_shelf AS a ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += " AND a.Id = '" + shelfId + "' ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据货架产品关联表ID,查询货架产品关联表信息
        public string QueryShelfProductMappingInfoById(string Id)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT * from tb_shelf_product_mapping AS a ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += " AND a.Id = '" + Id + "' ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据入库数据ID,查询入库数据信息
        public string QueryStockEntryDataById(string Id)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT * from tb_stock_entry AS a ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += " AND a.Id = '" + Id + "' ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //根据货架关联表ID,查询入库数据信息
        public string QueryStockEntryDataByMappingId(string MappingId)
        {
            string SqlStr = "";
            string SqlWhere = "";

            SqlStr = " SELECT * from tb_stock_entry AS a ";
            SqlStr += " WHERE 1=1 ";
            SqlStr += SqlWhere;
            SqlStr += " AND a.storageLocId = '" + MappingId + "' ";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //查询货架产品关联表数据,根据storageStatus,shelfId,productId查询
        public string QueryShelfProductMappingData(string SqlStr)
        {
            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //QueryStockEntryDataByStockId
        public string QueryStockEntryDataByStockId(string sqlStr)
        {
            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(sqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

        //新建货架产品关联表数据
        public int AddShelfProductMappingData(tb_shelf_product_mapping_Entity shelf_product_mappingModel)
        {
            int insertId = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                insertId = db.Insertable<tb_shelf_product_mapping_Entity>(shelf_product_mappingModel).ExecuteReturnIdentity();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回插入数量
            return insertId;
        }

        //更新货架产品关联表数据
        public int UpdateShelfProductMappingData(tb_shelf_product_mapping_Entity shelf_product_mappingModel)
        {
            int updateCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                updateCount = db.Updateable<tb_shelf_product_mapping_Entity>(shelf_product_mappingModel).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回插入数量
            return updateCount;
        }

        //删除入库数据,根据ID
        public int DeleteStockEntryDataById(int Id)
        {
            int deleteCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                deleteCount = db.Deleteable<tb_stock_entry_Entity>().Where(new tb_stock_entry_Entity() { Id = Id }).ExecuteCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //返回删除数量
            return deleteCount;
        }

        //删除货架产品关联表数据,根据ID
        public int DeleteShelfProductMappingDataById(int Id)
        {
            int deleteCount = 0;

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                deleteCount = db.Deleteable<tb_shelf_product_mapping_Entity>().Where(new tb_shelf_product_mapping_Entity() { Id = Id }).ExecuteCommand();
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
