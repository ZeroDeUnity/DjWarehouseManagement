using DjDAL.DBHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjDAL
{
    public class UserDAL
    {
        //用户登录,根据用户名和密码查询用户信息
        public string UserLogin(string username, string password)
        {


            string SqlStr = "select * from tb_user where username='" + username + "' and password='" + password + "'";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
        }

    }
}
