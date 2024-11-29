using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOP_C__10
{
    public partial class order : Form
    {
        public string login;
        public order()
        {
            InitializeComponent();
            fillDGV();
        }
        string connectionString = "Data Source=MUSHU;Initial Catalog=orders;Integrated Security=True;";

 
        private void fillDGV()
        {
            this.ordersTableAdapter.Fill(this.ordersDataSet.orders);

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >0) {
                
                textBox1.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                textBox2.Text = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                comboBox1.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1[6,dataGridView1.CurrentRow.Index].Value.ToString() == login) {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    string query = "Update orderr set orderr = @orderr, client = @client, status = @status, date_finish = @date_finish where id_order = @id_order ";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        con.Open();
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("orderr", textBox1.Text);
                        command.Parameters.AddWithValue("@client", textBox2.Text);
                        command.Parameters.AddWithValue("@status", comboBox1.SelectedIndex + 1);
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
                MessageBox.Show("Данные успешно изменены");
            }
            else
            {
                MessageBox.Show("Вы не можете изменять заявки других работников");
            }
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
                    command.Parameters.AddWithValue("@userr",login);             
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
            MessageBox.Show("Данные успешно внесены");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString() == login)
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
                MessageBox.Show("Данные успешно удалены");
            }
            else
            {
                MessageBox.Show("Вы не можете удалять заявки других работников");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Personal personal = new Personal();
            personal.loginn = login;
            personal.Show();
        }

      
    }
}

