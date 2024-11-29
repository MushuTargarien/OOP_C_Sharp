using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOP_C__10
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=MUSHU;Initial Catalog=orders;Integrated Security=True;";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "select * from users where login = @login and pass = @pass";
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            con.Open();
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@login", textBox1.Text);
                            command.Parameters.AddWithValue("@pass", textBox2.Text);
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    if (reader["staff"].ToString() == "администратор")
                                    {
                                        this.Hide();
                                        orderAdmin f2a = new orderAdmin();
                                        f2a.login = reader["login"].ToString();
                                        f2a.Show();
                                    }
                                    else
                                    {
                                        this.Hide();
                                        order f2 = new order();                                      
                                        f2.login = reader["login"].ToString();
                                        f2.Show();
                                        

                                    }
                                }

                            }
                            else MessageBox.Show("Данные не найдены :(");
                        }
                    }
                }
            } catch (Exception ex)
            { 
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            regisration reg = new regisration();
            reg.Show();
        }
    }
}
