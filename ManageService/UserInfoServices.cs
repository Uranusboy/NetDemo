using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServices.DataServices;
using MySql.Data.MySqlClient;

namespace ManageService
{
    /// <summary>
    /// 系统用户管理
    /// </summary>
    public class SystemUserService
    {
        // 数据库访问对象
        MySQLDataBasehelper db = new MySQLDataBasehelper();

        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="v">查询条件</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回记录数</param>
        /// <returns></returns>
        public List<Model.v_UserInfos> querying(Condition.SystemuserCondition v, int pageIndex, int pageSize, out int recordCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("Select UserID,UserAccount,UserPassword,UserName,UserType  From UserInfos ");
            sql.Append("  Where 1=1  ");

            if (!string.IsNullOrEmpty(v.UserName))
            {
                sql.Append(" and UserName Like '%" + v.UserName + "%'");
            }

            using (MySqlDataReader reader = db.Paging(sql.ToString(), pageIndex, pageSize, out recordCount))
            {
                List<Model.v_UserInfos> result = new List<Model.v_UserInfos>();
                while (reader.Read())
                {
                    result.Add(new Model.v_UserInfos()
                    {
                        UserID = reader.GetDecimal(0),
                        UserAccount = reader.GetString(1),
                        UserPassword = reader.GetString(2),
                        UserName = reader.GetString(3),
                        UserType = reader.GetString(4)
                    });
                }
                reader.Close();
                return result;
            }
        }

        /// <summary>
        /// 获取某一个用户的信息
        /// </summary>
        /// <param name="vAIS_Users"></param>
        /// <returns></returns>
        public Model.v_UserInfos loadone(decimal UserID)
        {
            Model.v_UserInfos users = null;

            StringBuilder sql = new StringBuilder();
            sql.Append("select UserID,UserAccount,UserName,UserPassword From UserInfos ");
            sql.Append("  where UserID=@UserID");
            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@UserID",UserID)
            };
            using (MySqlDataReader reader = db.getSelectData(sql.ToString(), paras))
            {
                if (reader.Read())
                {
                    users = new Model.v_UserInfos();
                    users.UserID = reader.GetDecimal(0);
                    users.UserAccount = reader.GetString(1);
                    users.UserName = reader.GetString(2);
                    users.UserPassword = reader.GetString(3);
                }
                reader.Close();

                return users;
            }
        }
        /// <summary>
        /// 测试用户的合法性
        /// </summary>
        /// <param name="vAIS_Users"></param>
        /// <returns></returns>
        public Model.v_UserInfos TestUser(Model.v_UserInfos vUser)
        {
            Model.v_UserInfos users = null;

            StringBuilder sql = new StringBuilder();

            sql.Append("select UserID,UserAccount,UserName  From UserInfos  ");
            sql.Append(" where UserAccount=@UserAccount and UserPassword=@UserPassword  ");
            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@UserAccount",vUser.UserAccount),
                new MySqlParameter("@UserPassword",vUser.UserPassword)
                };
            using (MySqlDataReader reader = db.getSelectData(sql.ToString(), paras))
            {
                if (reader.Read())
                {
                    users = new Model.v_UserInfos();
                    users.UserID = reader.GetDecimal(0);
                    users.UserAccount = reader.GetString(1);
                    users.UserName = reader.GetString(2);
                }
                reader.Close();
                return users;
            }
        }
        public bool add(Model.v_UserInfos vUser)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("Insert into UserInfos  (UserID,UserAccount,UserPassword,UserName,UserType)  ");
            sql.Append(" Values (@UserID,@UserAccount,@UserPassword,@UserName,@UserType)");
            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@UserID",vUser.UserID),
                new MySqlParameter("@UserAccount",vUser.UserAccount),
                new MySqlParameter("@UserPassword",vUser.UserPassword),
                new MySqlParameter("@UserName",vUser.UserName),
                new MySqlParameter("@UserType",vUser.UserType)
            };

            return db.insert_or_update_or_delete(sql.ToString(), paras) > 0 ? true : false;
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="vUser"></param>
        /// <returns></returns>
        public bool edit(Model.v_UserInfos vUser)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("Update UserInfos  Set  UserPassword=@UserPassword,UserName=@UserName ,UserType=@UserType ");
            sql.Append("  where UserID=@UserID");
            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@UserID",vUser.UserID),
                new MySqlParameter("@UserPassword",vUser.UserPassword),
                new MySqlParameter("@UserName",vUser.UserName),
                new MySqlParameter("@UserType",vUser.UserType)
            };

            return db.insert_or_update_or_delete(sql.ToString(), paras) > 0 ? true : false;
        }

        // 删除
        public bool del(Model.v_UserInfos vUser)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("Delete From  UserInfos    where UserID=@UserID");
            MySqlParameter[] paras = new MySqlParameter[]{
                new MySqlParameter("@UserID",vUser.UserID)
            };

            return db.insert_or_update_or_delete(sql.ToString(), paras) > 0 ? true : false;
        }

        /// <summary>
        /// 测试账号是否已经存在
        /// </summary>
        /// <param name="useraccount"></param>
        /// <returns></returns>
        public Model.v_UserInfos TestAccountIsExist(string useraccount)
        {
            Model.v_UserInfos users = null;
            string sql = string.Format("select UserAccount from UserInfos where UserAccount like '{0}%' order by UserAccount Desc", useraccount);
            using (MySqlDataReader reader = db.getSelectData(sql))
            {
                if (reader.Read())
                {
                    users = new Model.v_UserInfos();
                    users.UserAccount = reader.GetString(0);
                }
                reader.Close();
                return users;
            }
        }


    }
}
