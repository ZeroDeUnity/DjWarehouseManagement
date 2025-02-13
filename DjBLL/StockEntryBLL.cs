using DjDAL;
using DjDAL.DBHelper;
using DjDAL.Utility;
using DjModels.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DjDAL.Utility.TableData_Utility;

namespace DjBLL
{
    public class StockEntryBLL
    {
        //查询入库批次数据,根据入库时间查询
        public string QueryStockEntryBatchData(string startTime, string endTime)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryStockEntryBatchData(startTime, endTime);
        }

        /// <summary>
        /// 查询入库批次数据,根据入库时间查询
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public TableData QueryStockEntryBatchTableData(string startTime, string endTime)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryStockEntryBatchTableData(startTime, endTime);
        }

        //新建入库批次数据
        /// <summary>
        /// 新建入库批次数据
        /// </summary>
        /// <param name="source">产品来源</param>
        /// <returns></returns>
        public int AddStockEntryBatchData(string source, string date , string orderId)
        {
            List<tb_stock_entry_batch_Entity> stock_entry_batch_List = new List<tb_stock_entry_batch_Entity>();

            tb_stock_entry_batch_Entity stock_entry_batch = new tb_stock_entry_batch_Entity();
            stock_entry_batch.batchCode = "RK" + DateTime.Now.ToString("yyyyMMddHHmmss");
            stock_entry_batch.orderId = orderId;
            stock_entry_batch.entryTime = Convert.ToDateTime(date);
            stock_entry_batch.totalQuantity = 0;
            stock_entry_batch.source = source;
            stock_entry_batch.status = 1;
            stock_entry_batch.createUser = Utility.GetUserId().ToString()+"_"+ Utility.GetUserName();
            stock_entry_batch.createTime = DateTime.Now;
            stock_entry_batch.updateTime = DateTime.Now;
            stock_entry_batch.updateUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();

            stock_entry_batch_List.Add(stock_entry_batch);

            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.AddStockEntryBatchData(stock_entry_batch_List);
        }

        //修改入库批次数据
        /// <summary>
        /// 新建入库批次数据
        /// </summary>
        /// <param name="source">产品来源</param>
        /// <returns></returns>
        public int UpdateStockEntryBatchData(string batchId,string source, string orderId, string date)
        {
            //查询入库批次数据,根据batchId
            string batchData = QueryStockEntryBatchDataById(batchId);
            JArray batchDataJson = JArray.Parse(batchData);
            string batchCode = batchDataJson[0]["batchCode"]?.ToString();
            string entryTime = batchDataJson[0]["entryTime"]?.ToString();
            string totalQuantity = batchDataJson[0]["totalQuantity"]?.ToString();
            string status = batchDataJson[0]["status"]?.ToString();
            string createUser = batchDataJson[0]["createUser"]?.ToString();
            string createTime = batchDataJson[0]["createTime"]?.ToString();
            string updateTime = batchDataJson[0]["updateTime"]?.ToString();
            string updateUser = batchDataJson[0]["updateUser"]?.ToString();

            tb_stock_entry_batch_Entity stock_entry_batch = new tb_stock_entry_batch_Entity();

            stock_entry_batch.Id = int.Parse(batchId);
            stock_entry_batch.batchCode = batchCode;
            stock_entry_batch.orderId = orderId;
            stock_entry_batch.entryTime = Convert.ToDateTime(entryTime);
            stock_entry_batch.totalQuantity = int.Parse(totalQuantity);
            stock_entry_batch.source = source;
            stock_entry_batch.status = int.Parse(status);
            stock_entry_batch.createUser = createUser;
            stock_entry_batch.createTime = Convert.ToDateTime(createTime);
            stock_entry_batch.updateTime = DateTime.Now;
            stock_entry_batch.updateUser = Utility.GetUserId().ToString() + "_" + Utility.GetUserName();

            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.UpdateStockEntryBatchData(stock_entry_batch);
        }

        //新建入库数据
        public int AddStockEntryData(string dataJson)
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

            //根据batchId,查询入库批次数据
            string batchData = QueryStockEntryBatchDataById(batchId);
            JArray batchDataJson = JArray.Parse(batchData);
            string source = batchDataJson[0]["source"]?.ToString();

            //根据productId,查询产品信息
            string productData = QueryProductInfoById(productId);
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

            int stock = 0;
            string storageLoc = "";
            int storageLocId = 0;
            string ShelfProductMappingDataStr = QueryShelfProductMappingData(dataJson);
            JArray ShelfProductMappingDataJson = JArray.Parse(ShelfProductMappingDataStr);
            if (ShelfProductMappingDataJson.Count()>0)
            {
                int mappingId = int.Parse(ShelfProductMappingDataJson[0]["Id"]?.ToString());


                //根据storageLocId,查询货架产品关联表数据
                string shelfProductMappingData = QueryShelfProductMappingInfoById(mappingId.ToString());
                JArray shelfProductMappingDataJson = JArray.Parse(shelfProductMappingData);

                //实际入库数量
                int old_actualQuantity = 0;
                //入库数量
                int old_inboundQuantity = int.Parse(shelfProductMappingDataJson[0]["inboundQuantity"]?.ToString());
                //出库数量
                int old_outboundQuantity = int.Parse(shelfProductMappingDataJson[0]["outboundQuantity"]?.ToString());
                //剩余数量
                int old_remainingQuantity = int.Parse(shelfProductMappingDataJson[0]["remainingQuantity"]?.ToString());

                int inboundQuantity = old_inboundQuantity + int.Parse(quantity);
                int outboundQuantity = old_outboundQuantity;
                int remainingQuantity = old_remainingQuantity + int.Parse(quantity);
                int actualQuantity = old_inboundQuantity + int.Parse(quantity);

                data.Add("inboundQuantity", inboundQuantity.ToString());
                data.Add("outboundQuantity", outboundQuantity.ToString());
                data.Add("remainingQuantity", remainingQuantity.ToString());
                data.Add("actualQuantity", actualQuantity.ToString());

                stock = int.Parse(ShelfProductMappingDataJson[0]["total_productQuantity"]?.ToString());
                storageLocId = UpdateShelfProductMappingData(data.ToString(), mappingId);
            }
            else
            {
                //新建货架产品关联数据
                stock = 0;

                //实际入库数量
                int actualQuantity = 0;
                //入库数量
                int inboundQuantity = int.Parse(quantity);
                //出库数量
                int outboundQuantity = 0;
                //剩余数量
                int remainingQuantity = int.Parse(quantity);

                actualQuantity = int.Parse(quantity);

                data.Add("inboundQuantity", inboundQuantity.ToString());
                data.Add("outboundQuantity", outboundQuantity.ToString());
                data.Add("remainingQuantity", remainingQuantity.ToString());
                data.Add("actualQuantity", actualQuantity.ToString());

                storageLocId = AddShelfProductMappingData(data.ToString());
            }

            if (explain == "货架堆放")
            {
                storageLoc = shelf + "货架" + shelfLayer + "层";
            }
            else
            {
                storageLoc = nearShelf1 + "货架," + nearShelf2 + "货架附近";
            }

            //组装tb_stock_entry数据,把上面的数据都添加到jsonobject中
            JObject stockEntryData = new JObject();

            List<tb_stock_entry_Entity> stock_entry_List = new List<tb_stock_entry_Entity>();

            tb_stock_entry_Entity stock_entry = new tb_stock_entry_Entity();
            stock_entry.batchId = int.Parse(batchId);
            stock_entry.productName = productName;
            stock_entry.productId = int.Parse(productId);
            stock_entry.stock = stock;
            stock_entry.actualSize = int.Parse(actualSize);
            stock_entry.quantityIn = int.Parse(quantity);
            stock_entry.storageLoc = storageLoc;
            stock_entry.storageLocId = storageLocId;
            stock_entry.entryTime = DateTime.Now;
            stock_entry.source = source;
            stock_entry.status = 1;
            stock_entry.createUser = "admin";
            stock_entry.createTime = DateTime.Now;
            stock_entry.updateTime = DateTime.Now;
            stock_entry.updateUser = "admin";

            stock_entry_List.Add(stock_entry);

            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.AddStockEntryData(stock_entry_List);
        }

        //更新入库数据
        public int UpdateStockEntryData(string dataJson)
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

            //根据sockId,查询入库数据
            string stockEntryData = QueryStockEntryDataById(stockId);
            JArray stockEntryDataJson = JArray.Parse(stockEntryData);
            int old_productId = int.Parse(stockEntryDataJson[0]["productId"]?.ToString());
            int storageLocId = int.Parse(stockEntryDataJson[0]["storageLocId"]?.ToString());
            string createUser = stockEntryDataJson[0]["createUser"]?.ToString();
            int old_quantity = int.Parse(stockEntryDataJson[0]["quantityIn"]?.ToString());
            DateTime createTime = DateTime.Parse(stockEntryDataJson[0]["createTime"]?.ToString());
            DateTime entryTime = DateTime.Parse(stockEntryDataJson[0]["entryTime"]?.ToString());


            //根据batchId,查询入库批次数据
            string batchData = QueryStockEntryBatchDataById(batchId);
            JArray batchDataJson = JArray.Parse(batchData);
            string source = batchDataJson[0]["source"]?.ToString();

            //根据productId,查询产品信息
            string productData = QueryProductInfoById(productId);
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

            //更新操作之前,先在货架产品关联表中退回旧的入库数据
            //根据storageLocId,查询货架产品关联表数据
            string shelfProductMappingData1 = QueryShelfProductMappingInfoById(storageLocId.ToString());
            JArray shelfProductMappingDataJson1 = JArray.Parse(shelfProductMappingData1);
            JObject data1 = JObject.Parse(dataJson);
            //实际入库数量
            int old_actualQuantity = 0;
            //入库数量
            int old_inboundQuantity = int.Parse(shelfProductMappingDataJson1[0]["inboundQuantity"]?.ToString());
            //出库数量
            int old_outboundQuantity = int.Parse(shelfProductMappingDataJson1[0]["outboundQuantity"]?.ToString());
            //剩余数量
            int old_remainingQuantity = int.Parse(shelfProductMappingDataJson1[0]["remainingQuantity"]?.ToString());

            int inboundQuantity = old_inboundQuantity - old_quantity;
            int outboundQuantity = old_outboundQuantity;
            int remainingQuantity = old_remainingQuantity - old_quantity;
            int actualQuantity = old_inboundQuantity - old_quantity;

            tb_shelf_product_mapping_Entity shelf_product_mapping = new tb_shelf_product_mapping_Entity();

            shelf_product_mapping.Id = storageLocId;
            shelf_product_mapping.shelfId = int.Parse(shelfProductMappingDataJson1[0]["shelfId"]?.ToString());
            shelf_product_mapping.nearbyShelf = shelfProductMappingDataJson1[0]["nearbyShelf"]?.ToString();
            shelf_product_mapping.explain = shelfProductMappingDataJson1[0]["explain"]?.ToString();
            shelf_product_mapping.productId = old_productId;
            shelf_product_mapping.productQuantity = actualQuantity;
            shelf_product_mapping.inboundQuantity = inboundQuantity;
            shelf_product_mapping.outboundQuantity = outboundQuantity;
            shelf_product_mapping.remainingQuantity = remainingQuantity;
            shelf_product_mapping.storageStatus = shelfProductMappingDataJson1[0]["storageStatus"]?.ToString();
            shelf_product_mapping.status = int.Parse(shelfProductMappingDataJson1[0]["status"]?.ToString());
            shelf_product_mapping.createUser = shelfProductMappingDataJson1[0]["createUser"]?.ToString();
            shelf_product_mapping.createTime = DateTime.Parse(shelfProductMappingDataJson1[0]["createTime"]?.ToString());
            shelf_product_mapping.updateTime = DateTime.Now;
            shelf_product_mapping.updateUser = "admin";
            //UpdateShelfProductMappingData(shelf_product_mapping);

            if (inboundQuantity== 0 && outboundQuantity == 0 && outboundQuantity == 0 && remainingQuantity == 0 
                && int.Parse(quantity) != 0 && old_productId!= int.Parse(productId))
            {
                DeleteShelfProductMappingDataById(storageLocId);
            }
            else
            {
                UpdateShelfProductMappingData(shelf_product_mapping);
            }

            int stock = 0;
            string storageLoc = "";
            string ShelfProductMappingDataStr = QueryShelfProductMappingData(dataJson);
            JArray ShelfProductMappingDataJson = JArray.Parse(ShelfProductMappingDataStr);
            if (ShelfProductMappingDataJson.Count() > 0)
            {
                int mappingId = int.Parse(ShelfProductMappingDataJson[0]["Id"]?.ToString());


                //根据storageLocId,查询货架产品关联表数据
                string shelfProductMappingData = QueryShelfProductMappingInfoById(mappingId.ToString());
                JArray shelfProductMappingDataJson = JArray.Parse(shelfProductMappingData);
                //实际入库数量
                old_actualQuantity = 0;
                //入库数量
                old_inboundQuantity = int.Parse(shelfProductMappingDataJson[0]["inboundQuantity"]?.ToString());
                //出库数量
                old_outboundQuantity = int.Parse(shelfProductMappingDataJson[0]["outboundQuantity"]?.ToString());
                //剩余数量
                old_remainingQuantity = int.Parse(shelfProductMappingDataJson[0]["remainingQuantity"]?.ToString());

                inboundQuantity = old_inboundQuantity + int.Parse(quantity);
                outboundQuantity = old_outboundQuantity;
                remainingQuantity = old_remainingQuantity + int.Parse(quantity);
                actualQuantity = old_inboundQuantity + int.Parse(quantity);

                data.Add("inboundQuantity", inboundQuantity.ToString());
                data.Add("outboundQuantity", outboundQuantity.ToString());
                data.Add("remainingQuantity", remainingQuantity.ToString());
                data.Add("actualQuantity", actualQuantity.ToString());

                stock = int.Parse(ShelfProductMappingDataJson[0]["total_productQuantity"]?.ToString());
                storageLocId = UpdateShelfProductMappingData(data.ToString(), mappingId);
            }
            else
            {
                //新建货架产品关联数据
                stock = 0;

                //实际入库数量
                actualQuantity = 0;
                //入库数量
                inboundQuantity = int.Parse(quantity);
                //出库数量
                outboundQuantity = 0;
                //剩余数量
                remainingQuantity = int.Parse(quantity);

                actualQuantity = int.Parse(quantity);

                data.Add("inboundQuantity", inboundQuantity.ToString());
                data.Add("outboundQuantity", outboundQuantity.ToString());
                data.Add("remainingQuantity", remainingQuantity.ToString());
                data.Add("actualQuantity", actualQuantity.ToString());

                storageLocId = AddShelfProductMappingData(data.ToString());
            }

            if (explain == "货架堆放")
            {
                storageLoc = shelf + "货架" + shelfLayer + "层";
            }
            else
            {
                storageLoc = nearShelf1 + "货架," + nearShelf2 + "货架附近";
            }

            //组装tb_stock_entry数据,把上面的数据都添加到jsonobject中

            tb_stock_entry_Entity stock_entry = new tb_stock_entry_Entity();
            stock_entry.Id = int.Parse(stockId);
            stock_entry.batchId = int.Parse(batchId);
            stock_entry.productName = productName;
            stock_entry.productId = int.Parse(productId);
            stock_entry.stock = stock;
            stock_entry.actualSize = int.Parse(actualSize);
            stock_entry.quantityIn = int.Parse(quantity);
            stock_entry.storageLoc = storageLoc;
            stock_entry.storageLocId = storageLocId;
            stock_entry.entryTime = entryTime;
            stock_entry.source = source;
            stock_entry.status = 1;
            stock_entry.createUser = createUser;
            stock_entry.createTime = createTime;
            stock_entry.updateTime = DateTime.Now;
            stock_entry.updateUser = "admin";

            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.UpdateStockEntryData(stock_entry);
        }

        //查询入库数据,根据入库批次Id查询QueryStockEntryTableDataByBatchId
        public string QueryStockEntryDataByBatchId(string batchId)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryStockEntryDataByBatchId(batchId);
        }

        /// <summary>
        /// 查询入库数据,根据入库批次Id查询
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public TableData QueryStockEntryTableDataByBatchId(string batchId)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryStockEntryTableDataByBatchId(batchId);
        }

        //根据输入信息查询产品信息,输入信息可能是颜色或者颜色代码,模糊查询
        public string QueryProductInfoByInput(string selectProductType)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            string SqlWhere = " AND a.productType = '" + selectProductType + "' ";

            return stockEntryDAL.QueryProductInfoByInput(SqlWhere);
        }

        //根据输入信息查询货架信息,输入信息是货架名称
        public string QueryShelfInfoByInput(string input)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryShelfInfoByInput(input);
        }

        //根据输入信息查询货架信息,输入信息是货架名称和层级
        public string QueryShelfInfoByInput(string shelfName, string shelfLevel)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryShelfInfoByInput(shelfName, shelfLevel);
        }

        //根据入库批次ID,查询入库批次信息
        public string QueryStockEntryBatchDataById(string batchId)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryStockEntryBatchDataById(batchId);
        }

        //根据产品ID,查询产品信息
        public string QueryProductInfoById(string productId)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryProductInfoById(productId);
        }

        //根据货架ID,查询货架信息
        public string QueryShelfInfoById(string shelfId)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryShelfInfoById(shelfId);
        }

        //根据货架产品关联表ID,查询货架产品关联表信息
        public string QueryShelfProductMappingInfoById(string Id)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryShelfProductMappingInfoById(Id);
        }

        //根据入库数据ID,查询入库数据信息
        public string QueryStockEntryDataById(string Id)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryStockEntryDataById(Id);
        }

        //根据货架关联表ID,查询入库数据信息
        public string QueryStockEntryDataByMappingId(string MappingId)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.QueryStockEntryDataByMappingId(MappingId);
        }

        //查询货架产品关联表数据
        public string QueryShelfProductMappingData(string dataJson)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            JObject data = JObject.Parse(dataJson);

            string batchId = data["batchId"]?.ToString();
            string productId = data["productId"]?.ToString();
            string actualSize = data["actualSize"]?.ToString();
            string explain = data["explain"]?.ToString();
            string shelfId = data["shelfId"]?.ToString();
            string shelf = data["shelf"]?.ToString();
            string shelfLayer = data["shelfLayer"]?.ToString();
            string nearShelf1 = data["nearShelf1"]?.ToString();
            string nearShelf1Id = data["nearShelf1Id"]?.ToString();
            string nearShelf2 = data["nearShelf2"]?.ToString();
            string nearShelf2Id = data["nearShelf2Id"]?.ToString();

            string SqlStr = "";

            //判断explain是否是货架堆放
            if (explain == "货架堆放")
            {
                SqlStr = " SELECT                                                                      ";
                SqlStr += "     a.`explain`,                                                            ";
                SqlStr += "     a.Id,                                                            ";
                SqlStr += "     a.productQuantity AS total_productQuantity,                        ";
                SqlStr += "     a.remainingQuantity AS total_remainingQuantity,                      ";
                SqlStr += "     a.productId,                                                                ";
                SqlStr += "     b.color,                                                                ";
                SqlStr += "     b.colorCode,                                                            ";
                SqlStr += "     b.printCode,                                                            ";
                SqlStr += "     b.process,                                                              ";
                SqlStr += "     b.productType,                                                          ";
                SqlStr += "     b.size,                                                                 ";
                SqlStr += "     b.specification,                                                        ";
                //SqlStr += "     d.batchId,                                      ";
                SqlStr += "     d.actualSize                                      ";
                //SqlStr += "     c.shelfName,                                                            ";
                //SqlStr += "     c.shelfLevel                                                            ";
                SqlStr += " FROM                                                                        ";
                SqlStr += "     tb_shelf_product_mapping AS a                                           ";
                SqlStr += "         LEFT JOIN                                                           ";
                SqlStr += "     tb_product AS b ON a.productId = b.Id                                   ";
                SqlStr += "         LEFT JOIN                                                           ";
                SqlStr += "     tb_shelf AS c ON a.shelfId = c.Id                                       ";
                SqlStr += "         LEFT JOIN                                                           ";
                SqlStr += "     tb_stock_entry AS d ON a.Id = d.storageLocId                            ";
                SqlStr += " WHERE                                                                       ";
                SqlStr += "     a.`status` = '1' AND a.storageStatus = '"+ explain + "' AND c.shelfName = '"+ shelf + "' ";
                SqlStr += "         AND c.shelfLevel = '"+ shelfLayer + "'           ";
                if (!string.IsNullOrEmpty(productId))
                {
                    SqlStr += " AND a.productId = '" + productId + "' AND d.actualSize = '"+ actualSize + "' ";
                    //SqlStr += " AND d.batchId = '" + batchId + "' ";
                }
                SqlStr += " GROUP BY                                                                    ";
                SqlStr += "     a.`explain`,                                                            ";
                SqlStr += "     b.color,                                                                ";
                SqlStr += "     b.colorCode,                                                            ";
                SqlStr += "     b.printCode,                                                            ";
                SqlStr += "     b.process,                                                              ";
                SqlStr += "     b.productType,                                                          ";
                SqlStr += "     b.size,                                                                 ";
                SqlStr += "     b.specification,                                                        ";
                //SqlStr += "     d.batchId,                                                        ";
                SqlStr += "     d.actualSize                                                        ";
                //SqlStr += "     c.shelfName,                                                            ";
                //SqlStr += "     c.shelfLevel                                                            ";
            }
            else
            {
                SqlStr = "SELECT                                                   ";
                SqlStr += "    a.`explain`,                                         ";
                SqlStr += "    a.Id,                                          ";
                SqlStr += "    a.productQuantity total_productQuantity,       ";
                SqlStr += "    a.remainingQuantity total_remainingQuantity,   ";
                SqlStr += "     a.productId,                                                                ";
                SqlStr += "    b.color,                                             ";
                SqlStr += "    b.colorCode,                                         ";
                SqlStr += "    b.printCode,                                         ";
                SqlStr += "    b.process,                                           ";
                SqlStr += "    b.productType,                                       ";
                SqlStr += "    b.size,                                              ";
                SqlStr += "    b.specification,                                      ";
                //SqlStr += "    d.batchId,                                      ";
                SqlStr += "    d.actualSize                                      ";
                SqlStr += "FROM                                                     ";
                SqlStr += "    tb_shelf_product_mapping AS a                        ";
                SqlStr += "        LEFT JOIN                                        ";
                SqlStr += "    tb_product AS b ON a.productId = b.Id                ";
                SqlStr += "         LEFT JOIN                                                           ";
                SqlStr += "     tb_stock_entry AS d ON a.Id = d.storageLocId                            ";
                SqlStr += "WHERE                                                    ";
                SqlStr += "    a.`status` = '1' AND a.storageStatus = '"+ explain + "'  ";
                SqlStr += "        AND a.nearbyShelf = '"+ nearShelf1Id + ","+ nearShelf2Id + "'  ";
                if (!string.IsNullOrEmpty(productId))
                {
                    SqlStr += " AND a.productId = '" + productId + "' AND d.actualSize = '" + actualSize + "' ";
                    //SqlStr += " AND d.batchId = '" + batchId + "' ";
                }
                SqlStr += "GROUP BY                                                 ";
                SqlStr += "    a.`explain`,                                         ";
                SqlStr += "    b.color,                                             ";
                SqlStr += "    b.colorCode,                                         ";
                SqlStr += "    b.printCode,                                         ";
                SqlStr += "    b.process,                                           ";
                SqlStr += "    b.productType,                                       ";
                SqlStr += "    b.size,                                              ";
                SqlStr += "    b.specification,                                      ";
                //SqlStr += "     d.batchId,                                                        ";
                SqlStr += "    d.actualSize                                                        ";
            }


            return stockEntryDAL.QueryShelfProductMappingData(SqlStr);

        }

        //QueryStockEntryDataByStockId
        public string QueryStockEntryDataByStockId(string stockId)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            string sqlStr = "";

            sqlStr = @"SELECT 
                           a.batchId,
                           a.quantityIn,
                           a.productId,
                           a.storageLocId,
                           a.actualSize,
                           b.productType,
                           c.shelfId,
                           c.nearbyShelf,
                           c.productQuantity,
                           c.inboundQuantity,
                           c.remainingQuantity,
                           c.storageStatus,
                           d.Id AS shelf_Id,
                           d.shelfName,
                           d.shelfLevel
                           FROM
                           tb_stock_entry AS a
                           LEFT JOIN
                           tb_product AS b ON a.productId=b.Id
                           LEFT JOIN 
                           tb_shelf_product_mapping AS c ON a.storageLocId=c.Id
                           LEFT JOIN 
                           tb_shelf AS d ON c.shelfId=d.Id
                           WHERE 1=1 AND a.`status`='1' AND c.`status`='1'
                           ";
            sqlStr += " AND a.Id = '" + stockId + "' ";




            return stockEntryDAL.QueryStockEntryDataByStockId(sqlStr);
        }

        //AddShelfProductMappingData
        public int AddShelfProductMappingData(string dataJson)
        {
            JObject data = JObject.Parse(dataJson);

            string batchId = data["batchId"]?.ToString();
            string productType = data["productType"]?.ToString();
            string productId = data["productId"]?.ToString();
            string productText = data["productText"]?.ToString();
            string quantity = data["quantity"]?.ToString();
            string inboundQuantity = data["inboundQuantity"]?.ToString();
            string outboundQuantity = data["outboundQuantity"]?.ToString();
            string remainingQuantity = data["remainingQuantity"]?.ToString();
            string actualQuantity = data["actualQuantity"]?.ToString();
            string explain = data["explain"]?.ToString();
            string shelf = data["shelf"]?.ToString();
            string shelfLayer = data["shelfLayer"]?.ToString();
            string nearShelf1 = data["nearShelf1"]?.ToString();
            string nearShelf1Id = data["nearShelf1Id"]?.ToString();
            string nearShelf2 = data["nearShelf2"]?.ToString();
            string nearShelf2Id = data["nearShelf2Id"]?.ToString();

            tb_shelf_product_mapping_Entity shelf_product_mapping = new tb_shelf_product_mapping_Entity();
            if (explain == "货架堆放") 
            {
                //根据货架名称和层级查询货架ID
                string shelfData = QueryShelfInfoByInput(shelf, shelfLayer);
                JArray shelfDataJson = JArray.Parse(shelfData);
                int shelfId = int.Parse(shelfDataJson[0]["Id"].ToString());

                shelf_product_mapping.shelfId = shelfId;
                //shelf_product_mapping.nearbyShelf = nearShelf1Id + "," + nearShelf2Id;
                shelf_product_mapping.explain = shelf+"货架"+ shelfLayer+"层";
            }
            else
            {
                //shelf_product_mapping.shelfId = null;
                shelf_product_mapping.nearbyShelf = nearShelf1Id + "," + nearShelf2Id;
                shelf_product_mapping.explain = nearShelf1+"货架,"+ nearShelf2+"货架附近";
            }
            shelf_product_mapping.productId = int.Parse(productId);
            shelf_product_mapping.productQuantity = int.Parse(inboundQuantity);
            shelf_product_mapping.inboundQuantity = int.Parse(inboundQuantity);
            shelf_product_mapping.outboundQuantity = int.Parse(outboundQuantity);
            shelf_product_mapping.remainingQuantity = int.Parse(remainingQuantity);
            shelf_product_mapping.storageStatus = explain;
            shelf_product_mapping.status = 1;
            shelf_product_mapping.createUser = "admin";
            shelf_product_mapping.createTime = DateTime.Now;
            shelf_product_mapping.updateTime = DateTime.Now;
            shelf_product_mapping.updateUser = "admin";


            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.AddShelfProductMappingData(shelf_product_mapping);
        }

        //UpdateShelfProductMappingData
        public int UpdateShelfProductMappingData(string dataJson,int storageLocId)
        {
            JObject data = JObject.Parse(dataJson);

            string batchId = data["batchId"]?.ToString();
            string productType = data["productType"]?.ToString();
            string productId = data["productId"]?.ToString();
            string productText = data["productText"]?.ToString();
            string quantity = data["quantity"]?.ToString();
            string inboundQuantity = data["inboundQuantity"]?.ToString();
            string outboundQuantity = data["outboundQuantity"]?.ToString();
            string remainingQuantity = data["remainingQuantity"]?.ToString();
            string actualQuantity = data["actualQuantity"]?.ToString();
            string explain = data["explain"]?.ToString();
            string shelf = data["shelf"]?.ToString();
            string shelfLayer = data["shelfLayer"]?.ToString();
            string nearShelf1 = data["nearShelf1"]?.ToString();
            string nearShelf1Id = data["nearShelf1Id"]?.ToString();
            string nearShelf2 = data["nearShelf2"]?.ToString();
            string nearShelf2Id = data["nearShelf2Id"]?.ToString();

            //根据storageLocId,查询货架产品关联表数据
            string shelfProductMappingData = QueryShelfProductMappingInfoById(storageLocId.ToString());
            JArray shelfProductMappingDataJson = JArray.Parse(shelfProductMappingData);
            string createUser = shelfProductMappingDataJson[0]["createUser"]?.ToString();
            DateTime createTime = DateTime.Parse(shelfProductMappingDataJson[0]["createTime"]?.ToString());

            tb_shelf_product_mapping_Entity shelf_product_mapping = new tb_shelf_product_mapping_Entity();
            if (explain == "货架堆放")
            {
                //根据货架名称和层级查询货架ID
                string shelfData = QueryShelfInfoByInput(shelf, shelfLayer);
                JArray shelfDataJson = JArray.Parse(shelfData);
                int shelfId = int.Parse(shelfDataJson[0]["Id"].ToString());

                shelf_product_mapping.shelfId = shelfId;
                //shelf_product_mapping.nearbyShelf = nearShelf1Id + "," + nearShelf2Id;
                shelf_product_mapping.explain = shelf + "货架" + shelfLayer + "层";
            }
            else
            {
                //shelf_product_mapping.shelfId = null;
                shelf_product_mapping.nearbyShelf = nearShelf1Id + "," + nearShelf2Id;
                shelf_product_mapping.explain = nearShelf1 + "货架," + nearShelf2 + "货架附近";
            }
            shelf_product_mapping.Id = storageLocId;
            shelf_product_mapping.productId = int.Parse(productId);
            shelf_product_mapping.productQuantity = int.Parse(actualQuantity);
            shelf_product_mapping.inboundQuantity = int.Parse(inboundQuantity);
            shelf_product_mapping.outboundQuantity = int.Parse(outboundQuantity);
            shelf_product_mapping.remainingQuantity = int.Parse(remainingQuantity);
            shelf_product_mapping.storageStatus = explain;
            shelf_product_mapping.status = 1;
            shelf_product_mapping.createUser = createUser;
            shelf_product_mapping.createTime = createTime;
            shelf_product_mapping.updateTime = DateTime.Now;
            shelf_product_mapping.updateUser = "admin";


            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            int updateCount = stockEntryDAL.UpdateShelfProductMappingData(shelf_product_mapping);

            return storageLocId;
        }

        //更新货架产品关联表数据
        public int UpdateShelfProductMappingData(tb_shelf_product_mapping_Entity shelf_product_mappingModel)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.UpdateShelfProductMappingData(shelf_product_mappingModel);
        }

        //删除入库数据,根据ID
        public string DeleteStockEntryDataById(int Id)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            //根据sockId,查询入库数据
            string stockEntryData = QueryStockEntryDataById(Id.ToString());
            JArray stockEntryDataJson = JArray.Parse(stockEntryData);
            int old_productId = int.Parse(stockEntryDataJson[0]["productId"]?.ToString());
            int storageLocId = int.Parse(stockEntryDataJson[0]["storageLocId"]?.ToString());
            string createUser = stockEntryDataJson[0]["createUser"]?.ToString();
            int old_quantity = int.Parse(stockEntryDataJson[0]["quantityIn"]?.ToString());
            DateTime createTime = DateTime.Parse(stockEntryDataJson[0]["createTime"]?.ToString());
            DateTime entryTime = DateTime.Parse(stockEntryDataJson[0]["entryTime"]?.ToString());

            //更新操作之前,先在货架产品关联表中退回旧的入库数据
            //根据storageLocId,查询货架产品关联表数据
            string shelfProductMappingData1 = QueryShelfProductMappingInfoById(storageLocId.ToString());
            JArray shelfProductMappingDataJson1 = JArray.Parse(shelfProductMappingData1);

            //实际入库数量
            int old_actualQuantity = 0;
            //入库数量
            int old_inboundQuantity = int.Parse(shelfProductMappingDataJson1[0]["inboundQuantity"]?.ToString());
            //出库数量
            int old_outboundQuantity = int.Parse(shelfProductMappingDataJson1[0]["outboundQuantity"]?.ToString());
            //剩余数量
            int old_remainingQuantity = int.Parse(shelfProductMappingDataJson1[0]["remainingQuantity"]?.ToString());

            int inboundQuantity = old_inboundQuantity - old_quantity;
            int outboundQuantity = old_outboundQuantity;
            int remainingQuantity = old_remainingQuantity - old_quantity;
            int actualQuantity = old_inboundQuantity - old_quantity;

            tb_shelf_product_mapping_Entity shelf_product_mapping = new tb_shelf_product_mapping_Entity();

            shelf_product_mapping.Id = storageLocId;
            shelf_product_mapping.shelfId = int.Parse(shelfProductMappingDataJson1[0]["shelfId"]?.ToString());
            shelf_product_mapping.nearbyShelf = shelfProductMappingDataJson1[0]["nearbyShelf"]?.ToString();
            shelf_product_mapping.explain = shelfProductMappingDataJson1[0]["explain"]?.ToString();
            shelf_product_mapping.productId = old_productId;
            shelf_product_mapping.productQuantity = actualQuantity;
            shelf_product_mapping.inboundQuantity = inboundQuantity;
            shelf_product_mapping.outboundQuantity = outboundQuantity;
            shelf_product_mapping.remainingQuantity = remainingQuantity;
            shelf_product_mapping.storageStatus = shelfProductMappingDataJson1[0]["storageStatus"]?.ToString();
            shelf_product_mapping.status = int.Parse(shelfProductMappingDataJson1[0]["status"]?.ToString());
            shelf_product_mapping.createUser = shelfProductMappingDataJson1[0]["createUser"]?.ToString();
            shelf_product_mapping.createTime = DateTime.Parse(shelfProductMappingDataJson1[0]["createTime"]?.ToString());
            shelf_product_mapping.updateTime = DateTime.Now;
            shelf_product_mapping.updateUser = "admin";


            int delete_stockEntryCount = stockEntryDAL.DeleteStockEntryDataById(Id);

            //根据storageLocId查询入库数据,若存在入库数据,则更新货架关联表数据,若不存在,则删除货架关联表数据
            string StockEntryData = QueryStockEntryDataByMappingId(storageLocId.ToString());
            JArray StockEntryDataJson = JArray.Parse(StockEntryData);
            if (StockEntryDataJson.Count() > 0)
            {

                UpdateShelfProductMappingData(shelf_product_mapping);
            }
            else
            {
                DeleteShelfProductMappingDataById(storageLocId);
            }

            //创建json对象,返回处理信息
            JObject result = new JObject();

            if (delete_stockEntryCount > 0 )
            {
                result["status"] = "success";
            }
            else
            {
                result["status"] = "fail";
            }
            return result.ToString();
        }

        //删除货架产品关联表数据,根据ID
        public int DeleteShelfProductMappingDataById(int Id)
        {
            StockEntryDAL stockEntryDAL = new StockEntryDAL();

            return stockEntryDAL.DeleteShelfProductMappingDataById(Id);
        }

    }
}
