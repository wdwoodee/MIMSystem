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
                    conn.Open();
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
                    conn.Open();
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
        #region common method
        //public static DataTable 

        #endregion
    }
}
