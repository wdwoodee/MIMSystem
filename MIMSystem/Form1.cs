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
        public static string connectionString = ConfigurationManager.ConnectionStrings["connectString"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show(DBHelper.TestConnection());
            //MessageBox.Show(DBAccess.TestConnection());
            string insert = "insert into User(Username,password) values(dong,12345678);";
            DBHelper.ExecuteNonQuery(insert);

            SQLiteCommand cm = new SQLiteCommand();
            cm.CommandText = insert;
            //DBHelper2.ExecuteNonQuery(connectionString, cm);

            string sqlCustomer = @"select * from Customer where Mobile ='" + txtBoxMobile.Text + "'";
            string sqlDetail = @"select * from Integral where Mobile ='" + txtBoxMobile.Text + "'";
            string allTables = "select name from sqlite_master where type='table' order by name";
            DataTable dtAllTables = DBHelper.ExecuteSQL(allTables);
            
            DataTable dtCustomer = DBAccess.ExecuteSQL(sqlCustomer);
            DataTable dtDetail = DBAccess.ExecuteSQL(sqlDetail);

            string createTabUser = "Create Table User2(Username nvarchar(100) NOT NULL, Password nvarchar(100) NOT NULL)";
            DBHelper.ExecuteNonQuery(createTabUser);
            string insert2 = "insert into User2(Username,password) values(dong,12345678);";
            DBHelper.ExecuteSQL(insert2);


        }

        private void btnNewCon_Click(object sender, EventArgs e)
        {

        }
    }
}
