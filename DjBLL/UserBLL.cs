using DjDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DjBLL
{
    public class UserBLL
    {
        //用户登录,根据用户名和密码查询用户信息
        public string UserLogin(string username, string password)
        {
            UserDAL userDAL = new UserDAL();

            return userDAL.UserLogin(username, password);
        }
    }
}
