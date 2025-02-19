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

namespace DjDAL
{
    public class UserDAL
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserDAL));

        //用户登录,根据用户名和密码查询用户信息
        public string UserLogin(string username, string password)
        {


            string SqlStr = "select * from tb_user where username='" + username + "' and password='" + password + "'";

            var db = MysqlDBHelper.GetNewDb();

            var dt2 = db.Ado.GetDataTable(SqlStr);

            //返回json
            return JsonConvert.SerializeObject(dt2);
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
                string SqlStr = "";
                string SqlWhere = "";

                SqlStr = " SELECT ";
                SqlStr += " t1.Id,t1.userName,t1.email,t1.phone,t1.status,t1.last_login_time,t1.createUser,t1.createTime,t1.updateUser,t1.updateTime,GROUP_CONCAT(t2.roleType) AS roleTypes ";
                SqlStr += " FROM tb_user AS t1 ";
                SqlStr += " LEFT JOIN tb_user_type AS t2 ON t1.Id = t2.userId AND t2.`status`= '1' ";
                SqlStr += " WHERE 1=1 ";
                SqlStr += " AND t1.`status`='1' ";

                if (!string.IsNullOrEmpty(selectStr))
                {
                    SqlStr += " AND (t1.userName LIKE '%" + selectStr + "%' OR t1.email LIKE '%" + selectStr + "%' ) ";
                }

                SqlStr += " GROUP BY ";
                SqlStr += " t1.Id, t1.userName, t1.email, t1.phone, t1.status, t1.last_login_time, t1.createUser, t1.createTime, t1.updateUser, t1.updateTime ";
                SqlStr += " ORDER BY t1.userName DESC ";

                var db = MysqlDBHelper.GetNewDb();

                var dt2 = db.Ado.GetDataTable(SqlStr);

                //返回json
                tableData.Total = dt2.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt2);
            }
            catch (Exception ex)
            {
                log.Error("查询用户数据 error: ", ex);
                return tableData;
            }
            return tableData;
        }

        /// <summary>
        /// 新建用户数据
        /// </summary>
        /// <param name="tb_User_Entitys">用户实体列表</param>
        /// <returns>插入的记录数量</returns>
        public tb_user_Entity AddUserData(List<tb_user_Entity> tb_User_Entitys)
        {
            tb_user_Entity retEntity = new tb_user_Entity();

            try
            {
                var db = MysqlDBHelper.GetNewDb();

                retEntity = db.Insertable<tb_user_Entity>(tb_User_Entitys).ExecuteReturnEntity();
            }
            catch (Exception ex)
            {
                log.Error("新建用户数据 error: ", ex);
                return retEntity;
            }

            // 返回插入数量
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
                string SqlStr = " SELECT ";
                SqlStr += " t1.Id,t1.userName,t1.passWord,t1.email,t1.phone,t1.status,t1.last_login_time,t1.createUser,t1.createTime,t1.updateUser,t1.updateTime,GROUP_CONCAT(t2.roleType) AS roleTypes ";
                SqlStr += " FROM tb_user AS t1 ";
                SqlStr += " LEFT JOIN tb_user_type AS t2 ON t1.Id = t2.userId AND t2.`status`= '1' ";
                SqlStr += " WHERE 1=1 ";
                SqlStr += " AND t1.`status`='1' ";
                SqlStr += " AND t1.Id = "+ userId + " ";
                SqlStr += " GROUP BY ";
                SqlStr += " t1.Id, t1.userName,t1.passWord,t1.email, t1.phone, t1.status, t1.last_login_time, t1.createUser, t1.createTime, t1.updateUser, t1.updateTime ";
                SqlStr += " ORDER BY t1.createTime DESC ";


                var db = MysqlDBHelper.GetNewDb();

                var dt = db.Ado.GetDataTable(SqlStr);

                tableData.Total = dt.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                log.Error("根据用户 ID 获取用户数据 error: ", ex);
            }

            return tableData;
        }

        /// <summary>
        /// 根据用户 ID 删除用户数据（假删除模式）
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns>受影响的行数</returns>
        public int DeleteUserById(int userId)
        {
            int affectedRows = 0;
            try
            {
                var db = MysqlDBHelper.GetNewDb();
                affectedRows = db.Updateable<tb_user_Entity>()
                                 .SetColumns(u => u.status == 0)
                                 .Where(u => u.Id == userId)
                                 .ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("根据用户 ID 删除用户数据 error: ", ex);
            }
            return affectedRows;
        }

        /// <summary>
        /// 修改用户数据
        /// </summary>
        /// <param name="userEntity">用户实体对象</param>
        /// <returns>受影响的行数</returns>
        public int UpdateUserData(tb_user_Entity userEntity)
        {
            int affectedRows = 0;
            try
            {
                var db = MysqlDBHelper.GetNewDb();
                affectedRows = db.Updateable(userEntity)
                                 .IgnoreColumns(ignoreAllNullColumns: true)
                                 .ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("修改用户数据 error: ", ex);
            }
            return affectedRows;
        }

