using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_C__8.Models
{
    internal class StudentModel
    {
        private string connectionString = "Data Source=Mushu;Initial Catalog = Students; Integrated Security = True;";

        public void CreateStudent( int recordBook, string name, string group, string department, string specification, string dateOfAdmission)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT COUNT(*) FROM Students WHERE recordBook = @recordBook;";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@recordBook", recordBook);
                con.Open();
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Студент с таким номером зачётки уже существует.");
                }
                else
                { 
                    string query = "INSERT INTO Students (recordBook,name, groupp, department, specification, dateOfAdmission) VALUES (@recordBook, @name, @group, @department, @specification, @dateOfAdmission);";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@recordBook", recordBook);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@department", department);
                    cmd.Parameters.AddWithValue("@specification", specification);
                    cmd.Parameters.AddWithValue("@dateOfAdmission", dateOfAdmission);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ReadStudents()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students;";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public void UpdateStudent( string name, string group, string department, string specification, string dateOfAdmission, int RecordBook)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            { 
                string query = "UPDATE  Students SET name =@name, groupp = @group, department = @department, specification = @specification, dateOfAdmission = @dateOfAdmission WHERE RecordBook = @RecordBook;";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@group", group);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.Parameters.AddWithValue("@specification", specification);
                cmd.Parameters.AddWithValue("@dateOfAdmission", dateOfAdmission);
                cmd.Parameters.AddWithValue("@RecordBook", RecordBook);
                cmd.ExecuteNonQuery();
                
            }
        }

        public void DeleteStudent(int RecordBook)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE RecordBook = @RecordBook;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RecordBook", RecordBook);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
