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

            ToolTip ttpSetting = new ToolTip();
            ttpSetting.InitialDelay = 200;
            ttpSetting.AutoPopDelay = 10 * 1000;
            ttpSetting.ReshowDelay = 200;
            ttpSetting.ShowAlways = true;
            ttpSetting.IsBalloon = true;
            string tipOverwrite2 = "请输入11位手机号码！";
            ttpSetting.SetToolTip(txtBoxMobile, tipOverwrite2);


            DataTable top10Cus = new DataTable();
            top10Cus = Postgres.GetTop10InteCustomer();
            dataGridView2.DataSource = top10Cus;


            // 设定包括Header和所有单元格的列宽自动调整
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // 设定包括Header和所有单元格的行高自动调整
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //dataGridView2.Rows[0].ContextMenuStrip = this.contextMenuStrip1; 

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void button1_Click(object sender, EventArgs e)
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
            #endregion

            string sqlCustomer = @"select * from Customer where Mobile ='" + txtBoxMobile.Text + "'";
            string sqlDetail = @"select * from Integral where Mobile ='" + txtBoxMobile.Text + "'";
            string allTables = "select name from sqlite_master where type='table' order by name";
            DataTable dtAllTables = Postgres.ExecuteSQL(allTables);

            DataTable dtCustomer = Postgres.ExecuteSQL(sqlCustomer);
            DataTable dtDetail = Postgres.ExecuteSQL(sqlDetail);

            string createTabUser = "Create Table User2(Username nvarchar(100) NOT NULL, Password nvarchar(100) NOT NULL)";
            Postgres.ExecuteNonQuery(createTabUser);
            string insert2 = "insert into User2(Username,password) values(dong,12345678);";
            Postgres.ExecuteSQL(insert2);


        }

        private void btnNewCon_Click(object sender, EventArgs e)
        {
            Form NewConsumpation = new NewConsumpation();
            NewConsumpation.ShowDialog();
            //new NewConsumpation().ShowDialog();

            //this.Hide();
        }


        // RowContextMenuStripNeeded事件处理方法 

        //private void dataGridView2_RowContextMenuStripNeeded(object sender,

        //    DataGridViewRowContextMenuStripNeededEventArgs e)
        //{

        //    DataGridView dgv = (DataGridView)sender;

        //    // 当"Column1"列是Bool型且为True时、设定其的ContextMenuStrip 

        //    string boolVal = dgv["电话", e.RowIndex].Value.ToString();

        //    Console.WriteLine(boolVal);

        //    //if (boolVal is bool && (bool)boolVal)
        //    //{

        //    //    e.ContextMenuStrip = this.ContextMenuStrip;

        //    //}
        //    if (boolVal.Length > 0)
        //    {
        //        e.ContextMenuStrip = this.ContextMenuStrip;
        //    }

        //}

        //private void dataGridView2_RowContextMenuStripChanged(object sender, DataGridViewRowEventArgs e)
        //{
        //    dataGridView2.Rows[0].ContextMenuStrip = this.contextMenuStrip1; 
        //}

      
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mobile;
            DialogResult dr = MessageBox.Show("确认删除那客户记录吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                int a = dataGridView2.CurrentRow.Index;
                mobile = dataGridView2.Rows[a].Cells[0].Value.ToString();
                if (mobile != null && mobile.Length > 0)
                {
                    //删除customer表记录
                    string sqlDeleteCustomer = "delete from customer where mobile='" + mobile + "'";

                    //删除积分表记录
                    string sqlDeleteIntegral = "delete form integral where moibile='"+ mobile +"'";
                    Postgres.ExecuteNonQuery(sqlDeleteCustomer);
                    Postgres.ExecuteNonQuery(sqlDeleteIntegral);
                }
            }

        }

        private void 消费详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int a = dataGridView2.CurrentRow.Index;
            mobileForDetailQuery = dataGridView2.Rows[a].Cells[0].Value.ToString();
            if (mobileForDetailQuery != null && mobileForDetailQuery.Length > 0)
            {
                new ConsumpationDetail().ShowDialog();
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
       
    }
}
