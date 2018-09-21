using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CompanyManager.DataManager
{

    class MySqlConn
    {
        static String connetStr = "server=127.0.0.1;port=3306;user=hadoop;password=hadoop; database=huichun;SslMode = none;";

        static protected MySqlConnection conn;// = "server=127.0.0.1;port=3306;user=hadoop;password=hadoop; database=huichun;SslMode = none;";


        #region 修改接口
        public static int DoCommand(string sql, string[] para)
        {
            int result = -1;
            conn = new MySqlConnection(ConnetStr);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                for (int i = 0; i < para.Length; i++)
                {

                    cmd.Parameters.AddWithValue("para" + (i + 1).ToString(), para[i]);
                }
                result = cmd.ExecuteNonQuery();//3.执行插入、删除、更改语句。执行成功返回受影响的数据的行数，返回1可做true判断。执行失败不返回任何数据，报错，下面代码都不执行
            }
            catch (Exception ex)
            {
                result = -1;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public static int DoCommand(string sql)
        {
            int result = -1;
            conn = new MySqlConnection(ConnetStr);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
               
                result = cmd.ExecuteNonQuery();//3.执行插入、删除、更改语句。执行成功返回受影响的数据的行数，返回1可做true判断。执行失败不返回任何数据，报错，下面代码都不执行
            }
            catch (Exception ex)
            {
                result = -1;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        #endregion

        
        public static System.Data.DataSet GetDataSet(string sql)
        {
            //DataSet ds = new DataSet();
            //string conn = sqlconn;
            //SqlConnection con = new SqlConnection(conn);
            //SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.CommandTimeout = 180;
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //adp.Fill(ds);
            System.Data.DataSet ds = new System.Data.DataSet();
            conn = new MySqlConnection(ConnetStr);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 180;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(ds);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public static System.Data.DataSet GetDataSet(string sql,string[] para)
        {
            //DataSet ds = new DataSet();
            //string conn = sqlconn;
            //SqlConnection con = new SqlConnection(conn);
            //SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.CommandTimeout = 180;
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //adp.Fill(ds);
            System.Data.DataSet ds = new System.Data.DataSet();
            conn = new MySqlConnection(ConnetStr);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 180;
                for (int i = 0; i < para.Length; i++)
                {

                    cmd.Parameters.AddWithValue("para" + (i + 1).ToString(), para[i]);
                }
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(ds);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public static string ConnetStr
        {
            get
            {
                return connetStr;
            }

            set
            {
                connetStr = string.Format("server={0};port=3306;user=hadoop;password=hadoop; database=huichun;SslMode = none;", value);
            }
        }

        public MySqlConn()
        {
            // server=127.0.0.1/localhost 代表本机，端口号port默认是3306可以不写
           // MySqlConnection 
            //    conn = new MySqlConnection(connetStr);
            //try
            //{
            //    conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
            //    Console.WriteLine("已经建立连接");
            //    //在这里使用代码对数据库进行增删查改
            //}
            //catch (MySqlException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //}
        }

        protected System.Data.DataTable Select(string sql)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //string sql = "select * from user";
            conn = new MySqlConnection(ConnetStr);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
                bool col = true;

                while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
                {
                    if (col)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dt.Columns.Add(reader.GetName(i).Trim());
                        }
                        col = false;
                    }

                    System.Data.DataRow dr = dt.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dr[reader.GetName(i).Trim()] = reader[i].ToString();
                    }
                    dt.Rows.Add(dr);
                    //Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());
                    //Console.WriteLine(reader.GetInt32(0)+reader.GetString(1)+reader.GetString(2));
                    //Console.WriteLine(reader.GetInt32("userid") + reader.GetString("username") + reader.GetString("password"));//"userid"是数据库对应的列名，推荐这种方式
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        protected int ExecuteNonQuery(string sqlStr,MySqlParameter[] para)
        {
            int result = -1;
            //string sql = "insert into user(username,password,registerdate) values('啊宽','123','" + DateTime.Now + "')";
            //string sql = "delete from user where userid='9'";
            //string sql = "update user set username='啊哈',password='123' where userid='8'";
            conn = new MySqlConnection(ConnetStr);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                cmd.Parameters.AddRange(para);
                result = cmd.ExecuteNonQuery();//3.执行插入、删除、更改语句。执行成功返回受影响的数据的行数，返回1可做true判断。执行失败不返回任何数据，报错，下面代码都不执行
            }
            catch(Exception ex)
            {
                result = -1;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        protected int ExecuteNonQuery(string sqlStr,string[] para)
        {
            int result = -1;
            //string sql = "insert into user(username,password,registerdate) values('啊宽','123','" + DateTime.Now + "')";
            //string sql = "delete from user where userid='9'";
            //string sql = "update user set username='啊哈',password='123' where userid='8'";
            conn = new MySqlConnection(ConnetStr);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                for (int i = 0; i < para.Length; i++)
                {

                    cmd.Parameters.AddWithValue("para" + (i + 1).ToString(), para[i]);
                }
                result = cmd.ExecuteNonQuery();//3.执行插入、删除、更改语句。执行成功返回受影响的数据的行数，返回1可做true判断。执行失败不返回任何数据，报错，下面代码都不执行
            }
            catch(Exception ex)
            {
                result = -1;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// sql变量需要以para + 序列命名,如:para1,para2,以1开始
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        protected System.Data.DataTable Select(string sql,string[] para)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //string sql = "select * from user";
            conn = new MySqlConnection(ConnetStr);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                for (int i = 0; i < para.Length; i++)
                {

                    cmd.Parameters.AddWithValue("para" + (i + 1).ToString(), para[i]);
                }

                MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
                bool col = true;

                while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
                {
                    if (col)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dt.Columns.Add(reader.GetName(i).Trim());
                        }
                        col = false;
                    }

                    System.Data.DataRow dr = dt.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dr[reader.GetName(i).Trim()] = reader[i].ToString();
                    }
                    dt.Rows.Add(dr);
                    //Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());
                    //Console.WriteLine(reader.GetInt32(0)+reader.GetString(1)+reader.GetString(2));
                    //Console.WriteLine(reader.GetInt32("userid") + reader.GetString("username") + reader.GetString("password"));//"userid"是数据库对应的列名，推荐这种方式
                }
            }
            catch
            {
                dt = null;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #region 一些备注方法,供参考
        /// <summary>
        /// b.查询条件不固定
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool chaxun(string username,string password)
        {
            //string sql = "select * from user where username='"+username+"' and password='"+password+"'"; //我们自己按照查询条件去组拼
            string sql = "select * from user where username=@para1 and password=@para2";//在sql语句中定义parameter，然后再给parameter赋值
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            
            cmd.Parameters.AddWithValue("para1", username);
            cmd.Parameters.AddWithValue("para2", password);

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())//如果用户名和密码正确则能查询到一条语句，即读取下一行返回true
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// c.需要查询返回一个值
        /// </summary>
        /// <returns></returns>
        private int returnvalue()
        {
            int count = -1;
            string sql = "select count(*) from user";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            Object result = cmd.ExecuteScalar();//执行查询，并返回查询结果集中第一行的第一列。所有其他的列和行将被忽略。select语句无记录返回时，ExecuteScalar()返回NULL值
            if (result != null)
            {
                count = int.Parse(result.ToString());
            }
            return count;
        }


        private int insertdeleteupdate()
        {
            int result = -1;
            string sql = "insert into user(username,password,registerdate) values('啊宽','123','" + DateTime.Now + "')";
            //string sql = "delete from user where userid='9'";
            //string sql = "update user set username='啊哈',password='123' where userid='8'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                result = cmd.ExecuteNonQuery();//3.执行插入、删除、更改语句。执行成功返回受影响的数据的行数，返回1可做true判断。执行失败不返回任何数据，报错，下面代码都不执行
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        /// <summary>
        /// 事务(MySqlTransaction类)
        /// </summary>
        private void shiwu()
        {
            String connetStr = "server=127.0.0.1;user=root;password=root;database=minecraftdb;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            conn.Open();//必须打开通道之后才能开始事务
            MySqlTransaction transaction = conn.BeginTransaction();//事务必须在try外面赋值不然catch里的transaction会报错:未赋值
            Console.WriteLine("已经建立连接");
            try
            {
                string date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                string sql1 = "insert into user(username,password,registerdate) values('啊宽','123','" + date + "')";
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                cmd1.ExecuteNonQuery();

                string sql2 = "insert into user(username,password,registerdate) values('啊宽','123','" + date + "')";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();//事务ExecuteNonQuery()执行失败报错，username被设置unique
                conn.Close();
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    transaction.Commit();//事务要么回滚要么提交，即Rollback()与Commit()只能执行一个
                    conn.Close();
                }
            }
        }
        #endregion

    }
}

    