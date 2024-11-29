using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_C__8.Views
{
    public interface IStudentView
    {
        int RecordBook {  get; set; }
        string StudentName { get; set; }
        string Group { get; set; }
        string Department {  get; set; }
        string Specification {  get; set; }
        string DateOfAdmission {  get; set; }
        void DisplayStudents(DataTable students);
        event EventHandler AddStudent;
        event EventHandler UpdateStudent;
        event EventHandler DeleteStudent;
        event EventHandler ViewStudents;
    }
}
