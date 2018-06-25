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
    public partial class NewConsumpation : Form
    {
        public NewConsumpation()
        {
            InitializeComponent();
            this.cmbBoxConType.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region get txt value

            string conType = cmbBoxConType.Text;
            string mobile = txtBoxMobile.Text;
            if (mobile == null || mobile.Count() != 11)
            {
                MessageBox.Show("请输入正确11位手机号码！");
                return;
            }
            int conAmount = 0; ;
            if (txtBoxConAmount.Text == null)
            {
                MessageBox.Show("请输入消费金额，例如：100");
                return;
            }
            else
            {
                try
                {
                    conAmount = Convert.ToInt32(txtBoxConAmount.Text);
                }
                catch
                {
                    MessageBox.Show("请输入消费金额，例如：100");
                    return;
                }
            }
            int conIntegrel = 0;
            if (txtBoxIntgeralChange.Text == null)
            {
                MessageBox.Show("请输入变化积分，例如：-100，或者100");
                return;
            }
            else
            {
                try
                {
                    conIntegrel = Convert.ToInt32(txtBoxIntgeralChange.Text);
                }
                catch
                {
                    MessageBox.Show("请输入变化积分，例如：-100，或者100");
                }
            }
            #endregion

            //使用mobile查询customer表中是否有对应的customer
            string sqlQuery = "select mobile from customer where mobile='" + mobile + "'";
            DataTable custmoer = Postgres.ExecuteSQL(sqlQuery);
            int conTimes = 0;
            if (custmoer.Rows.Count == 0)
            {
                //为0，则表示新客户
                conTimes = 1;
                //更新Customer表
                Postgres.InsertCustomer(mobile, conTimes, conAmount, conIntegrel);
                //更新Integrel表
                Postgres.InsertIntegrel(mobile, conType, conAmount, conIntegrel);
            }
            else
            {
                //不为0，则表示不是新客户

                //更新Integrel表
                Postgres.InsertIntegrel(mobile, conType, conAmount, conIntegrel);

                //查询integrel表中消费的次数
                string sqlQueryIntegrel = "select * from integral where mobile='" + mobile + "'";
                DataTable dtIntegrel = new DataTable();
                dtIntegrel = Postgres.ExecuteSQL(sqlQueryIntegrel);
                conTimes = dtIntegrel.Rows.Count;
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
            this.Hide();
            //this.Hide();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            //this.Hide();
            this.Close();
        }
    }
}
