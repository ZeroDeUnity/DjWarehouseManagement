using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjModels.Entity
{
    [SugarTable("tb_user_type")] // 指定数据库表名
    public class tb_user_type_Entity
    {
        // 角色类型 ID，自增 ID
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        // 用户 ID
        public int userId { get; set; }

        // 角色类型（系统管理, 仓库管理, 车间管理, 普通）
        public string roleType { get; set; }

        // 数据状态（0: 已删除, 1: 正常），默认值为 1
        [SugarColumn(DefaultValue = "1")]
        public int status { get; set; }

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

