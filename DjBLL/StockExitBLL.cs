using DjDAL;
using DjDAL.Utility;
using DjModels.Entity;
using Mysqlx.Crud;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DjDAL.Utility.TableData_Utility;

namespace DjBLL
{
    public class StockExitBLL
    {
        //查询入库批次数据,根据入库时间查询
        public string QueryStockExitBatchData(string startTime, string endTime)
        {
            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.QueryStockExitBatchData(startTime, endTime);
        }

        /// <summary>
        /// 查询入库批次数据,根据入库时间查询
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public TableData QueryStockExitBatchTableData(string startTime, string endTime)
        {
            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.QueryStockExitBatchTableData(startTime, endTime);
        }

        //新建出库批次数据
        /// <summary>
        /// 新建出库批次数据
        /// </summary>
        /// <param name="destination">出库目标</param>
        /// <returns></returns>
        public int AddStockExitBatchData(string destination,string orderId, string date)
        {
            List<tb_stock_exit_batch_Entity> stock_exit_batch_List = new List<tb_stock_exit_batch_Entity>();

            tb_stock_exit_batch_Entity stock_exit_batch = new tb_stock_exit_batch_Entity();
            stock_exit_batch.batchCode = "CK" + DateTime.Now.ToString("yyyyMMddHHmmss");
            stock_exit_batch.orderId = orderId;
            stock_exit_batch.exitTime = Convert.ToDateTime(date);
            stock_exit_batch.totalQuantity = 0;
            stock_exit_batch.destination = destination;
            stock_exit_batch.status = 1;
            stock_exit_batch.createUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();
            stock_exit_batch.createTime = DateTime.Now;
            stock_exit_batch.updateTime = DateTime.Now;
            stock_exit_batch.updateUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();

            stock_exit_batch_List.Add(stock_exit_batch);

            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.AddStockExitBatchData(stock_exit_batch_List);
        }

        //查询出库数据,根据出库批次Id查询
        public string QueryStockExitDataByBatchId(string batchId)
        {
            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.QueryStockExitDataByBatchId(batchId);
        }

        /// <summary>
        /// 查询出库数据,根据出库批次Id查询
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public TableData QueryStockExitTableDataByBatchId(string batchId)
        {
            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.QueryStockExitTableDataByBatchId(batchId);
        }

        //修改出库批次数据
        /// <summary>
        /// 新建出库批次数据
        /// </summary>
        /// <param name="destination">出库目标</param>
        /// <returns></returns>
        public int UpdateStockExitBatchData(string batchId, string destination,string orderId, string date)
        {
            //查询出库批次数据,根据batchId
            string batchData = QueryStockExitBatchDataById(batchId);
            JArray batchDataJson = JArray.Parse(batchData);
            string batchCode = batchDataJson[0]["batchCode"]?.ToString();
            string exitTime = batchDataJson[0]["exitTime"]?.ToString();
            string totalQuantity = batchDataJson[0]["totalQuantity"]?.ToString();
            string status = batchDataJson[0]["status"]?.ToString();
            string createUser = batchDataJson[0]["createUser"]?.ToString();
            string createTime = batchDataJson[0]["createTime"]?.ToString();
            string updateTime = batchDataJson[0]["updateTime"]?.ToString();
            string updateUser = batchDataJson[0]["updateUser"]?.ToString();

            tb_stock_exit_batch_Entity stock_exit_batch = new tb_stock_exit_batch_Entity();

            stock_exit_batch.Id = int.Parse(batchId);
            stock_exit_batch.batchCode = batchCode;
            stock_exit_batch.orderId = orderId;
            stock_exit_batch.exitTime = Convert.ToDateTime(exitTime);
            stock_exit_batch.totalQuantity = int.Parse(totalQuantity);
            stock_exit_batch.destination = destination;
            stock_exit_batch.status = int.Parse(status);
            stock_exit_batch.createUser = createUser;
            stock_exit_batch.createTime = Convert.ToDateTime(createTime);
            stock_exit_batch.updateTime = DateTime.Now;
            stock_exit_batch.updateUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();

            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.UpdateStockExitBatchData(stock_exit_batch);
        }

        //根据入库批次ID,查询入库批次信息
        public string QueryStockExitBatchDataById(string batchId)
        {
            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.QueryStockExitBatchDataById(batchId);
        }

