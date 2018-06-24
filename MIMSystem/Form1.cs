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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //MessageBox.Show(DBHelper.TestConnection());
            //MessageBox.Show(DBAccess.TestConnection());
            //string insert = "insert into User(Username,password) values(dong,12345678);";
            //SQLiteCommand cm = new SQLiteCommand();
            //cm.CommandText = insert;
             
            //DBHelper.ExecuteNonQuery(insert);
            
            //DBHelper2.ExecuteNonQuery(connectionString, cm);
            //Postgres pg = new Postgres();
            MessageBox.Show(DBAccess.TestConnection());
            MessageBox.Show(Postgres.TestConnection());

            //DataTable dtAllTables = DBHelper.ExecuteSQL(allTables);

            //DataTable dtCustomer = DBAccess.ExecuteSQL(sqlCustomer);
            //DataTable dtDetail = DBAccess.ExecuteSQL(sqlDetail);


            //string insert = "insert into User(Username,password) values(dong,12345678);";
            string sqlUser = "select count(*) from users where Username = 'admin'";
            int a = Postgres.ExecuteScaler(sqlUser);
            Console.WriteLine(a);

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

        }
    }
}
