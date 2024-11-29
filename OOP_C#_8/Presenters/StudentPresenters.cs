using System;
using OOP_C__8.Views;
using OOP_C__8.Models;
using System.Diagnostics;

namespace OOP_C__8.Presenters
{
    internal class StudentPresenters
    {
        private readonly IStudentView view;
        private readonly StudentModel model;

        public StudentPresenters(IStudentView view, StudentModel model)
        {
            this.view = view;
            this.model = model;

            view.AddStudent += OnAddStudent;
            view.UpdateStudent += OnUpdateStudent;
            view.DeleteStudent += OnDeleteStudent;
            view.ViewStudents += OnViewStudents;
        }

        private void OnAddStudent(object sender, EventArgs e)
        {
            model.CreateStudent(view.RecordBook, view.StudentName,view.Group, view.Department, view.Specification, view.DateOfAdmission);
            OnViewStudents(sender, e);
        }

        private void OnUpdateStudent(object sender, EventArgs e)
        {
            model.UpdateStudent( view.StudentName, view.Group, view.Department, view.Specification, view.DateOfAdmission, view.RecordBook);
            OnViewStudents(sender, e);
        }

        private void OnDeleteStudent(object sender, EventArgs e)
        {
            model.DeleteStudent(view.RecordBook);
            OnViewStudents(sender, e);
        }

        private void OnViewStudents(object sender, EventArgs e)
        {
            Debug.WriteLine($"Просмотр студентов...");
            view.DisplayStudents(model.ReadStudents());
        }

    }
}
