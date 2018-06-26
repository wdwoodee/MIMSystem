using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Npgsql;


namespace MIMSystem.DLA
{
    public class Postgres
    {
        static string connectstr = ConfigurationManager.ConnectionStrings["connPostgres"].ConnectionString;

        # region 数据库的增删改方法
        ///
        /// 测试连接PostGreSQL数据库
        ///
        /// Success/Failure
        public static string TestConnection()
        {
            string dataDir = AppDomain.CurrentDomain.BaseDirectory;
            if (dataDir.EndsWith(@"\bin\Debug\")
                || dataDir.EndsWith(@"\bin\Release\"))
            {
                dataDir = System.IO.Directory.GetParent(dataDir).Parent.Parent.FullName;
                AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);
            }

            string str = connectstr;
            string strMessage = string.Empty;
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(str);
                conn.Open();
                strMessage = "Success";
                conn.Close();
            }
            catch
            {
                strMessage = "Failure";
            }
            return strMessage;
        }


        /// <summary>
        /// 读取数据表，返回数据集。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable ExecuteSQL(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectstr))
                {
                    //conn.Open();
                    using (NpgsqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                            cmd.Parameters.Clear();
                            return dt;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return dt;
            }
        }

        public static int ExecuteNonQuery(string sql)
        {
            int rowAffected = 0;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectstr))
                {
                    using (NpgsqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        conn.Open();
                        rowAffected = cmd.ExecuteNonQuery();
                        conn.Close();
                        return rowAffected;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return rowAffected;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connectstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteReader();
                }
            }
        }

        public static int ExecuteScaler(string sql)
        {
            int count = 0;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectstr))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        //cmd.CommandType = CommandType.Text;
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
            return count;
        }
        
        


        /// <summary>
        /// 根据参数，插入数据到
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="conTimes"></param>
        /// <param name="conTotalAmount"></param>
        /// <param name="conIntegral"></param>
        /// <returns></returns>
        public static int InsertCustomer(string mobile, int conTimes, int conTotalAmount, int conIntegral)
        {
            int affectCount = 0;
            if (!String.IsNullOrEmpty(mobile))
            {
                string sqlInsertCustomer = string.Format("insert into customer(mobile,contimes,totalconamount,totalintegral) values({0},{1},{2},{3})", mobile, conTimes, conTotalAmount, conIntegral);
                affectCount = ExecuteNonQuery(sqlInsertCustomer);
            }
            else
            {
                return affectCount;
            }
            
            return affectCount;
        }

        public static int UpdateCustomer(string mobile, int conTimes, int conTotalAmount, int conIntegral)
        {
            int affectCount = 0;
            if (!String.IsNullOrEmpty(mobile))
            {
                string sqlInsertCustomer = string.Format("update customer set mobile={0},contimes={1},totalconamount={2},totalintegral={3} where mobile='{4}'", mobile, conTimes, conTotalAmount, conIntegral, mobile);
                affectCount = ExecuteNonQuery(sqlInsertCustomer);
            }
            else
            {
                return affectCount;
            }

            return affectCount;
        }

        /// <summary>
        /// 根据参数更新Integrel表
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="conType"></param>
        /// <param name="conAmount"></param>
        /// <param name="conIntegral"></param>
        /// <returns></returns>
        public static int InsertIntegrel(string mobile, string conType, int conAmount, int conIntegral)
        {
            int affectCount = 0;
            //string date = DateTime.Now.Date.ToString();
            if (!String.IsNullOrEmpty(mobile) && !String.IsNullOrEmpty(mobile))
            {
                string sqlInsertIntegrel = string.Format("insert into integral(mobile,contype,conamount,integralchange,contime) values({0},'{1}',{2},{3},now())", mobile, conType, conAmount, conIntegral);
                affectCount = ExecuteNonQuery(sqlInsertIntegrel);
            }
            else
            {
                return affectCount;
            }

            return affectCount;
        }


        #region common method
        //public static DataTable 

        #endregion
    }
}
