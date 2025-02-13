using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjModels.Entity
{
    [SugarTable("tb_product_type")] // 指定数据库表名
    public class tb_product_type_Entity
    {
        // 产品类型ID, 自增ID
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        // 产品类型名称
        public string typeName { get; set; }

        // 类型描述
        public string description { get; set; }

        // 状态（0: 停用, 1: 启用）
        public int status { get; set; } = 1;

        // 创建人
        public string createUser { get; set; }

        // 创建时间
        public DateTime createTime { get; set; }

        // 更新人
        public string updateUser { get; set; }

        // 更新时间
        public DateTime? updateTime { get; set; }
    }
}
