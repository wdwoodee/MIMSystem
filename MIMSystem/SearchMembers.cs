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
    public partial class SearchMembers : Form
    {
        public SearchMembers()
        {
            InitializeComponent();

            //显示所查询数据
            //dataGridViewDetail.DataSource = Postgres.GetConDetail(Form1.mobileForDetailQuery);
            dataGridViewMember.DataSource = loadAllMembers();

            // 设定包括Header和所有单元格的列宽自动调整
            dataGridViewMember.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // 设定包括Header和所有单元格的行高自动调整
            dataGridViewMember.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            this.dataGridViewMember.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            //选中一行数据
            dataGridViewMember.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }




        public static DataTable loadAllMembers()
        {
            DataTable a = new DataTable();

            string sql = "select id as 会员排名, username as 会员, mobile as 电话号码 from members";
            a = Postgres.ExecuteSQL(sql);
            return a;
        }

        private void btnSearchMem_Click(object sender, EventArgs e)
        {
            string m = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(m))
            {
                MessageBox.Show("请输入会员名或者电话！");
                return;
            }
            else
            {
                DataTable dtm = Postgres.GetMemberBySearchText(m);
                dataGridViewMember.DataSource = dtm;
            }
        }

       
        private void dataGridViewMember_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    dataGridViewMember.ClearSelection();
                    dataGridViewMember.Rows[e.RowIndex].Selected = true;
                    dataGridViewMember.CurrentCell = dataGridViewMember.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMSDeleteMember.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void 删除会员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = 0;
            DialogResult dr = MessageBox.Show("确认删除此次客户消费记录吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                int a = dataGridViewMember.CurrentRow.Index;
                id = Convert.ToInt32(dataGridViewMember.Rows[a].Cells[0].Value);

                string sqldelete = "delete from members where id='" + id + "'";
                
                
                Postgres.ExecuteNonQuery(sqldelete);
                Postgres.DeleteConSummaryByMid(id.ToString());
                Postgres.DeleteConDetailByMid(id.ToString());

                dataGridViewMember.DataSource = loadAllMembers();
            }
        }

        private void SearchMembers_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1;
            form1 = (Form1)this.Owner;
            form1.reLoadDataForm1();
        }
    }
}
