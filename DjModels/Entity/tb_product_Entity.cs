using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjModels.Entity
{
    [SugarTable("tb_product")] // 指定数据库表名
    public class tb_product_Entity
    {
        // 产品ID, 自增ID
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        // 产品类型（正品，B级布）
        public string productType { get; set; }

        // 产品类型Id
        public string productTypeId { get; set; }

        // 颜色
        public string color { get; set; }

        // 色号
        public string colorCode { get; set; }

        // 加工工艺
        public string process { get; set; }

        // 印花版号
        public string printCode { get; set; }

        // 规格
        public string specification { get; set; }

        // 码数
        public string size { get; set; }

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
