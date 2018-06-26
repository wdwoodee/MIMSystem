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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            UserCheck();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string sqlUsers = "select count(*) from users where username='" + username + "'" + "and " + "password='" + password + "'";
            int count = 0;
            count = Postgres.ExecuteScaler(sqlUsers);
            if (count != 0)
            {
                //User existed
                new Form1().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或者密码错误！");
            }

        }

        private void lnkLabChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new ChangePassword().ShowDialog();
            
        }

        private static void UserCheck()
        {
            string sqlUser = "select count(*) from users";
            string sqlInsertUser = "insert users(username,password) values('admin','admin')";
            if (Convert.ToInt32(Postgres.ExecuteScaler(sqlUser)) < 1)
            {
                Postgres.ExecuteNonQuery(sqlInsertUser);
            }
        }
    }
}
