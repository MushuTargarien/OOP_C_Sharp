using System;
using System.Data;
using System.Windows.Forms;
using OOP_C__8.Views;
using OOP_C__8.Models;
using OOP_C__8.Presenters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace OOP_C__8
{
    public partial class Form1 : Form, IStudentView
    {
        public int RecordBook
        {
            get { return int.Parse(maskedTextBox2.Text); }
            set { maskedTextBox2.Text = value.ToString(); }
        }
        public string StudentName
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        public string Group
        {
            get { return maskedTextBox1.Text; }
            set { maskedTextBox1.Text = value; }
        }
        public string Department
        {
            get { return comboBox1.Text; }
            set { comboBox1.Text = value; }
        }
        public string Specification
        {
            get { return comboBox2.Text; }
            set { comboBox2.Text = value; }
        }
        public string DateOfAdmission
        {
            get { return dateTimePicker1.Text; }
            set { dateTimePicker1.Text = value; }
        }

        public event EventHandler AddStudent;
        public event EventHandler UpdateStudent;
        public event EventHandler DeleteStudent;
        public event EventHandler ViewStudents;

        public void DisplayStudents(DataTable students)
        {
            dataGridView1.DataSource = students;
        }
        public Form1()
        {
            InitializeComponent();

            var model = new StudentModel();
            var presenter = new StudentPresenters(this, model);
            btnAdd.Click += (s, e) => AddStudent?.Invoke(s, e);
            dataGridView1.DataSource = model.ReadStudents();
            dataGridView1.Columns[0].HeaderText = "Номер зачётки";
            dataGridView1.Columns[1].HeaderText = "ФИО студента";
            dataGridView1.Columns[2].HeaderText = "Номер группы";
            dataGridView1.Columns[3].HeaderText = "Институт";
            dataGridView1.Columns[4].HeaderText = "Направление";
            dataGridView1.Columns[5].HeaderText = "Дата поступления";


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                int recordBook = Convert.ToInt32(selectedRow.Cells["recordBook"].Value);
                RecordBook = recordBook;
                DeleteStudent?.Invoke(sender, e);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите студента для удаления.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                int recordBook = Convert.ToInt32(selectedRow.Cells["recordBook"].Value);
                UpdateStudent?.Invoke(sender, e);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите студента для обновления.");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            maskedTextBox2.Text = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            textBox2.Text = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            maskedTextBox1.Text = dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            comboBox1.Text = dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            comboBox2.Text = dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            dateTimePicker1.Text = dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] GUM = { "Русская филология", "Зарубежная филология", "Лингвистика", "Теория и практика перевода", "Литературоведение" };
            string[] Economy = { "Мировая экономика", "Государственное и муниципальное управление", "Международное право", "Уголовное право", "Маркетинг" };
            string[] Estestv = { "Молекулярная биология", "Генетика", "Органическая химия", "Физическая химия", "Биохимия" };
            string[] Inost = { "Английская филология", "Германская филология", "Тенория и практика перевода", "Социолингвистика", "Технический перевод" };
            string[] ITNIT = { "Программная инженерия", "Наноматериалы", "Искусственный интеллект", "Разработка программного обеспечения", "Интеллектуальные системы управления" };
            string[] Medecine = { "Общая медицина", "Стоматология", "Фармацевтика", "Психиатрия", "Медицинская биохимия" };

            comboBox2.Text = "";
            comboBox2.Items.Clear();

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    comboBox2.Items.AddRange(GUM);
                    break;
                case 1:
                    comboBox2.Items.AddRange(Economy);
                    break;
                case 2:
                    comboBox2.Items.AddRange(Estestv);
                    break;
                case 3:
                    comboBox2.Items.AddRange(Inost);
                    break;
                case 4:
                    comboBox2.Items.AddRange(ITNIT);
                    break;
                case 5:
                    comboBox2.Items.AddRange(Medecine);
                    break;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            maskedTextBox2.Clear();
            textBox2.Clear();
            maskedTextBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
