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
    public partial class Opening : Form
    {
        
        public Opening()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            About_Us About_Us = new About_Us();
            About_Us.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Farmer_Login Farmer_Login = new Farmer_Login();
            Farmer_Login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer_Login Customer_Login = new Customer_Login();
            Customer_Login.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Login Admin_Login = new Admin_Login();
            Admin_Login.Show();
        }
    }
}
