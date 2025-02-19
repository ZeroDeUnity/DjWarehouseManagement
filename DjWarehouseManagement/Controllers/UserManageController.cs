using DjBLL;
using DjModels.Entity;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DjDAL.Utility.TableData_Utility;
using DjDAL.Utility;
using DjDAL;

namespace DjWarehouseManagement.Controllers
{
    public class UserManageController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserManageController));

        // GET: Login
        /*[AllowAnonymous]*/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index_Add()
        {
            return View();
        }

        public ActionResult Index_Update() {

            // 获取URL参数
            string userId = Request.QueryString["userId"];

            // 将参数传递给视图
            ViewBag.userId = userId;

            return View();
        }

        /// <summary>
        /// 查询用户数据
        /// </summary>
        /// <param name="selectInput"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryUserTableData(string selectInput)
        {
            string retStr = "";
            try
            {
                UserBLL userBLL = new UserBLL();

                TableData tableData = new TableData();

                tableData = userBLL.QueryUserTableData(selectInput);

                // 直接访问静态类的属性来进行序列化
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("查询用户数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 创建用户数据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">用户密码</param>
        /// <param name="email">用户邮箱</param>
        /// <param name="phone">用户手机号</param>
        /// <returns></returns>
        [HttpPost]
        public string CreateUserData(string userName, string passWord, string email, string phone, string[] userTypes)
        {
            string retStr = "";

            try
            {
                UserBLL userBLL = new UserBLL();

                //组装List<tb_user_Entity> user_entry_List
                List<tb_user_Entity> user_entry_List = new List<tb_user_Entity>();
                tb_user_Entity user_entry = new tb_user_Entity();

                user_entry.userName = userName;
                user_entry.passWord = passWord;
                user_entry.email = email;
                user_entry.phone = phone;
                user_entry.status = 1;
                user_entry.createTime = DateTime.Now;
                user_entry.updateTime = DateTime.Now;
                user_entry.createUser = Utility.GetUserIdAndName();
                user_entry.updateUser = Utility.GetUserIdAndName();

                user_entry_List.Add(user_entry);

                TableData tableData = new TableData();

                tb_user_Entity user_Entity = new tb_user_Entity();

                user_Entity = userBLL.AddUserData(user_entry_List);

                List<tb_user_type_Entity> user_type_List = new List<tb_user_type_Entity>();

                foreach (string role in userTypes) {
                    tb_user_type_Entity tb_User_Type_Entity = new tb_user_type_Entity();
                    tb_User_Type_Entity.userId = user_Entity.Id;
                    tb_User_Type_Entity.roleType = role;
                    tb_User_Type_Entity.status = 1;
                    tb_User_Type_Entity.createTime = DateTime.Now;
                    tb_User_Type_Entity.updateTime = DateTime.Now;
                    tb_User_Type_Entity.createUser = Utility.GetUserIdAndName();
                    tb_User_Type_Entity.updateUser = Utility.GetUserIdAndName();
                    user_type_List.Add(tb_User_Type_Entity);
                }

                tableData = userBLL.AddUserTypeData(user_type_List);

                // 直接访问静态类的属性来进行序列化
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);

            }
            catch (Exception ex)
            {
                log.Error("创建用户数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 根据用户 ID 获取用户数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns>包含用户数据的 JSON 字符串</returns>
        [HttpPost]
        public string GetUserDataById(string userId)
        {
            string retStr = "";
            try
            {
                UserBLL userBLL = new UserBLL();

                TableData tableData = new TableData();

                // 调用 GetUserById 方法获取用户数据
                tableData = userBLL.GetUserById(int.Parse(userId));

                // 将用户数据序列化为 JSON 字符串
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                // 记录错误日志
                log.Error("根据用户 ID 获取用户数据 error: " + ex.Message);
            }
            return retStr;
        }

        /// <summary>
        /// 更新用户数据
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">用户密码</param>
        /// <param name="email">用户邮箱</param>
        /// <param name="phone">用户手机号</param>
        /// <param name="userTypes">角色类型</param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateUserData(string Id, string userName, string passWord, string email, string phone, string[] userTypes)
        {
            string retStr = "";

            try
            {
                UserBLL userBLL = new UserBLL();

                //组装List<tb_user_Entity> user_entry_List
                List<tb_user_Entity> user_entry_List = new List<tb_user_Entity>();
                tb_user_Entity user_entry = new tb_user_Entity();

                user_entry.Id = int.Parse(Id);
                user_entry.userName = userName;
                user_entry.passWord = passWord;
                user_entry.email = email;
                user_entry.phone = phone;
                user_entry.status = 1;
                user_entry.updateTime = DateTime.Now;
                user_entry.updateUser = Utility.GetUserIdAndName();

                user_entry_List.Add(user_entry);

                TableData tableData = new TableData();

                tableData = userBLL.UpdateUserData(user_entry);

                //删除该用户所有用户类型
                userBLL.DeleteUserTypeByUserId(user_entry.Id);

                List<tb_user_type_Entity> user_type_List = new List<tb_user_type_Entity>();

                foreach (string role in userTypes)
                {
                    tb_user_type_Entity tb_User_Type_Entity = new tb_user_type_Entity();
                    tb_User_Type_Entity.userId = user_entry.Id;
                    tb_User_Type_Entity.roleType = role;
                    tb_User_Type_Entity.status = 1;
                    tb_User_Type_Entity.createTime = DateTime.Now;
                    tb_User_Type_Entity.updateTime = DateTime.Now;
                    tb_User_Type_Entity.createUser = Utility.GetUserIdAndName();
                    tb_User_Type_Entity.updateUser = Utility.GetUserIdAndName();
                    user_type_List.Add(tb_User_Type_Entity);
                }

                tableData = userBLL.AddUserTypeData(user_type_List);

                // 直接访问静态类的属性来进行序列化
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);

            }
            catch (Exception ex)
            {
                log.Error("创建用户数据", ex);
                return retStr;
            }

            return retStr;
        }

        /// <summary>
        /// 根据用户名查询用户数据
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        public string QueryUserTableDataByUserName(string userName)
        {
            string retStr = "";
            try
            {
                UserBLL userBLL = new UserBLL();
                TableData tableData = new TableData();
                tableData = userBLL.QueryUserTableDataByUserName(userName);
                // 直接访问静态类的属性来进行序列化
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("查询用户数据", ex);
                return retStr;
            }
            return retStr;
        }

        //DeleteUser,参数是userId
        [HttpPost]
        public string DeleteUser(string userId)
        {
            string retStr = "";
            try
            {
                UserBLL userBLL = new UserBLL();
                TableData tableData = new TableData();
                tableData = userBLL.DeleteUserById(int.Parse(userId));
                // 直接访问静态类的属性来进行序列化
                retStr = JsonConvert.SerializeObject(tableData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                log.Error("删除用户数据", ex);
                return retStr;
            }
            return retStr;
        }



    }
}