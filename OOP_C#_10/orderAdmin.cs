using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_C__10
{
    public partial class orderAdmin : Form
    {
        public string login;
        string connectionString = "Data Source=MUSHU;Initial Catalog=orders;Integrated Security=True;";
        public orderAdmin()
        {
            InitializeComponent();
            fillDGV();
        }
 
        private void fillDGV()
        {
            this.ordersTableAdapter.Fill(this.ordersDataSet.orders);
            this.usersTableAdapter.Fill(this.ordersDataSet.users);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Insert into orderr (orderr, status, client, userr, datee, date_finish) values (@orderr, @status, @client, @userr, @datee, @date_finish)";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("orderr", textBox1.Text);
                    command.Parameters.AddWithValue("@status", 1);
                    command.Parameters.AddWithValue("@client", textBox2.Text);
                    command.Parameters.AddWithValue("@userr", comboBox2.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@datee", DateTime.Now.ToString("dd.MM.yyyy"));
                    if (dateTimePicker1.Value > DateTime.Today.AddDays(7))
                    {
                        command.Parameters.AddWithValue("@date_finish", dateTimePicker1.Value.ToShortDateString());
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@date_finish", DateTime.Today.AddDays(7).ToString("dd.MM.yyyy"));
                        MessageBox.Show("Дата сдачи, была автоматически исправлена на неделю впереёд!");
                    }
                    command.ExecuteNonQuery();
                }
            }
            fillDGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Update orderr set orderr = @orderr, client = @client, status = @status, userr = @user, date_finish = @date_finish where id_order = @id_order ";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("orderr", textBox1.Text);
                    command.Parameters.AddWithValue("@client", textBox2.Text);
                    command.Parameters.AddWithValue("@status", comboBox1.SelectedIndex + 1);
                    command.Parameters.AddWithValue("@user", comboBox2.SelectedValue.ToString());
                    if (dateTimePicker1.Value > DateTime.Today.AddDays(7))
                    {
                        command.Parameters.AddWithValue("@date_finish", dateTimePicker1.Value.ToShortDateString());
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@date_finish", DateTime.Today.AddDays(7).ToString("dd.MM.yyyy"));
                        MessageBox.Show("Дата сдачи, была автоматически исправлена на неделю впереёд!");
                    }
                    command.Parameters.AddWithValue("id_order", dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                    command.ExecuteNonQuery();
                }
            }
            fillDGV();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "delete from  orderr  where id_order = @id_order";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("id_order", dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                    command.ExecuteNonQuery();
                }
            }
            fillDGV();
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                textBox1.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                textBox2.Text = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                comboBox1.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                comboBox2.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            PersonalAdmin PA = new PersonalAdmin();
            PA.login = login;
            PA.Show();
        }
    }
}
