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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOP_C__10
{
    public partial class PersonalAdmin : Form
    {
        public string login;
        string connectionString = "Data Source=MUSHU;Initial Catalog=orders;Integrated Security=True;";
        public PersonalAdmin()
        {
            InitializeComponent();
        }

        public DataTable FindAllOrders()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT orderr.id_order, orderr.orderr, statuses.status, users.fio, orderr.client, orderr.datee, users.login, orderr.date_finish
                     FROM orderr 
                     INNER JOIN statuses ON orderr.status = statuses.Id_status 
                     INNER JOIN users ON orderr.userr = users.login;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    if (login == null)
                    {
                        throw new ArgumentNullException(nameof(login), "Login parameter cannot be null.");
                    }
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@login", login);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    con.Open();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "status")
            {

                switch (e.Value)
                {
                    case "Принят":
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                        break;
                    case "В процессе":
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                        break;
                    case "Завершён":
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        break;
                }
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 7)
            {
                DateTime dateFinish;
                if (DateTime.TryParse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString(), out dateFinish))
                {
                    if (dateFinish < DateTime.Today)
                    {
                        foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
                        {
                            cell.Style.BackColor = Color.Red;
                        }
                    }
                }
            }
        }

        private void PersonalAdmin_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "ordersDataSet.users". При необходимости она может быть перемещена или удалена.

            this.usersTableAdapter.Fill(this.ordersDataSet.users);
            dataGridView1.DataSource = FindAllOrders();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Заявка";
            dataGridView1.Columns[2].HeaderText = "Статус";
            dataGridView1.Columns[3].HeaderText = "ФИО сотрудника";
            dataGridView1.Columns[4].HeaderText = "ФИО клиента";
            dataGridView1.Columns[5].HeaderText = "Дата";
            dataGridView1.Columns[7].HeaderText = "Дата сдачи";
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }


        private DataTable findOrdersbyUser(string userLogin)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT orderr.id_order, orderr.orderr, statuses.status, users.fio, orderr.client, orderr.datee, users.login, orderr.date_finish
                     FROM orderr 
                     INNER JOIN statuses ON orderr.status = statuses.Id_status 
                     INNER JOIN users ON orderr.userr = users.login
                     Where login = @login;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    if (login == null)
                    {
                        throw new ArgumentNullException(nameof(login), "Login parameter cannot be null.");
                    }
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@login", userLogin);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    con.Open();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        private void count( string loginn)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT  
                                COUNT(CASE WHEN orderr.status = '1' THEN 1 END) AS Accepted,
                                COUNT(CASE WHEN orderr.status = '2' THEN 1 END) AS InProgress,
                                COUNT(CASE WHEN orderr.status = '3' THEN 1 END) AS Completed
                                FROM orderr 
                                INNER JOIN users ON orderr.userr = users.login
                                WHERE users.login = @login;";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@login", loginn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            label4.Text = "Принято: " + reader["Accepted"].ToString();
                            label5.Text = "В процессе: " + reader["InProgress"].ToString();
                            label6.Text = "Завершено: " + reader["Completed"].ToString();
                        }

                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.DataSource = FindAllOrders();
            }
            else
            {
                dataGridView1.DataSource = findOrdersbyUser(comboBox1.SelectedValue.ToString());
                count(comboBox1.SelectedValue.ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                DataTable dataTable = FindAllOrders();
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        dataGridView1.DataSource = FindAllOrders();
                        break;
                    case 1:
                        DataView dataView = new DataView(dataTable);
                        dataView.RowFilter = $"status = 'Принят'";
                        dataGridView1.DataSource = dataView;
                        break;
                    case 2:
                        DataView dataView2 = new DataView(dataTable);
                        dataView2.RowFilter = $"status = 'В процессе'";
                        dataGridView1.DataSource = dataView2;
                        break;
                    case 3:
                        DataView dataView3 = new DataView(dataTable);
                        dataView3.RowFilter = $"status = 'Завершён'";
                        dataGridView1.DataSource = dataView3;
                        break;
                }
            }

            else
            {
                DataTable dataTable = findOrdersbyUser(comboBox1.SelectedValue.ToString());
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        dataGridView1.DataSource = findOrdersbyUser(comboBox1.SelectedValue.ToString());
                        break;
                    case 1:
                        DataView dataView = new DataView(dataTable);
                        dataView.RowFilter = $"status = 'Принят'";
                        dataGridView1.DataSource = dataView;
                        break;
                    case 2:
                        DataView dataView2 = new DataView(dataTable);
                        dataView2.RowFilter = $"status = 'В процессе'";
                        dataGridView1.DataSource = dataView2;
                        break;
                    case 3:
                        DataView dataView3 = new DataView(dataTable);
                        dataView3.RowFilter = $"status = 'Завершён'";
                        dataGridView1.DataSource = dataView3;
                        break;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            orderAdmin orderAdmin = new orderAdmin();
            orderAdmin.login = login;
            orderAdmin.Show();
        }
    }
}
