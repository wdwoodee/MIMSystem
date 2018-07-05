using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MIMSystem.DLA;
using System.Data.SQLite;
using System.Configuration;


namespace MIMSystem
{
    public partial class Form1 : Form
    {
        //public static string connectionString = ConfigurationManager.ConnectionStrings["connectString"].ConnectionString;
        public static string mobileForDetailQuery = string.Empty;
        public Form1()
        {
            InitializeComponent();

            #region tooltip
            ToolTip ttpSetting = new ToolTip();
            ttpSetting.InitialDelay = 200;
            ttpSetting.AutoPopDelay = 10 * 1000;
            ttpSetting.ReshowDelay = 200;
            ttpSetting.ShowAlways = true;
            ttpSetting.IsBalloon = true;
            string tipOverwrite2 = "请输入会员名称或者手机号码！";
            ttpSetting.SetToolTip(txtBoxSearch, tipOverwrite2);
            #endregion

            //调用初始化数据方法；
            reLoadDataForm1();

            // 设定包括Header和所有单元格的列宽自动调整
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // 设定包括Header和所有单元格的行高自动调整
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //dataGridView2.Rows[0].ContextMenuStrip = this.contextMenuStrip1; 

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            #region test sqlite
            //MessageBox.Show(DBHelper.TestConnection());
            //MessageBox.Show(DBAccess.TestConnection());
            //string insert = "insert into User(Username,password) values(dong,12345678);";
            //SQLiteCommand cm = new SQLiteCommand();
            //cm.CommandText = insert;

            //DBHelper.ExecuteNonQuery(insert);

            //DBHelper2.ExecuteNonQuery(connectionString, cm);
            //Postgres pg = new Postgres();
            //MessageBox.Show(DBAccess.TestConnection());
            //MessageBox.Show(Postgres.TestConnection());

            //DataTable dtAllTables = DBHelper.ExecuteSQL(allTables);

            //DataTable dtCustomer = DBAccess.ExecuteSQL(sqlCustomer);
            //DataTable dtDetail = DBAccess.ExecuteSQL(sqlDetail);


            //string insert = "insert into User(Username,password) values(dong,12345678);";
            #endregion

            # region test postgress
            //MessageBox.Show(Postgres.TestConnection());
            //string sqlUser = "select count(*) from users where Username = 'admin'";
            //int a = Postgres.ExecuteScaler(sqlUser);
            //Console.WriteLine(a);
            //获取所有的table name
            //string allTables = "select name from sqlite_master where type='table' order by name";
            //DataTable dtAllTables = Postgres.ExecuteSQL(allTables);

            //create table test
            #endregion
            
            #region Test create table, postgresql
            //string createTabUser = "Create Table User2(Username nvarchar(100) NOT NULL, Password nvarchar(100) NOT NULL)";
            //Postgres.ExecuteNonQuery(createTabUser);

            //string insert2 = "insert into User2(Username,password) values(dong,12345678);";
            //Postgres.ExecuteSQL(insert2);

            #endregion

            #region 根据mobile信息查询Summary

            string sqlSearch = txtBoxSearch.Text.ToString();
            if (string.IsNullOrEmpty(sqlSearch))
            {
                MessageBox.Show("请输入会员名或者电话！");
                return;
            }
            else
            {
                DataTable dtCustomer = Postgres.GetSummaryBySearchText(sqlSearch);
                //查询的结果显示到data grid view
                dataGridView2.DataSource = dtCustomer;
            }
            
            #endregion

        }
       


        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mobile;
            DialogResult dr = MessageBox.Show("确认删除此客户吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                int a = dataGridView2.CurrentRow.Index;
                mobile = dataGridView2.Rows[a].Cells[1].Value.ToString();
                if (mobile != null && mobile.Length > 0)
                {
                    //删除customer表记录
                    string sqlDeleteCustomer = "delete from customer where mobile='" + mobile + "'";

                    //删除积分表记录
                    string sqlDeleteIntegral = "delete from integral where mobile='" + mobile + "'";
                    Postgres.ExecuteNonQuery(sqlDeleteCustomer);
                    Postgres.ExecuteNonQuery(sqlDeleteIntegral);
                    reLoadDataForm1();
                }
            }

        }

        private void 消费详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int a = dataGridView2.CurrentRow.Index;
            mobileForDetailQuery = dataGridView2.Rows[a].Cells[0].Value.ToString();
            if (mobileForDetailQuery != null && mobileForDetailQuery.Length > 0)
            {
                //fuForm中打开ziForm时需要设置所有者，就是ziForm的所有者是fuForm
                ConsumpationDetail conDetailForm = new ConsumpationDetail();
                conDetailForm.Owner = this;
                conDetailForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("非法查询！");
            }
        }

        //dataGridView选中一行时右键出现菜单
        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    dataGridView2.ClearSelection();
                    dataGridView2.Rows[e.RowIndex].Selected = true;
                    dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        public void reLoadDataForm1()
        {
            DataTable top10Cus = new DataTable();
            top10Cus = Postgres.GetTop10Summary();
            dataGridView2.DataSource = top10Cus;
        }

        private void 添加会员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form addMem = new AddMember();
            addMem.Owner = this;
            addMem.ShowDialog();
        }

        private void 消费ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form newConFrom = new NewConsumpation();
            newConFrom.Owner = this;
            newConFrom.ShowDialog();
            //new NewConsumpation().ShowDialog();
            //this.Hide();
        }

        private void 修改电话号码ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
