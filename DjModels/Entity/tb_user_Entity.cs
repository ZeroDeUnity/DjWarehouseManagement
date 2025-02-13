using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjModels.Entity
{
    [SugarTable("tb_user")] // 指定数据库表名
    public class tb_user_Entity
    {
        // 用户ID，自增ID
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        // 用户名
        public string userName { get; set; }

        // 用户密码（建议存储加密后的密码）
        public string passWord { get; set; }

        // 用户邮箱
        public string email { get; set; }

        // 用户手机号
        public string phone { get; set; }

        // 用户数据状态(0:已删除,1:正常)
        public int status { get; set; }

        // 上次登录时间
        public DateTime? last_login_time { get; set; }

        // 创建人（可以记录创建此用户账号的操作人员）
        public string createUser { get; set; }

        // 创建时间
        public DateTime createTime { get; set; }

        // 更新人（记录最后修改用户信息的操作人员）
        public string updateUser { get; set; }

        // 更新时间
        public DateTime? updateTime { get; set; }
    }
}

