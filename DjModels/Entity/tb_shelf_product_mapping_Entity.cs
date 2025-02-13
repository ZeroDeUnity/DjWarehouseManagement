using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjModels.Entity
{
    [SugarTable("tb_shelf_product_mapping")] // 指定数据库表名
    public class tb_shelf_product_mapping_Entity
    {
        // ID, 自增ID
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        // 货架ID
        public int shelfId { get; set; }

        //邻近货架,可输入多个货架,用以定位堆放在货架附近的产品
        public string nearbyShelf { get; set; }

        //解释,说明,用以描述具体位置
        public string explain { get; set; }

        // 产品ID
        public int productId { get; set; }

        // 产品数量
        public int productQuantity { get; set; }

        // 入库数量
        public int inboundQuantity { get; set; }

        // 出库数量
        public int outboundQuantity { get; set; }

        // 剩余数量
        public int remainingQuantity { get; set; }

        //产品存放的状态(货架堆放,货架附近堆放)
        public string storageStatus { get; set; }

        // 数据状态(0:已删除,1:正常)
        public int status { get; set; }

        // 创建人
        public string createUser { get; set; }

        // 创建时间
        public DateTime createTime { get; set; }

        // 更新人
        public string updateUser { get; set; }

        // 更新时间
        public DateTime updateTime { get; set; }
    }
}
