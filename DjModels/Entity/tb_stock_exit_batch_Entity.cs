using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjModels.Entity
{
    [SugarTable("tb_stock_exit_batch")] // 指定数据库表名
    public class tb_stock_exit_batch_Entity
    {
        // 出库批次ID, 自增ID
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        // 批次代码，唯一标识该出库批次
        public string batchCode { get; set; }

        //订单号
        public string orderId { get; set; }

        // 出库时间
        public DateTime exitTime { get; set; }

        // 总出库量
        public int totalQuantity { get; set; }

        // 出库目标信息
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
