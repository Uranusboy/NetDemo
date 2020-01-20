using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace DataServices
{
    namespace DataServices
    {
        /// <summary>
        /// MySql数据库操作数据库
        /// </summary>        
        public class MySQLDataBasehelper
        {

            // 连接字符串
            private static string ConnString = ConfigurationManager.AppSettings["MySQLDBConnectString"];
            MySqlConnection conn;

            public MySQLDataBasehelper()
            {

            }
            /// <summary>
            /// 打开数据库连接
            /// </summary>
            public MySqlConnection getOpenConnect()
            {
                conn = new MySqlConnection(ConnString);
                conn.Open();
                return conn;
            }

            /// <summary>
            /// 返回查询的数据(无参数)
            /// </summary>
            /// <param name="Sql">Sql查询命令</param>
            /// <returns>以数阅读器的形式返回</returns>
            public MySqlDataReader getSelectData(string Sql)
            {
                MySqlDataReader dr = null;
                try
                {
                    MySqlCommand cmd = getOpenConnect().CreateCommand();
                    cmd.CommandText = Sql;
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); //CommandBehavior.CloseConnection
                                                                             //cmd.Connection.Close();
                                                                             //cmd.Connection.Dispose();
                                                                             //cmd.Dispose();
                    return dr;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    // conn.Close();                
                }
            }

            /// <summary>
            /// 返回查询的数据(带参数)
            /// </summary>
            /// <param name="Sql">Sql查询命令</param>
            /// <returns>以数阅读器的形式返回</returns>
            public MySqlDataReader getSelectData(string Sql, MySqlParameter[] paras)
            {
                MySqlDataReader dr = null;
                try
                {
                    MySqlCommand cmd = getOpenConnect().CreateCommand();
                    cmd.CommandText = Sql;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddRange(paras);
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    //cmd.Connection.Close();
                    //cmd.Connection.Dispose();
                    //cmd.Dispose();
                    return dr;

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    //conn.Close();                
                }
            }
            /// <summary>
            /// 返回查询的数据(带参数)--以DataTable形式返回
            /// </summary>
            /// <param name="Sql">Sql查询命令</param>
            /// <returns>以DataTable形式返回</returns>
            public DataTable getSelectData_DT(string Sql, MySqlParameter[] paras)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(Sql, getOpenConnect()))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddRange(paras);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                            cmd.Dispose();
                            return ds.Tables[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    //CloseConnect();
                }
            }
            /// <summary>
            /// 返回查询的数据(不带参数)--以DataTable形式返回
            /// </summary>
            /// <param name="Sql">Sql查询命令</param>
            /// <returns>以DataTable形式返回</returns>
            public DataTable getSelectData_DT(string Sql)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(Sql, getOpenConnect()))
                    {
                        cmd.CommandTimeout = 0;
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                            cmd.Dispose();
                            return ds.Tables[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    //CloseConnect();
                }
            }
            /// <summary>
            /// 测试数据是否存在
            /// </summary>
            /// <param name="Sql">SQL命令</param>
            /// <param name="paras">参数</param>
            /// <returns></returns>
            public bool testIsExist(string Sql, MySqlParameter[] paras)
            {
                bool test = false;
                MySqlDataReader dr = null;
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(Sql, getOpenConnect()))
                    {
                        cmd.Parameters.AddRange(paras);
                        dr = cmd.ExecuteReader();
                        if (dr.Read() && dr.HasRows)
                        {
                            test = true;
                        }
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                        cmd.Dispose();
                        return test;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dr.Close();
                    //CloseConnect();
                }
            }
            /// <summary>
            /// 是否已经存在
            /// </summary>
            /// <param name="Sql">SQL命令</param>
            /// <returns></returns>
            public bool testIsExist(string Sql)
            {
                bool testResult = false;
                MySqlDataReader dr = null;
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(Sql, getOpenConnect()))
                    {
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            testResult = true;
                        }
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                        cmd.Dispose();
                        return testResult;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dr.Close();
                    //CloseConnect();
                }
            }

            /// <summary>
            /// 完成对数据库中数据表的添加删除修改的操作（参数）
            /// </summary>
            /// <param name="Sql">要执行的Sql命令</param>
            /// <param name="paras">Sql命令要使用到的参数列表</param>
            /// <returns>返回值，失败：-1，>0：成功</returns>
            public int insert_or_update_or_delete(string Sql, MySqlParameter[] paras)
            {
                int res = -1;
                try
                {

                    MySqlCommand cmd = new MySqlCommand(Sql, getOpenConnect());
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(paras);
                    res = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    cmd.Dispose();
                    return res;
                }
                catch (Exception ex)
                {
                    return res;
                }
                finally
                {
                    //CloseConnect();
                }
            }
            /// <summary>
            /// 完成对数据库中数据表的添加删除修改的操作
            /// </summary>
            /// <param name="Sql">要执行的Sql命令</param>       
            /// <returns>返回值，失败：-1，>0：成功</returns>
            public int insert_or_update_or_delete(string Sql)
            {
                int res = -1;
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(Sql, getOpenConnect()))
                    {
                        cmd.Parameters.Clear();
                        res = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                        cmd.Dispose();
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    return res;
                }
                finally
                {
                    //CloseConnect();
                }
            }
            /// <summary>
            /// 删除所有
            /// </summary>
            /// <param name="Sql"></param>
            /// <returns></returns>
            public int deleteall(string Sql)
            {
                int res = -1;
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(Sql, getOpenConnect()))
                    {
                        res = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                        cmd.Dispose();
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    return res;
                }
                finally
                {
                    //CloseConnect();
                }
            }

            /// <summary>
            /// 执行带事务的处理
            /// </summary>
            /// <param name="Sql"></param>
            /// <param name="Sqlparas"></param>
            /// <returns></returns>
            public bool SqlTransaction(List<string> Sql, List<MySqlParameter[]> Sqlparas)
            {
                // 打开与数据库之间的连接
                MySqlCommand myComm = new MySqlCommand();
                MySqlTransaction myTran;
                MySqlConnection conn = getOpenConnect();
                //创建一个事务
                myTran = conn.BeginTransaction();
                try
                {
                    for (int i = 0; i < Sql.Count; i++)
                    {
                        myComm.Parameters.Clear();
                        //下面绑定连接和事务对象
                        myComm.Connection = conn;
                        myComm.Transaction = myTran;
                        myComm.CommandText = Sql[i];
                        myComm.Parameters.AddRange(Sqlparas[i]);
                        myComm.ExecuteNonQuery();
                    }
                    myTran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    myTran.Rollback();
                    return false;
                    throw (ex);
                }
                finally
                {
                    //关闭与数据库的连接
                    conn.Close();
                }
            }

            /// <summary>
            /// 执行带事务的处理(不带参数)
            /// </summary>
            /// <param name="Sql"></param>
            /// <param name="Sqlparas"></param>
            /// <returns></returns>
            public bool SqlTransaction(List<string> Sql)
            {
                // 打开与数据库之间的连接

                MySqlCommand myComm = new MySqlCommand();
                MySqlTransaction myTran;
                MySqlConnection conn = getOpenConnect();
                //创建一个事务
                myTran = conn.BeginTransaction();
                try
                {
                    for (int i = 0; i < Sql.Count; i++)
                    {
                        myComm.Connection = conn;
                        myComm.Transaction = myTran;
                        myComm.CommandText = Sql[i];
                        myComm.ExecuteNonQuery();
                    }
                    myTran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    myTran.Rollback();
                    return false;
                    throw (ex);
                }
                finally
                {
                    //关闭与数据库的连接
                    conn.Close();
                }
            }

            /// <summary>
            /// 分页（新开发）
            /// </summary>
            /// <param name="sql">SQL命令</param>
            /// <param name="pageIndex">页数（第几页）</param>
            /// <param name="pageSize">页大小（每页显示多少行数据）</param>
            /// <param name="rowCount">总行数</param>        
            /// <returns></returns>
            public MySqlDataReader Paging(string sql, int pageIndex, int pageSize, out int rowCount)
            {
                MySqlDataReader dr;
                try
                {
                    if (string.IsNullOrEmpty(sql)) { throw new ArgumentNullException("sql"); }
                    if (pageIndex < 1) { throw new ArgumentOutOfRangeException("pageIndex"); }
                    if (pageSize < 1) { throw new ArgumentOutOfRangeException("pageSize"); }
                    int start = (pageIndex - 1) * pageSize;
                    int end = start + pageSize;
                    // 统计总数
                    rowCount = Counting(sql);
                    // 分页
                    // SELECT * FROM xtxx LIMIT 开始位置，页大小
                    string paging = string.Format(sql + " LIMIT {0},{1}", start, pageSize);

                    MySqlCommand cmd = getOpenConnect().CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = paging;
                    cmd.CommandTimeout = 0;
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return dr;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            // 统计返回的总数
            public int Counting(string sql)
            {
                int num = 0;
                try
                {
                    if (string.IsNullOrEmpty(sql)) { throw new ArgumentNullException("sql"); }
                    string m_sql = sql.ToUpper();

                    int index1 = m_sql.IndexOf("SELECT ");
                    int index2 = m_sql.IndexOf(" FROM");

                    if (index1 >= 0 && index2 >= 0)
                    {
                        m_sql = m_sql.Replace(m_sql.Substring(index1 + 6, index2 - index1 - 6), " 1  as  OneNumber "); // MSSQL 不能直接写数字，还得取个列的别名
                    }

                    if (m_sql.LastIndexOf("ORDER BY") > 1)
                    {
                        m_sql = m_sql.Remove(m_sql.LastIndexOf("ORDER BY"));
                    }
                    string sqlstring = string.Format("select count(1) from ({0}) as t", m_sql);  // MSSQL 要用as 表的形式加表名
                    using (MySqlCommand cmd = new MySqlCommand(sqlstring, getOpenConnect()))
                    {
                        cmd.CommandTimeout = 0;
                        num = Convert.ToInt32(cmd.ExecuteScalar());
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                        cmd.Dispose();
                        return num;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
