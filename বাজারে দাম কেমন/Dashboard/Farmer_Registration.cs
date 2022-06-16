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
using System.Text.RegularExpressions;
using System.IO;

namespace Dashboard
{
    public partial class Farmer_Registration : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        //string pattren = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
        public Farmer_Registration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Farmer_Login Farmer_Login = new Farmer_Login();
            Farmer_Login.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Opening Opening = new Opening();
            Opening.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "ALL IMAGE FILE (*.*) | *.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                textBox1.Focus();
                errorProvider1.Icon = Properties.Resources.Error;
                errorProvider1.SetError(this.textBox1, "Please Fill The Name Field");
            }
            else
            {
                errorProvider1.Icon = Properties.Resources.Check;
                errorProvider1.SetError(this.textBox1, "Fulfild");
                //errorProvider1.Clear();
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text) == true)
            {
                textBox7.Focus();
                errorProvider2.Icon = Properties.Resources.Error;
                errorProvider2.SetError(this.textBox7, "Please Fill The Username Field");
            }
            else
            {
                errorProvider2.Icon = Properties.Resources.Check;
                errorProvider2.SetError(this.textBox7, "Fulfild");
                //errorProvider2.Clear();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text) == true)
            {
                //textBox5.Focus();
                errorProvider3.Icon = Properties.Resources.Error;
                errorProvider3.SetError(this.textBox5, "Please Fill Password Field");
            }
            //else if (Regex.IsMatch(textBox5.Text, pattren) == false)
            //{
            //    //textBox5.Focus();
            //    errorProvider3.Icon = Properties.Resources.Error;
            //    errorProvider3.SetError(this.textBox5, "Please Fill UPPERCASE,LOWERCASE,NUMBERS,SPECIAL CHARACTERS");
            //}
            else
            {
                errorProvider3.Icon = Properties.Resources.Check;
                errorProvider3.SetError(this.textBox5, "Fulfild");
                //errorProvider3.Clear();
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) == true)
            {
                //textBox6.Focus();
                errorProvider4.Icon = Properties.Resources.Error;
                errorProvider4.SetError(this.textBox6, "Please Fill Confram Password Field");
            }
            else if (textBox5.Text != textBox6.Text)
            {
                //textBox6.Focus();
                errorProvider4.Icon = Properties.Resources.Error;
                errorProvider4.SetError(this.textBox6, "Password Not Match");
            }
            else
            {
                errorProvider4.Icon = Properties.Resources.Check;
                errorProvider4.SetError(this.textBox6, "Fulfild");
                //errorProvider4.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" 
                && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "insert into farmer_tbl values (@name,@username,@address,@nid,@phone,@pass,@pic)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@username", textBox7.Text);
                cmd.Parameters.AddWithValue("@address", textBox2.Text);
                cmd.Parameters.AddWithValue("@nid", textBox3.Text);
                cmd.Parameters.AddWithValue("@phone", textBox4.Text);
                cmd.Parameters.AddWithValue("@pass", textBox5.Text);
                cmd.Parameters.AddWithValue("@pic", SavePhoto());

                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    this.Hide();
                    Farmer_Login Farmer_Login = new Farmer_Login();
                    Farmer_Login.Show();
                    MessageBox.Show("Registration Successfully..!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Registration Not Successfull..!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Fill The Field!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }
    }
}
