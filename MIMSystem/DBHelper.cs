using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace MIMSystem
{
    class DBHelper
    {
        static string connectstr = ConfigurationManager.ConnectionStrings["connectString"].ConnectionString;
        //static string connectstr = "MembershipSystem.db";

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
                SQLiteConnection conn = new SQLiteConnection(str);
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
                using (SQLiteConnection conn = new SQLiteConnection(connectstr))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
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
                using (SQLiteConnection conn = new SQLiteConnection(connectstr))
                {
                    //conn.Open();
                    using (SQLiteCommand cmd = conn.CreateCommand())
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

        #region common method
        //public static DataTable 

        #endregion
    }
}
