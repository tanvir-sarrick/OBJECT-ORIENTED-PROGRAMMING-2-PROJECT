﻿using System;
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
    public partial class Customer_Login : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Customer_Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                textBox1.Focus();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            switch (status)
            {
                case true:
                    textBox2.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox2.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "select * from customer_tbl where username=@user and pass=@pass";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {

                    Customer_to_Farmer Customer_to_Farmer = new Customer_to_Farmer();
                    Customer_to_Farmer.Show();
                    this.Hide();
                    MessageBox.Show("Login Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Login Faled!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Please Fill The Field!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Opening Opening = new Opening();
            Opening.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer_Registration Customer_Registration = new Customer_Registration();
            Customer_Registration.Show();
        }
    }
}
