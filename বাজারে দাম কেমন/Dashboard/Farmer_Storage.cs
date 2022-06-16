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
using System.IO;

namespace Dashboard
{
    public partial class Farmer_Storage : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        //int id;
        private int id;

        public Farmer_Storage()
        {
            InitializeComponent();
            BindGridView();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            //ofd.Filter = "PNG FILE (*.PNG) | *.PNG";
            ofd.Filter = "ALL IMAGE FILE (*.*) | *.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(ofd.FileName);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                textBox1.Focus();
                errorProvider1.Icon = Properties.Resources.Error;
                errorProvider1.SetError(this.textBox1, "Please Fill The Field");
            }
            else
            {
                errorProvider1.Icon = Properties.Resources.Check;
                errorProvider1.SetError(this.textBox1, "Fulfild");
                //errorProvider1.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                textBox2.Focus();
                errorProvider2.Icon = Properties.Resources.Error;
                errorProvider2.SetError(this.textBox2, "Please Fill The Field");
            }
            else
            {
                errorProvider2.Icon = Properties.Resources.Check;
                errorProvider2.SetError(this.textBox2, "Fulfild");
                //errorProvider2.Clear();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "insert into farmerproduct_tbl values (@item,@amount,@price,@date,@pic)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@item", textBox1.Text);
                cmd.Parameters.AddWithValue("@amount", textBox2.Text);
                cmd.Parameters.AddWithValue("@price", textBox3.Text);
                cmd.Parameters.AddWithValue("@date", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@pic", SavePhoto());

                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    BindGridView();
                    ResetControl();
                    MessageBox.Show("Data Insert Successfully..!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Data Not Insert Successfull..!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Fill The Field!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private byte[] SavePhoto()
        {
            MemoryStream s = new MemoryStream();
            pictureBox2.Image.Save(s, pictureBox2.Image.RawFormat);
            return s.GetBuffer();
        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from farmerproduct_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            //Image
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[5];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            //column auto size
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 80;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "update farmerproduct_tbl set item=@item,amount=@amount,price=@price,edate=@date,picture=@pic where id='" + id +"'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@item", textBox1.Text);
                cmd.Parameters.AddWithValue("@amount", textBox2.Text);
                cmd.Parameters.AddWithValue("@price", textBox3.Text);
                cmd.Parameters.AddWithValue("@date", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@pic", SavePhoto());

                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    BindGridView();
                    ResetControl();
                    MessageBox.Show("Data Update Successfully..!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Data Not Update Successfull..!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Fill The Field!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "delete from farmerproduct_tbl where id='" + id + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                

                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    BindGridView();
                    ResetControl();
                    MessageBox.Show("Data Delete Successfully..!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Data Not Delete Successfull..!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Fill The Field!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        void ResetControl()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.ResetText();
            pictureBox2.Image = Properties.Resources.no_image_avaiable;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[4].Value);
            pictureBox2.Image= GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[5].Value);
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream p = new MemoryStream(photo);
            return Image.FromStream(p);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Farmer_Login farmer_Login = new Farmer_Login();
            farmer_Login.Show();
            this.Hide();
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
    }
}
