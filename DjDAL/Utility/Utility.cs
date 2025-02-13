using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjDAL.Utility
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 从cookie中获取登录的用户的Id
        /// </summary>
        /// <returns></returns>
        public static int GetUserId()
        {
            int userId = 0;
            if (System.Web.HttpContext.Current.Request.Cookies["Id"] != null)
            {
                userId = Convert.ToInt32(System.Web.HttpContext.Current.Request.Cookies["Id"].Value);
            }
            return userId;
        }

        /// <summary>
        /// 从cookie中获取登录的用户的userName
        /// </summary>
        /// <returns></returns>
        public static string GetUserName() 
        {
            string userName = "";
            if (System.Web.HttpContext.Current.Request.Cookies["userName"] != null)
            {
                userName = System.Web.HttpContext.Current.Request.Cookies["userName"].Value;
            }
            return userName;
        }

    }
}
