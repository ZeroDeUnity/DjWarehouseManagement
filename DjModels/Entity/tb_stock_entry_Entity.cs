using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjModels.Entity
{
    [SugarTable("tb_stock_entry")] // 指定数据库表名
    public class tb_stock_entry_Entity
    {
        // 入库记录ID, 自增ID
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        //批次Id
        public int batchId { get; set; }

        // 产品名称
        public string productName { get; set; }

        // 产品ID
        public int productId { get; set; }
        
        //实际码数
        public int actualSize { get; set; }

        // 当前库存
        public int stock { get; set; }

        // 本次入库量
        public int quantityIn { get; set; }

        // 入库仓位
        public string storageLoc { get; set; }

        // 仓位ID
        public int storageLocId { get; set; }

        // 入库时间
        public DateTime entryTime { get; set; }

        // 产品来源
        public string source { get; set; }

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
