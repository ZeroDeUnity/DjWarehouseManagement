using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjDAL.Utility
{
    public class TableData_Utility
    {
        /// <summary>
        /// 数据表格数据
        /// </summary>
        public class TableData
        {
            /// <summary>
            /// 总数
            /// </summary>
            public int Total { get; set; }
            /// <summary>
            /// 行数据
            /// </summary>
            public string Rows { get; set; } // 改为 string 类型
            /// <summary>
            /// 处理状态
            /// </summary>
            public string Status { get; set; } // 改为 string 类型
            /// <summary>
            /// 返回信息
            /// </summary>
            public string ReturnMsg { get; set; } // 改为 string 类型
        }
    }
}
