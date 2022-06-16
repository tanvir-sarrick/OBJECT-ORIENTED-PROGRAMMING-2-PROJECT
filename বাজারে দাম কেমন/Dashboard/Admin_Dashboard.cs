using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Dashboard
{
    public partial class Admin_Dashboard : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin_to_Farmer admin_To_Farmer = new Admin_to_Farmer();
            admin_To_Farmer.Show();
            this.Hide();
        }
         
        private void button3_Click(object sender, EventArgs e)
        {
            Admin_to_Customer admin_To_Customer = new Admin_to_Customer();
            admin_To_Customer.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Admin_Login admin_Login = new Admin_Login();
            admin_Login.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Order Admin_Order = new Admin_Order();
            Admin_Order.Show();
        }

        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection(cs);
            Con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from farmer_tbl", Con);
            DataTable dt = new DataTable();
            sda1.Fill(dt); Farmer.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda2 = new SqlDataAdapter("select count(*) from customer_tbl", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2); customer.Text = dt2.Rows[0][0].ToString();



            SqlDataAdapter sda3 = new SqlDataAdapter("select count(*) from order_tbl", Con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3); order.Text = dt3.Rows[0][0].ToString();



            SqlDataAdapter sda4 = new SqlDataAdapter("select count(*) from admin_tbl", Con);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4); admin.Text = dt4.Rows[0][0].ToString();
            Con.Close();
        }

        private void Farmer_Click(object sender, EventArgs e)
        {

        }
    }
}
