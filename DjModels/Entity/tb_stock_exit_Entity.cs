using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjModels.Entity
{
    [SugarTable("tb_stock_exit")] // 指定数据库表名
    public class tb_stock_exit_Entity
    {
        // 出库记录ID, 自增ID
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        //批次ID
        public int batchId { get; set; }

        // 产品名称
        public string productName { get; set; }

        // 产品ID
        public int productId { get; set; }

        //实际码数
        public int actualSize { get; set; }

        // 当前库存
        public int stock { get; set; }

        // 本次出库量
        public int quantityOut { get; set; }

        // 出库仓位
        public string storageLoc { get; set; }

        // 仓位ID,由多个数据组成格式为(仓位ID,数量):1_20,2_12,5_6
        public int storageLocId { get; set; }

        // 出库时间
        public DateTime exitTime { get; set; }

        // 出库目标
        public string destination { get; set; }

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
