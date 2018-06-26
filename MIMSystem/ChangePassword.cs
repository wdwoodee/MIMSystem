using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MIMSystem.DLA;

namespace MIMSystem
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string user = txtBoxUsername.Text;
            string oldPwd = txtBoxPassword.Text;
            string newPwd = txtBoxNewPwd.Text;
            string newPwd2 = txtBoxNewPwd2.Text;

            string sqlSelectUser = "select username,password from users where username='" + user + "'";
            string sqlUpdateUser = "update users set password='" + newPwd + "'" + "where username='" + user + "'";
            DataTable dt = new DataTable();
            dt = Postgres.ExecuteSQL(sqlSelectUser);
            if (dt.Rows.Count > 0)
            {
                //user exist
                if (dt.Rows[0][1].ToString() != oldPwd)
                {
                    MessageBox.Show("原密码输入有误，请重新输入！");
                    return;
                }
                else
                {
                    if (newPwd == newPwd2 && newPwd != oldPwd)
                    {
                        Postgres.ExecuteNonQuery(sqlUpdateUser);
                        this.Close();
                    }
                    else if (newPwd == oldPwd)
                    {
                        MessageBox.Show("新密码和原密码不能相同，请重新输入！");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("新密码两次输入不相同，请重新输入！");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("用户不存在！");
                return;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
