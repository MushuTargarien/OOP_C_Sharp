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
    public partial class regisration : Form
    {
        public regisration()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=MUSHU;Initial Catalog=orders;Integrated Security=True;";

        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            login log = new login();
            log.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox2.Text == textBox3.Text)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "Insert into users (login, pass, fio, staff) values (@login, @pass, @fio, @staff);";
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            con.Open();
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@login", textBox1.Text);
                            command.Parameters.AddWithValue("@pass", textBox2.Text);
                            command.Parameters.AddWithValue("@fio", textBox4.Text);
                            command.Parameters.AddWithValue("@staff", textBox3.Text);
                            command.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Данные успешно сохранены");


                        }
                    }
                    this.Hide();
                    order f2 = new order();
                    f2.Text = textBox4.Text;
                    f2.login = textBox1.Text;
                    f2.Show();
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Введите логин");
                    }
                    if (textBox2.Text != textBox3.Text && textBox2.Text != "" && textBox3.Text != "")
                    {
                        MessageBox.Show("Пароли не совпадают");
                    }
                    if ( textBox2.Text == "" && textBox3.Text == "")
                    {
                        MessageBox.Show("Введите Пароль");
                    }

                    if (textBox4.Text == "")
                    {
                        MessageBox.Show("Введите ФИО");
                    }
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0'; // Убираем password char
            }
            else
            {
                textBox2.PasswordChar = '*'; // Восстанавливаем password char
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox3.PasswordChar = '\0'; // Убираем password char
            }
            else
            {
                textBox3.PasswordChar = '*'; // Восстанавливаем password char
            }
        }
    }
}
