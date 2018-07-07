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
        public static DataTable GetTop10Summary()
        {
            string sqlGetTop10Cus = @"select s.mid as 会员排名, m.username as 会员, m.mobile as 电话, s.contimes as 总消费次数, s.totalconamount as 总消费金额,
s.totalintegral as 总积分 from consumptionsummary as s,members as m where s.mid=m.id order by totalintegral desc limit 10;";
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


        /// <summary>
        /// search summary
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataTable GetSummaryBySearchText(string searchText)
        {
            string sqlGetTop10Cus = string.Format(@"select s.mid as 会员排名, m.username as 会员, m.mobile as 电话, s.contimes as 总消费次数, s.totalconamount as 总消费金额,
s.totalintegral as 总积分 from consumptionsummary as s,members as m where s.mid=m.id and (m.username='{0}' or m.mobile='{0}');", searchText);
            DataTable top10Cus = new DataTable();
            top10Cus = ExecuteSQL(sqlGetTop10Cus);
            return top10Cus;
        }

        /// <summary>
        /// 获取所有会员
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataTable GetMemberBySearchText(string searchText)
        {
            DataTable dt = new DataTable();
            string sqlSearch = "select id as 会员排名, username as 会员, mobile as 电话号码 from members where username='" + searchText + "'" + "or mobile='" + searchText + "'";
            dt = ExecuteSQL(sqlSearch);
            return dt;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static DataTable GetConDetailsforDisply(string mid)
        {
            //联合查询，需要加上member表
            string sqlGetTop10Cus = string.Format(@"select d.id as 消费排名,m.username as 会员, m.mobile as 电话, d.contype as 总消费类型, 
d.conamount as 消费金额, d.integralchange as 积分变更, d.contime as 消费日期 from consumptiondetail as d, members as m where d.mid=m.id and mid='{0}' order by contime desc;", mid);
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
            string sqlGetTop10Cus = "select mobile as 电话, contimes as 总消费次数, totalconamount as 总消费金额, totalintegral as 总积分 from consumptionsummary order by totalconamount desc limit 10;";
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
        public static int InsertConSummary(string mid, int conTimes, int conTotalAmount, int conIntegral)
        {
            int affectCount = 0;
            if (!String.IsNullOrEmpty(mid))
            {
                string sqlInsertCustomer = string.Format("insert into consumptionsummary(mid,contimes,totalconamount,totalintegral) values({0},{1},{2},{3})", mid, conTimes, conTotalAmount, conIntegral);
                affectCount = ExecuteNonQuery(sqlInsertCustomer);
            }
            else
            {
                return affectCount;
            }

            return affectCount;
        }

        public static int UpdateConSummary(string mid, int conTimes, int conTotalAmount, int conIntegral)
        {
            int affectCount = 0;
            if (!String.IsNullOrEmpty(mid))
            {
                string sqlInsertCustomer = string.Format("update consumptionsummary set contimes={1},totalconamount={2},totalintegral={3} where mid='{0}'", mid, conTimes, conTotalAmount, conIntegral);
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
        /// <param name="mid"></param>
        /// <param name="conType"></param>
        /// <param name="conAmount"></param>
        /// <param name="conIntegral"></param>
        /// <returns></returns>
        public static int InsertConDetail(string mid, string conType, int conAmount, int conIntegral)
        {
            int affectCount = 0;
            //string date = DateTime.Now.Date.ToString();
            if (!String.IsNullOrEmpty(mid) && !String.IsNullOrEmpty(mid))
            {
                string sqlInsertIntegrel = string.Format(@"insert into consumptiondetail(mid,contype,conamount,integralchange,contime) 
values({0},'{1}',{2},{3},now())", mid, conType, conAmount, conIntegral);
                affectCount = ExecuteNonQuery(sqlInsertIntegrel);
            }
            else
            {
                return affectCount;
            }

            return affectCount;
        }


        public static void RecalculateCustomer(string mid)
        {
            int conTimes = 0;
            //查询integrel表中消费的次数
            string sqlQueryIntegrel = "select * from consumptiondetail where mid='" + mid + "'";
            DataTable dtIntegrel = new DataTable();
            dtIntegrel = Postgres.ExecuteSQL(sqlQueryIntegrel);

            conTimes = dtIntegrel.Rows.Count;
            if (conTimes == 0)
            {
                //已经全部删除消费记录，需要删除customer记录
                string slqDeleteCustomer = "delete from consumptionsummary where mid='" + mid + "'";
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
                Postgres.UpdateConSummary(mid, conTimes, conAmount2, conIntegrel2);
            }

        }

        /// <summary>
        /// 新建会员
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static int InsertMember(string name, string mobile)
        {
            int affectRow = 0;
            string sqlInsertMemb = string.Format("Insert into Members(username,mobile) values('{0}','{1}')", name, mobile);
            affectRow = ExecuteNonQuery(sqlInsertMemb);
            return affectRow;
        }

        /// <summary>
        /// 通过mid获取所有的consumption detail
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public static DataTable GetConDetailsByMid(string mid)
        {
            string sqlGetTop10Cus = string.Format(@"select * from consumptiondetail where mid='{0}';", mid);
            DataTable detial = new DataTable();
            detial = ExecuteSQL(sqlGetTop10Cus);
            return detial;
        }

        /// <summary>
        /// 根据用户名，修改电话号码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newMobile"></param>
        /// <returns></returns>
        public static int UpdateMembersMobile(string name, string oldMobile, string newMobile)
        {
            int affectCount = 0;
            string sqlUpdate = string.Format("update members set mobile='{0}' where username='{1}' and mobile='{2}'", newMobile, name, oldMobile);
            affectCount = Postgres.ExecuteNonQuery(sqlUpdate);
            return affectCount;
        }


        /// <summary>
        /// 删除消费summary信息
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public static int DeleteConSummaryByMid(string mid)
        {
            int a = 0;
            string sqlD = string.Format("delete from consumptionsummary where mid='{0}'",mid);
            a = ExecuteNonQuery(sqlD);
            return a;
        }


        /// <summary>
        /// 删除消费detail信息
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public static int DeleteConDetailByMid(string mid)
        {
            int a = 0;
            string sqlD = string.Format("delete from consumptiondetail where mid='{0}'", mid);
            a = ExecuteNonQuery(sqlD);
            return a;
        }

        #endregion
    }
}
