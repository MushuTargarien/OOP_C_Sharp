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
using static Raven.Database.Indexing.IndexingWorkStats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOP_C__10
{
    public partial class Personal : Form
    {
        string connectionString = "Data Source=MUSHU;Initial Catalog=orders;Integrated Security=True;";
        public string loginn;


        public Personal()
        {
            InitializeComponent();
        }

        public DataTable ReadOrders()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT orderr.id_order, orderr.orderr, statuses.status, users.fio, orderr.client, orderr.datee, users.login, orderr.date_finish
                     FROM orderr 
                     INNER JOIN statuses ON orderr.status = statuses.Id_status 
                     INNER JOIN users ON orderr.userr = users.login
                     WHERE users.login = @login ;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    if (loginn == null)
                    {
                        throw new ArgumentNullException(nameof(loginn), "Login parameter cannot be null.");
                    }
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@login",  loginn);

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


    private void count()
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
                            label1.Text = "Принято: " + reader["Accepted"].ToString();
                            label2.Text = "В процессе: " + reader["InProgress"].ToString();
                            label3.Text = "Завершено: " + reader["Completed"].ToString();
                        }

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            order order = new order();
            order.login = loginn;
            order.Show();
        }

     

        private void Personal_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource =  ReadOrders();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Заявка";
            dataGridView1.Columns[2].HeaderText = "Статус";
            dataGridView1.Columns[3].HeaderText = "ФИО сотрудника";
            dataGridView1.Columns[4].HeaderText = "ФИО клиента";
            dataGridView1.Columns[5].HeaderText = "Дата";
            dataGridView1.Columns[7].HeaderText = "Дата сдачи";
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            count();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable = ReadOrders();
            switch (comboBox1.SelectedIndex) {
                case 0:
                    dataGridView1.DataSource =  ReadOrders();
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
}
