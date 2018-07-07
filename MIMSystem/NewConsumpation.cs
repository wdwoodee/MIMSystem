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

            #region 创建tooltip
            //创建tooltip
            ToolTip ttpSettings = new ToolTip();

            ttpSettings.InitialDelay = 200;

            ttpSettings.AutoPopDelay = 10 * 1000;

            ttpSettings.ReshowDelay = 200;

            ttpSettings.ShowAlways = true;

            ttpSettings.IsBalloon = true;

            string tipOverwrite = "积分的示例：-100或者100";
            ttpSettings.SetToolTip(txtBoxIntegral, tipOverwrite);


            ToolTip ttpSetting2 = new ToolTip();

            ttpSetting2.InitialDelay = 200;

            ttpSetting2.AutoPopDelay = 10 * 1000;

            ttpSetting2.ReshowDelay = 200;

            ttpSetting2.ShowAlways = true;

            ttpSetting2.IsBalloon = true;

            string tipOverwrite2 = "金额的示例：-100，或者100";
            //string tipOverwrite2 = "请输入变化积分，例如：-100，或者100。换购示例：-100。含义：减200，增加100，总数-100";

            ttpSetting2.SetToolTip(txtBoxConAmount, tipOverwrite2);
            #endregion
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region get txt value

            string conType = cmbBoxConType.Text.Trim();
            string mobile = txtBoxMobile.Text.Trim();
            if (mobile == null || mobile.Count() != 11)
            {
                MessageBox.Show("请输入正确11位手机号码！");
                return;
            }

            int conAmount = 0; ;
            if (txtBoxConAmount.Text == null)
            {
                MessageBox.Show("请输入消费金额，例如：100或者-100");
                return;
            }
            else
            {
                try
                {
                    conAmount = Convert.ToInt32(txtBoxConAmount.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("请输入数值类型，例如：100或者-100");
                    return;
                }
            }
            int conIntegrelChange = 0;
            //int conIncreaseInte = 0;
            //int conDecreaseInte = 0;

            //获取新增积分
            if (txtBoxIntegral.Text.Trim() == null)
            {
                MessageBox.Show("请输入积分，例如：100或者-100");
                return;
            }
            else
            {
                try
                {
                    conIntegrelChange = Convert.ToInt32(txtBoxIntegral.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("请输入数值类型数据，例如：100或者-100");
                }
            }

            #endregion

            //使用mobile查询members表中是否有对应的members,记录唯一
            string sqlQuery = "select * from members where mobile='" + mobile + "'";
            DataTable custmoer = Postgres.ExecuteSQL(sqlQuery);
            int conTimes = 0;
            if (custmoer.Rows.Count == 0)
            {
                //为0，则表示新客户
                #region old code
                //conTimes = 1;
                ////更新Customer表
                //Postgres.InsertCustomer(mobile, conTimes, conAmount, conIntegrelChange);
                ////更新Integrel表
                //Postgres.InsertIntegrel(mobile, conType, conAmount, conIntegrelChange);
                #endregion

                MessageBox.Show("新顾客，请先添加会员！");
                return;
            }
            else
            {
                //不为0，则表示不是新客户

                //获取客户id
                string id = custmoer.Rows[0]["id"].ToString();
                //获取客户name
                string name = custmoer.Rows[0]["username"].ToString();

                //更新consumpationdetail表
                Postgres.InsertConDetail(id, conType, conAmount, conIntegrelChange);

                //查询consupationdetail表中消费的次数
                DataTable dtIntegrel = new DataTable();
                dtIntegrel = Postgres.GetConDetailsByMid(id);

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

                string sqlSelectSummary = "select count(*) from consumptionsummary where mid='" + id + "'";

                DataTable dt = Postgres.ExecuteSQL(sqlSelectSummary);

                if (Convert.ToInt32( dt.Rows[0]["count"]) != 0)
                {
                    //如果不是第一次插入，则更新
                    Postgres.UpdateConSummary(id, conTimes, conAmount2, conIntegrel2);
                }
                else
                {
                    //否则，插入
                    Postgres.InsertConSummary(id, conTimes, conAmount2, conIntegrel2);
                }

            }
            this.Hide();
            //this.Hide();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.Hide();
            //this.Close();
        }

        /// <summary>
        /// close此窗体时，重新load父窗体的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewConsumpation_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form1;
            form1 = (Form1)this.Owner;
            form1.reLoadDataForm1();
        }





    }
}
