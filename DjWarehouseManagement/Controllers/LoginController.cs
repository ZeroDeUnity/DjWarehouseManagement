using DjBLL;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DjWarehouseManagement.Controllers
{
    public class LoginController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginController));

        // GET: Login
        /*[AllowAnonymous]*/
        public ActionResult Index()
        {
            return View();
        }

        // GET: Index_New
        [AllowAnonymous]
        public ActionResult Index_New()
        {
            return View();
        }

        // GET: Login_res
        [AllowAnonymous]
        public ActionResult Login_res()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>返回true和false</returns>
        [AllowAnonymous]
        [HttpPost]
        public string UserLogin(string username, string password)
        {
            UserBLL userBLL = new UserBLL();

            try
            {
                //根据用户名和密码进行查询,返回用户信息
                string userData = userBLL.UserLogin(username, password);

                //判断用户信息是否为空,转换为json对象,判断是否为空
                JArray userDataJson = JArray.Parse(userData);
                if (userDataJson.Count == 0)
                {
                    return "false";
                }
                else
                {
                    //获取用户Id
                    int Id = Convert.ToInt32(userDataJson[0]["Id"].ToString());
                    //获取用户名
                    string userName = userDataJson[0]["userName"].ToString();

                    HttpCookie cookie = new HttpCookie("Id", Convert.ToString(Id));
                    cookie.Expires = DateTime.Now.AddMinutes(10 * 60);//30分钟的co0kie
                    System.Web.HttpContext.Current.Response.AppendCookie(cookie); //写入Cookie
                    HttpCookie cookie1 = new HttpCookie("userName", userName);
                    cookie1.Expires = DateTime.Now.AddMinutes(10 * 60);//30分钟的co0kie
                    System.Web.HttpContext.Current.Response.AppendCookie(cookie1); //写入Cookie

                    return "true";
                }
            }
            catch (Exception ex)
            {
                log.Error("用户:"+ username + " 登录异常", ex);
                return "false";
            }


        }

    }
}