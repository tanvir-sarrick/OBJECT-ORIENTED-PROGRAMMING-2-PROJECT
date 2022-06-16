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
    public partial class Farmer_Order_Customer : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Farmer_Order_Customer()
        {
            InitializeComponent();
            BindGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Farmer_Storage farmer_Storage = new Farmer_Storage();
            farmer_Storage.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Farmer_Order_Customer farmer_Order_Customer = new Farmer_Order_Customer();
            farmer_Order_Customer.Show();
            this.Hide();
        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from order_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            //Image
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[4];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            //column auto size
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 80;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Farmer_Login farmer_Login = new Farmer_Login();
            farmer_Login.Show();
            this.Hide();
        }
    }
}