        //根据输入信息查询产品信息,输入信息可能是颜色或者颜色代码,模糊查询
        public string QueryProductInfoByInput(string selectProductType, string now_stockViewStatus, string now_mappingId)
        {
            StockExitDAL stockExitDAL = new StockExitDAL();

            string SqlWhere = "";

            if (now_stockViewStatus == "新增")
            {
                SqlWhere = " AND a.remainingQuantity > 0 AND b.productType = '" + selectProductType + "' ";
            }
            else
            {
                SqlWhere += " AND (a.remainingQuantity > 0 ";
                string[] now_mappingIds = now_mappingId.Split(',');
                for (int i = 0; i < now_mappingIds.Length; i++)
                {
                    SqlWhere += " OR a.Id = '" + now_mappingIds[i] + "' ";
                }
                SqlWhere += " ) ";
                SqlWhere += " AND b.productType = '" + selectProductType + "' ";
            }

            return stockExitDAL.QueryProductInfoByInput(SqlWhere);
        }

        //查询货架产品关联表数据
        public string queryProductStatus(string dataJson)
        {
            StockExitDAL stockExitDAL = new StockExitDAL();

            JObject data = JObject.Parse(dataJson);

            string batchId = data["batchId"]?.ToString();
            string selectedProduct = data["selectedProduct"]?.ToString();
            string selectProductType = data["selectProductType"]?.ToString();
            string quantityOut = data["quantityOut"]?.ToString();

            string product_Id = selectedProduct.Split('_')[0];
            string mapping_Id = selectedProduct.Split('_')[1];
            string actualSize = selectedProduct.Split('_')[2];

            string SqlStr = "";
            string SqlWhere = "";

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
                        WHERE a.`status`= '1' AND b.`status`= '1' AND c.`status`= '1' ";

            SqlStr += " AND a.`explain` = ( SELECT tb1.`explain` FROM tb_shelf_product_mapping AS tb1 WHERE tb1.Id = '" + mapping_Id + "' ) ";
            SqlStr += " AND a.productId = '" + product_Id + "'";
            SqlStr += " AND c.actualSize = '" + actualSize + "'";
            SqlStr += @"            GROUP BY
                        	a.productId,
                        	a.`explain`,
                            c.actualSize
            ";

            return stockExitDAL.queryProductStatus(SqlStr);

        }

