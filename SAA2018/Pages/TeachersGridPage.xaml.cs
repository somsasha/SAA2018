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
    /// Логика взаимодействия для TeachersGridPage.xaml
    /// </summary>
    public partial class TeachersGridPage : Page
    {
        ObservableCollection<Teacher> Teachers;
        DBL dbl;
        public TeachersGridPage(DBL d)
        {
            InitializeComponent();
            dbl = d;
            Teachers = new ObservableCollection<Teacher>();
            foreach (Teacher t in dbl.GiveTeachers())
                if (t.University == dbl.LoggedPerson.University)
                    Teachers.Add(t);
            TeachersGrid.ItemsSource = Teachers;
        }

        private void TeachersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeachersGrid.SelectedItem != null)
            {
                RemoveTeacher.IsEnabled = true;
                EditTeacher.IsEnabled = true;
            }
            else
            {
                RemoveTeacher.IsEnabled = false;
                EditTeacher.IsEnabled = false;
            }
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            Teacher NewTeacher = null;
            TeacherEdite_Event(NewTeacher);
        }

        private void RemoveTeacher_Click(object sender, RoutedEventArgs e)
        {
            int SelectedTeacherId;
            SelectedTeacherId = TeachersGrid.SelectedIndex;
            Teacher SelectedTeacher;
            SelectedTeacher = (Teacher)TeachersGrid.Items[SelectedTeacherId];
            if (SelectedTeacher.Account != null)
                dbl.RemoveAccount(SelectedTeacher.Account);
            dbl.RemovePerson(SelectedTeacher);
            Teachers.Remove(SelectedTeacher);
        }

        private void EditTeacher_Click(object sender, RoutedEventArgs e)
        {
            int SelectedTeacherId;
            SelectedTeacherId = TeachersGrid.SelectedIndex;
            Teacher SelectedTeacher;
            SelectedTeacher = (Teacher)TeachersGrid.Items[SelectedTeacherId];
            TeacherEdite_Event(SelectedTeacher);
        }

        public delegate void TeacherDataEventHandler(Teacher t);
        public event TeacherDataEventHandler TeacherEdite_Event;
    }
}
