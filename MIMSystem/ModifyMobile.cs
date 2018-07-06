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
    public partial class ModifyMobile : Form
    {
        public ModifyMobile()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            #region get txt

            string name = txtBoxUser.Text;
            string oldMobile = txtBoxMobile.Text;
            string newMobile = txtBoxNewMobile.Text;
            string newMobileAgain = txtBoxNewMobileA.Text;

            #endregion

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入会员名！");
                return;
            }
            if (string.IsNullOrEmpty(oldMobile))
            {
                MessageBox.Show("请输入老电话号码！");
                return;
            }
            else if (oldMobile.Length != 11)
            {
                MessageBox.Show("请输入11位号码！");
                return;
            }

            if (string.IsNullOrEmpty(newMobile) || string.IsNullOrEmpty(newMobileAgain))
            {
                MessageBox.Show("请输入新号码！");
                return;
            }
            else if (newMobile.Length != 11 || newMobileAgain.Length != 11)
            {
                MessageBox.Show("请输入11位新号码号码！");
                return;
            }
            else if (newMobile != newMobileAgain)
            {
                MessageBox.Show("新号码两次输入不相同，请重新输入！");
                return;
            }
            else
            {
                //string sqlName = "select count(*) from members where username='" + name + "'";

                //string sqlMobile = "select count(*) from members where mobile='" + oldMobile + "'";

                //DataTable dt1 = new DataTable();
                //DataTable dt2 = new DataTable();
                //dt1 = Postgres.ExecuteSQL(sqlMobile);
                //dt2 = Postgres.ExecuteSQL(sqlName);

                //if (Convert.ToInt32(dt2.Rows[0]["count"]) == 0)
                //{
                //    MessageBox.Show("该会员不存在!");
                //    return;
                //}
                //else if (Convert.ToInt32(dt1.Rows[0]["count"]) == 0)
                //{
                //    MessageBox.Show("该会员的电话号码错误，");
                //    return;
                //}
                //else
                //{
                //    Postgres.UpdateMembersMobile(name, oldMobile, newMobile);
                //}


                DataTable dt = new DataTable();

                string sqlNM = "select count(*) from members where username='" + name + "'" + "and mobile='" + oldMobile + "'";
                dt = Postgres.ExecuteSQL(sqlNM);

                int count = Convert.ToInt32(dt.Rows[0]["count"]);

                if (count == 0)
                {
                    MessageBox.Show("该会员不存在或者电话号不正确!");
                    return;
                }
                else
                {
                    string sqlSelectCount = "select count(*) from members where mobile='" + newMobile + "'";
                    DataTable dtMembs = Postgres.ExecuteSQL(sqlSelectCount);
                    if (Convert.ToInt32(dtMembs.Rows[0]["count"]) > 0)
                    {
                        MessageBox.Show("该会员的电话在系统中已经存在！");
                        return;
                    }
                    else 
                    {
                        Postgres.UpdateMembersMobile(name, oldMobile, newMobile);
                        this.Close();
                    }
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