        //新建入库数据
        public int AddStockExitData(string dataJson)
        {
            JObject data = JObject.Parse(dataJson);

            string stockId = data["stockId"]?.ToString();
            string batchId = data["batchId"]?.ToString();
            string selectedProductType = data["selectedProductType"]?.ToString();
            string selectedProduct = data["selectedProduct"]?.ToString();
            string actualSize = data["actualSize"]?.ToString();
            string quantityOut = data["quantityOut"]?.ToString();
            string now_productQuantity = data["now_productQuantity"]?.ToString();

            string productId = selectedProduct.Split('_')[0];
            string mappingId = selectedProduct.Split('_')[1];
            string entryProductSize = selectedProduct.Split('_')[2];

            //根据batchId,查询出库批次数据
            string batchData = QueryStockExitBatchDataById(batchId);
            JArray batchDataJson = JArray.Parse(batchData);
            string destination = batchDataJson[0]["destination"]?.ToString();

            //根据productId,查询产品信息
            StockEntryBLL stockEntry = new StockEntryBLL();
            string productData = stockEntry.QueryProductInfoById(productId);
            JArray productDataJson = JArray.Parse(productData);
            string color = productDataJson[0]["color"]?.ToString();
            string colorCode = productDataJson[0]["colorCode"]?.ToString();
            string process = productDataJson[0]["process"]?.ToString();
            string printCode = productDataJson[0]["printCode"]?.ToString();
            string specification = productDataJson[0]["specification"]?.ToString();
            string size = productDataJson[0]["size"]?.ToString();

            string productName = color + "_" + colorCode + "_" + specification + process;
            //+ "_" + printCode + "_" + specification + "_" + size;
            //判断process中是否含有"花"字,有的话,加上printCode
            if (process.Contains("花"))
            {
                productName = productName + "_" + printCode;
            }
            productName = productName + "_" + size + "码";

            //根据shelf,查询货架信息
            //string shelfData = QueryShelfInfoById(shelfId);
            //JArray shelfDataJson = JArray.Parse(shelfData);
            //string shelfId = shelfDataJson[0]["shelfId"]?.ToString();

            string storageLoc = "";
            string storageLocId = "";

            string ProductInfoData = QueryProductInfoByProductIdAndMappingId(productId, mappingId, entryProductSize);
            JArray ProductInfoDataJson = JArray.Parse(ProductInfoData);
            storageLoc = ProductInfoDataJson[0]["explain"]?.ToString();

            //创建一个对象,可以用来储存数据,数据为mapping_Id和剩余productQuantity的组合
            List<string> updateMappingDataList = new List<string>();

            int quantityOut_Val = int.Parse(quantityOut);
            //遍历ProductInfoDataJson,按顺序取出productQuantity值
            for (int i = 0; i < ProductInfoDataJson.Count; i++)
            {
                int outboundQuantity = int.Parse(ProductInfoDataJson[i]["outboundQuantity"]?.ToString());
                int remainingQuantity = int.Parse(ProductInfoDataJson[i]["remainingQuantity"]?.ToString());

                if (remainingQuantity <= 0)
                {
                    //如果remainingQuantity小于等于0,则跳过当前循环    
                    continue;
                }
                if (remainingQuantity >= quantityOut_Val)
                {
                    //如果remainingQuantity大于等于quantityOut_Val,则取出对应的mapping_Id
                    storageLocId = ProductInfoDataJson[i]["mapping_Id"]?.ToString();

                    int new_outboundQuantity = outboundQuantity + quantityOut_Val;
                    int new_remainingQuantity = remainingQuantity - quantityOut_Val;

                    string updateMappingData = ProductInfoDataJson[i]["mapping_Id"]?.ToString() + "_" + new_outboundQuantity + "_"+ new_remainingQuantity;
                    updateMappingDataList.Add(updateMappingData);
                    //跳出循环
                    break;
                }
            }


            //组装tb_stock_entry数据,把上面的数据都添加到jsonobject中
            JObject stockExitData = new JObject();

            List<tb_stock_exit_Entity> stock_exit_List = new List<tb_stock_exit_Entity>();

            tb_stock_exit_Entity stock_exit = new tb_stock_exit_Entity();
            stock_exit.batchId = int.Parse(batchId);
            stock_exit.productName = productName;
            stock_exit.productId = int.Parse(productId);
            stock_exit.actualSize = int.Parse(actualSize);
            stock_exit.stock = int.Parse(now_productQuantity);
            stock_exit.quantityOut = int.Parse(quantityOut);
            stock_exit.storageLoc = storageLoc;
            stock_exit.storageLocId = int.Parse(storageLocId);
            stock_exit.exitTime = DateTime.Now;
            stock_exit.destination = destination;
            stock_exit.status = 1;
            stock_exit.createUser = "admin";
            stock_exit.createTime = DateTime.Now;
            stock_exit.updateTime = DateTime.Now;
            stock_exit.updateUser = "admin";

            stock_exit_List.Add(stock_exit);

            //updateMappingDataList,修改tb_shelf_product_mapping表中的productQuantity
            UpdateProductQuantityById(updateMappingDataList);

            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.AddStockExitData(stock_exit_List);
        }

