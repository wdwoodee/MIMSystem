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
    public partial class AddMember : Form
    {
        public AddMember()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            #region get name and mobile
            string name = txtBoxName.Text.Trim();
            string mobile = txtBoxMobile.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入会员名，会员名不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(mobile))
            {
                MessageBox.Show("请正确输入电话，电话不能为空！");
                return;
            }
            else if (mobile.Length != 11)
            {
                MessageBox.Show("请输入11位电话号码！");
                return;
            }
            #endregion

            string sqlSelectCount = "select count(*) from members where mobile='" + mobile + "'";
            DataTable dtMembs = Postgres.ExecuteSQL(sqlSelectCount);
            if (Convert.ToInt32(dtMembs.Rows[0]["count"]) > 0)
            {
                MessageBox.Show("该会员的电话在系统中已经存在！");
                return;
            }
            else
            {
                Postgres.InsertMember(name, mobile);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
