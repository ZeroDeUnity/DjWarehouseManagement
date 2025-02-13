using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjModels.Entity
{
    [SugarTable("tb_product_price_history")] // 指定数据库表名
    public class tb_product_price_history_Entity
    {
        // 价格历史ID, 自增ID
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        // 产品类型ID, 关联产品类型表(tb_product_type.Id)
        public int productTypeId { get; set; }

        // 销售价
        public decimal salePrice { get; set; }

        // 报价
        public decimal offerPrice { get; set; }

        // 价格生效时间
        public DateTime startDate { get; set; }

        // 价格结束时间, NULL表示当前价格生效中
        public DateTime? endDate { get; set; }

        // 状态（1:有效, 0:撤销）
        public int status { get; set; } = 1;

        // 创建人
        public string createUser { get; set; }

        // 创建时间
        public DateTime createTime { get; set; }
    }
}