/*        /// <summary>
        /// 根据用户 ID 删除用户类型数据（假删除模式）
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns>受影响的行数</returns>
        public int DeleteUserTypeByUserId(int userId)
        {
            int affectedRows = 0;
            try
            {
                var db = MysqlDBHelper.GetNewDb();
                affectedRows = db.Updateable<tb_user_type_Entity>()
                                 .SetColumns(u => u.status == 0)
                                 .Where(u => u.userId == userId)
                                 .ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("根据用户 ID 删除用户类型数据 error: ", ex);
            }
            return affectedRows;
        }*/

        /// <summary>
        /// 根据用户Id删除用户类型数据(真删除)
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        public int DeleteUserTypeByUserId(int userId)
        {
            int affectedRows = 0;
            try
            {
                var db = MysqlDBHelper.GetNewDb();
                affectedRows = db.Deleteable<tb_user_type_Entity>()
                                 .Where(u => u.userId == userId)
                                 .ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("根据用户类型删除用户类型数据 error: ", ex);
            }
            return affectedRows;
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
                string sqlStr = "SELECT ";
                sqlStr += "t.Id, t.userId, t.roleType, t.status, t.createUser, t.createTime, t.updateUser, t.updateTime ";
                sqlStr += "FROM tb_user_type t ";
                sqlStr += "WHERE t.userId = @userId AND t.status = 1 ";
                sqlStr += "ORDER BY t.createTime DESC";

                var db = MysqlDBHelper.GetNewDb();
                var param = new MySqlParameter("@userId", userId);

                var dt = db.Ado.GetDataTable(sqlStr, param);

                tableData.Total = dt.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                log.Error("根据用户 ID 获取用户类型数据 error: ", ex);
            }

            return tableData;
        }

        /// <summary>
        /// 新建用户类型数据
        /// </summary>
        /// <param name="tb_user_type_Entity">用户类型实体列表</param>
        /// <returns>插入的记录数量</returns>
        public int AddUserTypeData(List<tb_user_type_Entity> tb_User_Type_Entitys)
        {
            int insertCount = 0;
            try
            {
                var db = MysqlDBHelper.GetNewDb();
                insertCount = db.Insertable<tb_user_type_Entity>(tb_User_Type_Entitys).ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("新建用户类型数据 error: ", ex);
                return insertCount;
            }
            // 返回插入数量
            return insertCount;
        }

        /// <summary>
        /// 修改用户类型数据
        /// </summary>
        /// <param name="userTypeEntity">用户类型实体对象</param>
        /// <returns>受影响的行数</returns>
        public int UpdateUserTypeData(tb_user_type_Entity userTypeEntity)
        {
            int affectedRows = 0;
            try
            {
                var db = MysqlDBHelper.GetNewDb();
                affectedRows = db.Updateable(userTypeEntity)
                                 .IgnoreColumns(ignoreAllNullColumns: true)
                                 .ExecuteCommand();
            }
            catch (Exception ex)
            {
                log.Error("修改用户类型数据 error: ", ex);
            }
            return affectedRows;
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
                string SqlStr = "";
                string SqlWhere = "";
                SqlStr = " SELECT ";
                SqlStr += " t1.Id,t1.userName,t1.email,t1.phone,t1.status,t1.last_login_time,t1.createUser,t1.createTime,t1.updateUser,t1.updateTime,GROUP_CONCAT(t2.roleType) AS roleTypes ";
                SqlStr += " FROM tb_user AS t1 ";
                SqlStr += " LEFT JOIN tb_user_type AS t2 ON t1.Id = t2.userId AND t2.`status`= '1' ";
                SqlStr += " WHERE 1=1 ";
                SqlStr += " AND t1.`status`='1' ";
                if (!string.IsNullOrEmpty(userName))
                {
                    SqlStr += " AND t1.userName = '" + userName + "' ";
                }
                SqlStr += " GROUP BY ";
                SqlStr += " t1.Id, t1.userName, t1.email, t1.phone, t1.status, t1.last_login_time, t1.createUser, t1.createTime, t1.updateUser, t1.updateTime ";
                SqlStr += " ORDER BY t1.userName DESC ";
                var db = MysqlDBHelper.GetNewDb();
                var dt2 = db.Ado.GetDataTable(SqlStr);
                //返回json
                tableData.Total = dt2.Rows.Count;
                tableData.Rows = JsonConvert.SerializeObject(dt2);
            }
            catch (Exception ex)
            {
                log.Error("查询用户数据 error: ", ex);
                return tableData;
            }
            return tableData;
        }

    }
}
