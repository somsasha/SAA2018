using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SAA2018.Models;

namespace SAA2018.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentsGridPage.xaml
    /// </summary>
    public partial class StudentsGridPage : Page
    {
        ObservableCollection<Student> Students;
        DBL dbl;
        public StudentsGridPage(DBL d)
        {
            InitializeComponent();
            dbl = d;
            Students = new ObservableCollection<Student>();
            if (dbl.accounttype == DBL.AccountType.Dean)
            {
                foreach (Student s in dbl.GiveStudents())
                    if (s.University == dbl.LoggedPerson.University && s.Group.Faculty == ((Dean)dbl.LoggedPerson).Faculty)
                        Students.Add(s);
            }
            else if(dbl.accounttype == DBL.AccountType.Teacher)
            {
                foreach (Student s in dbl.GiveStudents())
                    if (s.University == dbl.LoggedPerson.University)
                        Students.Add(s);
                AddStudent.Visibility = Visibility.Hidden;
                RemoveStudent.Visibility = Visibility.Hidden;
                EditStudent.Visibility = Visibility.Hidden;
            }
            else if (dbl.accounttype == DBL.AccountType.Rector)
            {
                foreach (Student s in dbl.GiveStudents())
                    if (s.University == dbl.LoggedPerson.University)
                        Students.Add(s);
            }
            StudentsGrid.ItemsSource = Students;
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            Student NewStudent = null;
            StudentEdite_Event(NewStudent);
        }

        private void RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            int SelectedStudentId;
            SelectedStudentId = StudentsGrid.SelectedIndex;
            Student SelectedStudent;
            SelectedStudent = (Student)StudentsGrid.Items[SelectedStudentId];
            if(SelectedStudent.Account!=null)
                dbl.RemoveAccount(SelectedStudent.Account);
            dbl.RemovePerson(SelectedStudent);
            Students.Remove(SelectedStudent);
        }

        private void EditStudent_Click(object sender, RoutedEventArgs e)
        {
            int SelectedStudentId;
            SelectedStudentId = StudentsGrid.SelectedIndex;
            Student SelectedStudent;
            SelectedStudent = (Student)StudentsGrid.Items[SelectedStudentId];
            StudentEdite_Event(SelectedStudent);
        }

        public delegate void StudentDataEventHandler(Student student);
        public event StudentDataEventHandler StudentEdite_Event;

        private void StudentsGrid_Selected(object sender, RoutedEventArgs e)
        {
            if (StudentsGrid.SelectedItem != null)
            {
                RemoveStudent.IsEnabled = true;
                EditStudent.IsEnabled = true;
            }
            else
            {
                RemoveStudent.IsEnabled = false;
                EditStudent.IsEnabled = false;
            }
        }
    }
}