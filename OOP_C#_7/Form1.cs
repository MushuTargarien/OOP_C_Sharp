using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_C__7
{
    public partial class Form1 : Form
    {
       BindingList<Student> Students = new BindingList<Student>();
        public Form1()
        {
            InitializeComponent();
            ReadFromFile();
            setupDGV();
            dataGridView1.ClearSelection();

        }

        public void ReadFromFile()
        {
            string filepath = "students.txt ";

            if (!File.Exists(filepath))
            {
                MessageBox.Show("Файл не найден");
            }

            try
            {
                foreach (var line in File.ReadLines(filepath))
                {
                    // Пропуск пустых строк
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Разделение строки на части
                    var parts = line.Split(',');
                    if (parts.Length != 6)
                    {
                        Console.WriteLine($"Неверный формат строки: {line}");
                        continue;
                    }

                    // Преобразование данных в объект Student
                    try
                    {
                        var student = new Student(
                            FullName: parts[0].Trim(),
                            RecordBook: int.Parse(parts[1].Trim()),
                            Group: parts[2].Trim(),
                            Department: parts[3].Trim(),
                            Specification: parts[4].Trim(),
                            DateOfAdmission: parts[5].Trim()
                        );

                        Students.Add(student);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Ошибка преобразования данных: {e.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
            }

        }

        private void setupDGV()
        {

            dataGridView1.DataSource = Students;

            dataGridView1.Columns[0].HeaderText = "ФИО";
            dataGridView1.Columns[1].HeaderText = "Номер студенческого";
            dataGridView1.Columns[2].HeaderText = "Группа";
            dataGridView1.Columns[3].HeaderText = "Институт";
            dataGridView1.Columns[4].HeaderText = "Направление";
            dataGridView1.Columns[5].HeaderText = "Дата поступления";

            dataGridView1.Columns[0].Width = 175; 
            dataGridView1.Columns[1].Width = 100; 
            dataGridView1.Columns[2].Width = 65; 
            dataGridView1.Columns[3].Width = 232;
            dataGridView1.Columns[4].Width = 160;
            dataGridView1.Columns[5].Width = 100;
        }

        class Student
        {
            public string fullName { get; set; }
            public int recordBook { get; set; }
            public string group { get; set; }
            public string department { get; set; }
            public string specification { get; set; }
            public string dateOfAdmission { get; set; }
           

            public Student(string FullName, int RecordBook, string Group, string Department, string Specification, string DateOfAdmission)
            {
                recordBook = RecordBook;
                fullName = FullName;
                department = Department;
                group = Group;
                specification = Specification;
                dateOfAdmission = DateOfAdmission;
    
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            string[] GUM = { "Русская филология", "Зарубежная филология", "Лингвистика", "Теория и практика перевода", "Литературоведение" };
            string[] Economy = { "Мировая экономика", "Государственное и муниципальное управление", "Международное право", "Уголовное право", "Маркетинг" };
            string[] Estestv = { "Молекулярная биология", "Генетика", "Органическая химия", "Физическая химия", "Биохимия" };
            string[] Inost = { "Английская филология", "Германская филология", "Тенория и практика перевода", "Социолингвистика", "Технический перевод" };
            string[] ITNIT = { "Программная инженерия", "Наноматериалы", "Искусственный интеллект", "Разработка программного обеспечения", "Интеллектуальные системы управления" };
            string[] Medecine = { "Общая медицина", "Стоматология", "Фармацевтика", "Психиатрия", "Медицинская биохимия" };

            comboBox2.Text="";
            comboBox2.Items.Clear();

            switch(comboBox1.SelectedIndex)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean isExist = false;
            foreach (Student s in Students)
            {
                if (s.recordBook == int.Parse(textBox1.Text))
                {
                    MessageBox.Show("Данный номер зачётки уже существует, проверьте данные");
                    isExist = true;
                    break;
                } 
            }

            if (isExist == false)
            {
                Students.Add(new Student(textBox2.Text, int.Parse(textBox1.Text), textBox3.Text, comboBox1.Text, comboBox2.Text, dateTimePicker1.Text));
                StreamWriter strWrite = new StreamWriter("students.txt",true);
                strWrite.WriteLine($"{textBox2.Text},{textBox1.Text},{textBox3.Text},{comboBox1.Text},{comboBox2.Text},{dateTimePicker1.Text}");
                strWrite.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {      
                Student newStud = new Student(textBox2.Text, int.Parse(textBox1.Text), textBox3.Text, comboBox1.Text, comboBox2.Text, dateTimePicker1.Text);
                Students[dataGridView1.CurrentCell.RowIndex] = newStud;

                string[] allLines = File.ReadAllLines("students.txt");
                allLines[dataGridView1.CurrentCell.RowIndex ] = ($"{textBox2.Text},{textBox1.Text},{textBox3.Text},{comboBox1.Text},{comboBox2.Text},{dateTimePicker1.Text}");
                File.WriteAllLines("students.txt", allLines);        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string item = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex].ToString();
            if (item != null && dataGridView1.CurrentCell.ColumnIndex != 0)
            {
                int RecBook = Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value);
                var Stud = Students.Where(w => w.recordBook == RecBook).ToList();

                foreach (Student s in Stud)
                {
                    Students.Remove(s);
                }


                var allLines = File.ReadAllLines("students.txt").ToList();
                allLines.RemoveAt(dataGridView1.CurrentCell.RowIndex );
                File.WriteAllLines("students.txt", allLines);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            textBox2.Text = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            textBox3.Text = dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            comboBox1.Text = dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            comboBox2.Text = dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            dateTimePicker1.Text = dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;    
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            textBox2.Text = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            textBox3.Text = dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            comboBox1.Text = dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            comboBox2.Text = dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            dateTimePicker1.Text = dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.TextLength < 8 || int.Parse(textBox1.Text) <= 0)
            {
                textBox1.Clear();
                MessageBox.Show("Номер зачётки введён не правильно, попробуйте ещё раз");
            }
        }
    }
}