        //UpdateStockExitData
        public int UpdateStockExitData(string dataJson)
        {
            JObject data = JObject.Parse(dataJson);

            string stockId = data["stockId"]?.ToString();
            string batchId = data["batchId"]?.ToString();
            string selectedProductType = data["selectedProductType"]?.ToString();
            string selectedProduct = data["selectedProduct"]?.ToString();
            string actualSize = data["actualSize"]?.ToString();
            string quantityOut = data["quantityOut"]?.ToString();
            string now_productQuantity = data["now_productQuantity"]?.ToString();
            
            string productId = selectedProduct.Split('_')[0];
            string mappingId = selectedProduct.Split('_')[1];
            string entryProductSize = selectedProduct.Split('_')[2];

            string old_StockExitData = QueryStockExitDataById(stockId);
            JArray old_StockExitDataJson = JArray.Parse(old_StockExitData);
            string old_storageLoc = old_StockExitDataJson[0]["storageLoc"]?.ToString();
            string old_storageLocId = old_StockExitDataJson[0]["storageLocId"]?.ToString();
            string old_productId = old_StockExitDataJson[0]["productId"]?.ToString();
            string old_quantityOut = old_StockExitDataJson[0]["quantityOut"]?.ToString();
            string old_createTime = old_StockExitDataJson[0]["createTime"]?.ToString();
            string old_createUser = old_StockExitDataJson[0]["createUser"]?.ToString();

            string mapping_Id = old_storageLocId.Split('_')[0];

            //根据货架产品关联表ID,查询货架产品关联表信息
            StockEntryBLL stockEntryBLL = new StockEntryBLL();
            string ShelfProductMappingInfo = stockEntryBLL.QueryShelfProductMappingInfoById(old_storageLocId);
            JArray ShelfProductMappingInfoJson = JArray.Parse(ShelfProductMappingInfo);
            string explain = ShelfProductMappingInfoJson[0]["explain"]?.ToString();
            string outboundQuantity1 = ShelfProductMappingInfoJson[0]["outboundQuantity"]?.ToString();
            string remainingQuantity1 = ShelfProductMappingInfoJson[0]["remainingQuantity"]?.ToString();

            //回退原本出库数据至产品货架关联表
            //创建一个对象,可以用来储存数据
            List<string> updateMappingDataList1 = new List<string>();

            int new_outboundQuantity1 = int.Parse(outboundQuantity1)- int.Parse(old_quantityOut);

            int new_remainingQuantity1 = int.Parse(remainingQuantity1) + int.Parse(old_quantityOut);

            string updateMappingData1 = old_storageLocId + "_" + new_outboundQuantity1 + "_" + new_remainingQuantity1;
            updateMappingDataList1.Add(updateMappingData1);
            //修改tb_shelf_product_mapping表中的productQuantity
            UpdateProductQuantityById(updateMappingDataList1);


            //根据batchId,查询出库批次数据
            string batchData = QueryStockExitBatchDataById(batchId);
            JArray batchDataJson = JArray.Parse(batchData);
            string destination = batchDataJson[0]["destination"]?.ToString();

            //根据productId,查询产品信息
            StockEntryBLL stockEntry = new StockEntryBLL();
            string productData = stockEntry.QueryProductInfoById(productId);
            JArray productDataJson = JArray.Parse(productData);
            string color = productDataJson[0]["color"]?.ToString();
            string colorCode = productDataJson[0]["colorCode"]?.ToString();
            string process = productDataJson[0]["process"]?.ToString();
            string printCode = productDataJson[0]["printCode"]?.ToString();
            string specification = productDataJson[0]["specification"]?.ToString();
            string size = productDataJson[0]["size"]?.ToString();

            string productName = color + "_" + colorCode + "_" + specification + process;
            //+ "_" + printCode + "_" + specification + "_" + size;
            //判断process中是否含有"花"字,有的话,加上printCode
            if (process.Contains("花"))
            {
                productName = productName + "_" + printCode;
            }
            productName = productName + "_" + size + "码";


            string storageLoc = "";
            string storageLocId = "";

            string ProductInfoData = QueryProductInfoByProductIdAndMappingId(productId, mappingId, entryProductSize);
            JArray ProductInfoDataJson = JArray.Parse(ProductInfoData);
            storageLoc = ProductInfoDataJson[0]["explain"]?.ToString();

            //创建一个对象,可以用来储存数据,数据为mapping_Id和剩余productQuantity的组合
            List<string> updateMappingDataList = new List<string>();

            int quantityOut_Val = int.Parse(quantityOut);
            //遍历ProductInfoDataJson,按顺序取出productQuantity值
            for (int i = 0; i < ProductInfoDataJson.Count; i++)
            {
                int outboundQuantity = int.Parse(ProductInfoDataJson[i]["outboundQuantity"]?.ToString());
                int remainingQuantity = int.Parse(ProductInfoDataJson[i]["remainingQuantity"]?.ToString());

                if (remainingQuantity <= 0)
                {
                    //如果remainingQuantity小于等于0,则跳过当前循环    
                    continue;
                }
                if (remainingQuantity >= quantityOut_Val)
                {
                    //如果remainingQuantity大于等于quantityOut_Val,则取出对应的mapping_Id
                    storageLocId = ProductInfoDataJson[i]["mapping_Id"]?.ToString();

                    int new_outboundQuantity = outboundQuantity + quantityOut_Val;
                    int new_remainingQuantity = remainingQuantity - quantityOut_Val;

                    string updateMappingData = ProductInfoDataJson[i]["mapping_Id"]?.ToString() + "_" + new_outboundQuantity + "_" + new_remainingQuantity;
                    updateMappingDataList.Add(updateMappingData);
                    //跳出循环
                    break;
                }
            }


            //组装tb_stock_entry数据,把上面的数据都添加到jsonobject中
            JObject stockExitData = new JObject();

            tb_stock_exit_Entity stock_exit = new tb_stock_exit_Entity();
            stock_exit.Id = int.Parse(stockId);
            stock_exit.batchId = int.Parse(batchId);
            stock_exit.productName = productName;
            stock_exit.productId = int.Parse(productId);
            stock_exit.actualSize = int.Parse(actualSize);
            stock_exit.stock = int.Parse(now_productQuantity);
            stock_exit.quantityOut = int.Parse(quantityOut);
            stock_exit.storageLoc = storageLoc;
            stock_exit.storageLocId = int.Parse(storageLocId);
            stock_exit.exitTime = DateTime.Now;
            stock_exit.destination = destination;
            stock_exit.status = 1;
            stock_exit.createUser = old_createUser;
            stock_exit.createTime = DateTime.Parse(old_createTime);
            stock_exit.updateTime = DateTime.Now;
            stock_exit.updateUser = "admin";

            //updateMappingDataList,修改tb_shelf_product_mapping表中的productQuantity
            UpdateProductQuantityById(updateMappingDataList);

            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.UpdateStockExitData(stock_exit);
        }

