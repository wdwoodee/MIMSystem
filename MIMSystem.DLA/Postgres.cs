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

        #region common method

        /// <summary>
        /// 获取总金额前10的消费者
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTop10InteCustomer()
        {
            string sqlGetTop10Cus = "select id as 用户排名, mobile as 电话, contimes as 总消费次数, totalconamount as 总消费金额, totalintegral as 总积分 from customer order by totalintegral desc limit 10;";
            DataTable top10Cus = new DataTable();

            #region
            //try
            //{
            //    using (NpgsqlConnection conn = new NpgsqlConnection(connectstr))
            //    {
            //        //conn.Open();
            //        using (NpgsqlCommand cmd = conn.CreateCommand())
            //        {
            //            cmd.CommandText = sqlGetTop10Cus;
            //            cmd.CommandType = CommandType.Text;
            //            using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
            //            {
            //                da.Fill(top10Cus);
            //                cmd.Parameters.Clear();
            //                return top10Cus;
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    return top10Cus;
            //}
            #endregion

            top10Cus = ExecuteSQL(sqlGetTop10Cus);
            return top10Cus;
        }

        public static DataTable GetInteCustomerByMobile(string mobile)
        {
            string sqlGetTop10Cus = string.Format("select id as 用户排名, mobile as 电话, contimes as 总消费次数, totalconamount as 总消费金额, totalintegral as 总积分 from customer where mobile ='{0}' order by totalintegral desc limit 10;", mobile);
            DataTable top10Cus = new DataTable();
            top10Cus = ExecuteSQL(sqlGetTop10Cus);
            return top10Cus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static DataTable GetConDetail(string mobile)
        {
            string sqlGetTop10Cus = string.Format(@"select id as 消费次数排名, mobile as 电话, contype as 总消费类型, 
conamount as 消费金额, integralchange as 积分变更, contime as 消费日期 from integral where mobile='{0}' order by contime desc;",mobile);
            DataTable detial = new DataTable();

            #region
            //try
            //{
            //    using (NpgsqlConnection conn = new NpgsqlConnection(connectstr))
            //    {
            //        //conn.Open();
            //        using (NpgsqlCommand cmd = conn.CreateCommand())
            //        {
            //            cmd.CommandText = sqlGetTop10Cus;
            //            cmd.CommandType = CommandType.Text;
            //            using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
            //            {
            //                da.Fill(detial);
            //                cmd.Parameters.Clear();
            //                return detial;
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    return detial;
            //}
            #endregion

            detial = ExecuteSQL(sqlGetTop10Cus);
            return detial;
        }

        /// <summary>
        /// 消费金额前10的客户
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTop10ConsAmountCustomer()
        {
            string sqlGetTop10Cus = "select mobile as 电话, contimes as 总消费次数, totalconamount as 总消费金额, totalintegral as 总积分 from customer order by totalconamount desc limit 10;";
            DataTable top10Cus = new DataTable();

            top10Cus = ExecuteSQL(sqlGetTop10Cus);
            return top10Cus;
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


        public static void RecalculateCustomer(string mobile)
        {
            int conTimes = 0;
            //查询integrel表中消费的次数
            string sqlQueryIntegrel = "select * from integral where mobile='" + mobile + "'";
            DataTable dtIntegrel = new DataTable();
            dtIntegrel = Postgres.ExecuteSQL(sqlQueryIntegrel);
            
            conTimes = dtIntegrel.Rows.Count;
            if (conTimes == 0)
            {
                //已经全部删除消费记录，需要删除customer记录
                string slqDeleteCustomer = "delete from customer where mobile='"+ mobile + "'";
                ExecuteNonQuery(slqDeleteCustomer);
            }
            else
            {
                //还有消费记录，则需要重新计算customer表的值，消费总次数，消费总金额，总积分的更新
                int conAmount2 = 0;
                foreach (DataRow row in dtIntegrel.Rows)
                {
                    conAmount2 += Convert.ToInt32(row["conamount"]);
                }
                int conIntegrel2 = 0;
                foreach (DataRow row in dtIntegrel.Rows)
                {
                    conIntegrel2 += Convert.ToInt32(row["integralchange"]);
                }

                //更新Customer表
                Postgres.UpdateCustomer(mobile, conTimes, conAmount2, conIntegrel2);
            }
            
        }
        

        #endregion
    }
}
