using DjDAL;
using DjDAL.DBHelper;
using DjModels.Entity;
using log4net;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DjDAL.Utility.TableData_Utility;


namespace DjBLL
{
    public class UserBLL
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserBLL));

        //用户登录,根据用户名和密码查询用户信息
        public string UserLogin(string username, string password)
        {
            UserDAL userDAL = new UserDAL();

            return userDAL.UserLogin(username, password);
        }

        /// <summary>
        /// 查询用户数据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public TableData QueryUserTableData(string selectStr)
        {
            TableData tableData = new TableData();
            try
            {
                UserDAL userDAL = new UserDAL();

                tableData = userDAL.QueryUserTableData(selectStr);
            }
            catch (Exception ex)
            {
                log.Error("查询用户数据 error: " + ex.Message);
                return tableData;
            }
            return tableData;
        }

        /// <summary>
        /// 新建用户数据
        /// </summary>
        /// <param name="tb_User_Entitys">用户实体列表</param>
        /// <returns>返回Entity对象</returns>
        public tb_user_Entity AddUserData(List<tb_user_Entity> tb_User_Entitys)
        {
            tb_user_Entity retEntity = new tb_user_Entity();

            try
            {
                UserDAL userDAL = new UserDAL();

                retEntity = userDAL.AddUserData(tb_User_Entitys);

            }
            catch (Exception ex)
            {
                log.Error("新建用户数据 error: " + ex.Message);
                return retEntity;
            }

            // 返回Entity对象
            return retEntity;
        }

        /// <summary>
        /// 根据用户 ID 获取用户数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns>包含用户数据的 TableData 对象</returns>
        public TableData GetUserById(int userId)
        {
            TableData tableData = new TableData();

            try
            {
                UserDAL userDAL = new UserDAL();
                tableData = userDAL.GetUserById(userId);
            }
            catch (Exception ex)
            {
                log.Error("根据用户 ID 获取用户数据 error: " + ex.Message);
            }

            return tableData;
        }

        /// <summary>
        /// 根据用户 ID 删除用户数据（假删除模式）
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns>包含操作状态和返回信息的 TableData 对象</returns>
        public TableData DeleteUserById(int userId)
        {
            TableData tableData = new TableData();
            try
            {
                UserDAL userDAL = new UserDAL();
                int affectedRows = userDAL.DeleteUserById(userId);
                if (affectedRows > 0)
                {
                    tableData.Status = "成功";
                    tableData.ReturnMsg = $"成功删除 {affectedRows} 条记录";
                }
                else
                {
                    tableData.Status = "失败";
                    tableData.ReturnMsg = "未找到对应的用户记录或删除操作未生效";
                }
            }
            catch (Exception ex)
            {
                log.Error("根据用户 ID 删除用户数据 error: " + ex.Message);
                tableData.Status = "失败";
                tableData.ReturnMsg = $"出现异常：{ex.Message}";
            }
            return tableData;
        }

        /// <summary>
        /// 修改用户数据
        /// </summary>
        /// <param name="userEntity">用户实体对象</param>
        /// <returns>包含操作状态和返回信息的 TableData 对象</returns>
        public TableData UpdateUserData(tb_user_Entity userEntity)
        {
            TableData tableData = new TableData();
            try
            {
                UserDAL userDAL = new UserDAL();
                int affectedRows = userDAL.UpdateUserData(userEntity);
                if (affectedRows > 0)
                {
                    tableData.Status = "成功";
                    tableData.ReturnMsg = $"成功修改 {affectedRows} 条记录";
                }
                else
                {
                    tableData.Status = "失败";
                    tableData.ReturnMsg = "未找到对应的用户记录或修改操作未生效";
                }
            }
            catch (Exception ex)
            {
                log.Error("修改用户数据 error: " + ex.Message);
                tableData.Status = "失败";
                tableData.ReturnMsg = $"出现异常：{ex.Message}";
            }
            return tableData;
        }

        /// <summary>
        /// 根据用户 ID 删除用户类型数据（真删除模式）
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns>包含操作状态和返回信息的 TableData 对象</returns>
        public TableData DeleteUserTypeByUserId(int userId)
        {
            TableData tableData = new TableData();
            try
            {
                UserDAL userDAL = new UserDAL();
                int affectedRows = userDAL.DeleteUserTypeByUserId(userId);
                if (affectedRows > 0)
                {
                    tableData.Status = "成功";
                    tableData.ReturnMsg = $"成功删除 {affectedRows} 条用户类型记录";
                }
                else
                {
                    tableData.Status = "失败";
                    tableData.ReturnMsg = "未找到对应的用户类型记录或删除操作未生效";
                }
            }
            catch (Exception ex)
            {
                log.Error("根据用户 ID 删除用户类型数据 error: " + ex.Message);
                tableData.Status = "失败";
                tableData.ReturnMsg = $"出现异常：{ex.Message}";
            }
            return tableData;
        }

        /// <summary>
        /// 根据用户 ID 获取用户类型数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns>包含用户类型数据的 TableData 对象</returns>
        public TableData QueryUserTypeDataByUserId(int userId)
        {
            TableData tableData = new TableData();
            try
            {
                UserDAL userDAL = new UserDAL();
                tableData = userDAL.QueryUserTypeDataByUserId(userId);
            }
            catch (Exception ex)
            {
                log.Error("根据用户 ID 获取用户类型数据 error: " + ex.Message);
            }
            return tableData;
        }

        /// <summary>
        /// 新建用户类型数据
        /// </summary>
        /// <param name="tb_user_type_Entity">用户类型实体列表</param>
        /// <returns>包含操作状态和返回信息的 TableData 对象</returns>
        public TableData AddUserTypeData(List<tb_user_type_Entity> tb_User_Type_Entitys)
        { 
            TableData tableData = new TableData();
            try
            {
                UserDAL userDAL = new UserDAL();
                int insertCount = userDAL.AddUserTypeData(tb_User_Type_Entitys);
                tableData.Status = "成功";
                tableData.ReturnMsg = insertCount.ToString();
            }
            catch (Exception ex)
            {
                log.Error("新建用户类型数据 error: " + ex.Message);
                tableData.Status = "失败";
                tableData.ReturnMsg = $"出现异常：{ex.Message}";
            }
            return tableData;
        }

        /// <summary>
        /// 修改用户类型数据
        /// </summary>
        /// <param name="userTypeEntity">用户类型实体对象</param>
        /// <returns>包含操作状态和返回信息的 TableData 对象</returns>
        public TableData UpdateUserTypeData(tb_user_type_Entity userTypeEntity)
        {
            TableData tableData = new TableData();
            try
            {
                UserDAL userDAL = new UserDAL();
                int affectedRows = userDAL.UpdateUserTypeData(userTypeEntity);
                if (affectedRows > 0)
                {
                    tableData.Status = "成功";
                    tableData.ReturnMsg = $"成功修改 {affectedRows} 条用户类型记录";
                }
                else
                {
                    tableData.Status = "失败";
                    tableData.ReturnMsg = "未找到对应的用户类型记录或修改操作未生效";
                }
            }
            catch (Exception ex)
            {
                log.Error("修改用户类型数据 error: " + ex.Message);
                tableData.Status = "失败";
                tableData.ReturnMsg = $"出现异常：{ex.Message}";
            }
            return tableData;
        }

        /// <summary>
        /// 根据用户名查询用户数据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public TableData QueryUserTableDataByUserName(string userName)
        {
            TableData tableData = new TableData();
            try
            {
                UserDAL userDAL = new UserDAL();
                tableData = userDAL.QueryUserTableDataByUserName(userName);
            }
            catch (Exception ex)
            {
                log.Error("根据用户名查询用户数据 error: " + ex.Message);
                return tableData;
            }
            return tableData;
        }

    }

}

