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

namespace MIMSystem
{
    public partial class ConsumpationDetail : Form
    {
        public ConsumpationDetail()
        {
            InitializeComponent();

            //显示所查询数据
            //dataGridViewDetail.DataSource = Postgres.GetConDetail(Form1.mobileForDetailQuery);
            reloadData(Form1.mobileForDetailQuery);

            // 设定包括Header和所有单元格的列宽自动调整
            dataGridViewDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // 设定包括Header和所有单元格的行高自动调整
            dataGridViewDetail.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            this.dataGridViewDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            //选中一行数据
            dataGridViewDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
        }

        //设置datagridview的方法
        //1.先绑定datagridview的conttextmenu属性为contextMenuStripDetail
        //2.再设置CellMouseUp事件

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewDetail_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    dataGridViewDetail.ClearSelection();
                    dataGridViewDetail.Rows[e.RowIndex].Selected = true;
                    dataGridViewDetail.CurrentCell = dataGridViewDetail.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStripDetail.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void 删除此次消费记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = 0;
            DialogResult dr = MessageBox.Show("确认删除此次客户消费记录吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                int a = dataGridViewDetail.CurrentRow.Index;
                id = Convert.ToInt32(dataGridViewDetail.Rows[a].Cells[0].Value);
                string moible = dataGridViewDetail.Rows[a].Cells[2].Value.ToString();

                string sqldelete = "delete from consumptiondetail where id='" + id + "'";

                Postgres.ExecuteNonQuery(sqldelete);
                Postgres.RecalculateCustomer(Form1.mid);
                //dataGridViewDetail.DataSource = Postgres.GetConDetail(moible);
                reloadData(Form1.mid);
            }
        }
        public void reloadData(string mid)
        {
            //显示初始化数据
            dataGridViewDetail.DataSource = Postgres.GetConDetailsforDisply(mid);
        }

        //private void ConsumpationDetail_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Form1 form1;
        //    form1 = (Form1)this.Owner;
        //    form1.reLoadFrom();
        //}

       //出发closing事件时，reload data。
        private void ConsumpationDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1;
            form1 = (Form1)this.Owner;
            form1.reLoadDataForm1();
        }
    }
}