        //根据产品ID和货架关联ID查询产品信息货架产品关联信息
        public string QueryProductInfoByProductIdAndMappingId(string productId, string mappingId,string actualSize)
        {
            string SqlStr = "";

            SqlStr += @"
                        SELECT
                        	a.Id AS mapping_Id,
                        	b.Id AS product_Id,
                        	a.`explain`,
                        	a.productQuantity,
                            a.inboundQuantity,
                            a.outboundQuantity,
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
                        WHERE ";
            SqlStr += "   a.`explain` = ( SELECT tb1.`explain` FROM tb_shelf_product_mapping AS tb1 WHERE tb1.Id = '" + mappingId + "' ) ";
            SqlStr += "   AND a.productId = '" + productId + "'";
            SqlStr += "   AND c.actualSize = '" + actualSize + "'";
            
            SqlStr += @"  ORDER BY
                        	a.Id DESC;
            ";

            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.QueryProductInfoByProductIdAndMappingId(SqlStr);
        }

        //根据updateMappingDataList,修改tb_shelf_product_mapping表中的productQuantity
        public int UpdateProductQuantityById(List<string> updateMappingDataList)
        {
            int resultCount = 0;

            StockExitDAL stockExitDAL = new StockExitDAL();

            for (int i = 0; i < updateMappingDataList.Count; i++)
            {
                string mapping_Id = updateMappingDataList[i].Split('_')[0];
                string outboundQuantity = updateMappingDataList[i].Split('_')[1];
                string remainingQuantity = updateMappingDataList[i].Split('_')[2];

                stockExitDAL.UpdateProductQuantityById(mapping_Id, outboundQuantity, remainingQuantity);

                resultCount++;
            }

            return resultCount;
        }

