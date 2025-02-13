using DjDAL;
using DjDAL.Utility;
using DjModels.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DjDAL.Utility.TableData_Utility;

namespace DjBLL
{
    public class InventoryQueryBLL
    {
        //查询库存数据,返回json格式数据
        public string QueryInventoryData(string selectInput,string selectType, string selectColor)
        {
            InventoryQueryDAL inventoryQueryDAL = new InventoryQueryDAL();

            return inventoryQueryDAL.QueryInventoryData(selectInput, selectType, selectColor);
        }

        //查询库存数据,返回数据
        public TableData QueryInventoryTableData(string selectInput, string selectType, string selectColor)
        {
            InventoryQueryDAL inventoryQueryDAL = new InventoryQueryDAL();

            return inventoryQueryDAL.QueryInventoryTableData(selectInput, selectType, selectColor);
        }

        public string QueryProductData(string selectInput)
        {
            InventoryQueryDAL inventoryQueryDAL = new InventoryQueryDAL();

            return inventoryQueryDAL.QueryProductData(selectInput);
        }

        /// <summary>
        /// 查询产品数据
        /// </summary>
        /// <param name="selectInput"></param>
        /// <returns></returns>
        public TableData QueryProductTableData(string selectInput)
        {
            InventoryQueryDAL inventoryQueryDAL = new InventoryQueryDAL();

            return inventoryQueryDAL.QueryProductTableData(selectInput);
        }

        public int CreateProductData(string productType, string color, string colorCode, string process, string printCode, string specification, string size)
        {
            InventoryQueryDAL inventoryQueryDAL = new InventoryQueryDAL();

            //组装List<tb_product_Entity> product_entry_List
            List<tb_product_Entity> product_entry_List = new List<tb_product_Entity>();
            tb_product_Entity product_entry = new tb_product_Entity();

            product_entry.productType = productType;
            product_entry.color = color;
            product_entry.colorCode = colorCode;
            product_entry.process = process;
            product_entry.printCode = printCode;
            product_entry.specification = specification;
            product_entry.size = size;
            product_entry.status = 1;
            product_entry.createTime = DateTime.Now;
            product_entry.updateTime = DateTime.Now;
            product_entry.createUser = "admin";
            product_entry.updateUser = "admin";

            product_entry_List.Add(product_entry);

            return inventoryQueryDAL.CreateProductData(product_entry_List);
        }

        /// <summary>
        /// 关联产品类型数据,根据输入的产品类型ID,结合产品ID,把产品类型ID更新到产品表中
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public int UpdateProductTypeData(string productTypeId, string productId)
        {
            InventoryQueryDAL inventoryQueryDAL = new InventoryQueryDAL();

            return inventoryQueryDAL.UpdateProductTypeData(productTypeId, productId);
        }
    }
}
