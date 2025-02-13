using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjModels.Entity
{
    [SugarTable("dbstudent")]//当和数据库名称不一样可以设置表别名 指定表明
    public class tb_shelf_Entity
    {
        //货架ID, 自增ID
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//数据库是自增才配自增 

        public int Id { get; set; }

        //货架名称
        public string shelfName { get; set; }

        //货架层级
        public int shelfLevel { get; set; }

        //数据状态(0:已删除,1:正常)
        public int status { get; set; }

        //创建人
        public string createUser { get; set; }

        //创建时间
        public DateTime createTime { get; set; }

        //更新人
        public string updateUser { get; set; }

        //更新时间
        public DateTime updateTime { get; set; }
    }
}