        //QueryStockExitDataByStockId
        public string QueryStockExitDataByStockId(string stockId)
        {
            StockExitDAL stockExitDAL = new StockExitDAL();

            string StockExitData = QueryStockExitDataById(stockId);
            JArray StockExitDataJson = JArray.Parse(StockExitData);
            string storageLoc = StockExitDataJson[0]["storageLoc"]?.ToString();
            string storageLocId = StockExitDataJson[0]["storageLocId"]?.ToString();
            string productId = StockExitDataJson[0]["productId"]?.ToString();
            string quantityOut = StockExitDataJson[0]["quantityOut"]?.ToString();
            string Exit_actualSize = StockExitDataJson[0]["actualSize"]?.ToString();

            string mapping_Id = storageLocId.Split('_')[0];

            string mapping_IdList = storageLocId;

            string SqlStr = "";

            SqlStr += @"
                        SELECT
                        	MAX(a.Id) AS mapping_Id,
                        	b.Id AS productId,
                        	a.`explain`,
                        	a.inboundQuantity,
                        	a.outboundQuantity,
                        	a.remainingQuantity,
                        	b.color,
                        	b.colorCode,
                        	b.printCode,
                        	b.specification,
                        	b.process,
                        	b.size,
                        	b.productType,
                            c.actualSize,
                            '" + Exit_actualSize + @"' AS Exit_actualSize,
                            '" + quantityOut + @"' AS quantityOut,
                            '" + mapping_IdList + @"' AS mapping_IdList
                        FROM
                        	tb_shelf_product_mapping AS a
                        LEFT JOIN tb_product AS b ON a.productId = b.Id
                        LEFT JOIN tb_stock_entry AS c ON a.Id = c.storageLocId
                        WHERE ";

            SqlStr += " a.`explain` = ( SELECT tb1.`explain` FROM tb_shelf_product_mapping AS tb1 WHERE tb1.`status`= '1' AND tb1.Id = '" + mapping_Id + "' ) ";
            SqlStr += " AND a.productId = '" + productId + "' ";
            SqlStr += " AND c.actualSize = (SELECT tb2.actualSize FROM tb_stock_entry AS tb2 WHERE tb2.`status`= '1' AND tb2.storageLocId = '" + mapping_Id + "' LIMIT 1) ";
            
            SqlStr += @"            GROUP BY
                        	a.productId,
                        	a.`explain`,
                            c.actualSize
            ";




            return stockExitDAL.QueryStockExitDataByStockId(SqlStr);
        }

        //根据出库数据ID,查询出库数据
        public string QueryStockExitDataById(string Id)
        {
            StockExitDAL stockExitDAL = new StockExitDAL();

            return stockExitDAL.QueryStockExitDataById(Id);
        }

        //删除入库数据,根据ID
        public string DeleteStockExitDataById(int Id)
        {
            StockExitDAL stockExitDAL = new StockExitDAL();

            //根据货架产品关联表ID,查询货架产品关联表信息
            StockEntryBLL stockEntryBLL = new StockEntryBLL();

            string old_StockExitData = QueryStockExitDataById(Id.ToString());
            JArray old_StockExitDataJson = JArray.Parse(old_StockExitData);
            string old_storageLoc = old_StockExitDataJson[0]["storageLoc"]?.ToString();
            string old_storageLocId = old_StockExitDataJson[0]["storageLocId"]?.ToString();
            string old_productId = old_StockExitDataJson[0]["productId"]?.ToString();
            string old_quantityOut = old_StockExitDataJson[0]["quantityOut"]?.ToString();
            string old_createTime = old_StockExitDataJson[0]["createTime"]?.ToString();
            string old_createUser = old_StockExitDataJson[0]["createUser"]?.ToString();

            string ShelfProductMappingInfo = stockEntryBLL.QueryShelfProductMappingInfoById(old_storageLocId);
            JArray ShelfProductMappingInfoJson = JArray.Parse(ShelfProductMappingInfo);
            string explain = ShelfProductMappingInfoJson[0]["explain"]?.ToString();
            string outboundQuantity1 = ShelfProductMappingInfoJson[0]["outboundQuantity"]?.ToString();
            string remainingQuantity1 = ShelfProductMappingInfoJson[0]["remainingQuantity"]?.ToString();


            //回退原本出库数据至产品货架关联表
            //创建一个对象,可以用来储存数据
            List<string> updateMappingDataList1 = new List<string>();

            int new_outboundQuantity1 = int.Parse(outboundQuantity1) - int.Parse(old_quantityOut);

            int new_remainingQuantity1 = int.Parse(remainingQuantity1) + int.Parse(old_quantityOut);

            string updateMappingData1 = old_storageLocId + "_" + new_outboundQuantity1 + "_" + new_remainingQuantity1;
            updateMappingDataList1.Add(updateMappingData1);
            //修改tb_shelf_product_mapping表中的productQuantity
            UpdateProductQuantityById(updateMappingDataList1);

            int delete_stockExitCount = stockExitDAL.DeleteStockExitDataById(Id);


            //创建json对象,返回处理信息
            JObject result = new JObject();

            if (delete_stockExitCount > 0 )
            {
                result["status"] = "success";
            }
            else
            {
                result["status"] = "fail";
            }
            return result.ToString();
        }

    }
}
