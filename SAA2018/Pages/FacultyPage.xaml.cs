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
using SAA2018.Models;

namespace SAA2018.Pages
{
    /// <summary>
    /// Логика взаимодействия для FacultyPage.xaml
    /// </summary>
    public partial class FacultyPage : Page
    {
        Faculty faculty;
        List<Dean> deans;
        DBL dbl;
        public FacultyPage(Faculty f, DBL d)
        {
            InitializeComponent();
            faculty = new Faculty();
            faculty = f;
            dbl = d;
            deans = new List<Dean>();
            foreach (Dean dean in dbl.GiveDeans())
            {
                if (dean.University == dbl.LoggedPerson.University)
                {
                    deans.Add(dean);
                    Dean_Box.Items.Add(dean.SurName + " " + dean.Name + " " + dean.MidleName);
                }
            }
            if (faculty == null)
            {
                faculty = new Faculty();
                NewFaculty();
            }
            else
                EditFaculty();
        }

        private void EditFaculty()
        {
            Name_Field.Text = faculty.Name;
            if(faculty.Deans.Count>0)
                Dean_Box.SelectedIndex = deans.IndexOf(faculty.Deans.First());
            SaveFaculty_Button.Visibility = Visibility.Visible;
        }

        private void NewFaculty()
        {
            AddFaculty_Button.Visibility = Visibility.Visible;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Name_Field.Text.Length > 0 && Dean_Box.SelectedIndex != -1 && NameError.Visibility != Visibility.Visible)
            {
                NameError.Visibility = Visibility.Hidden;
                DeanError.Visibility = Visibility.Hidden;
                faculty.Name = Name_Field.Text;
                faculty.University = dbl.LoggedPerson.University;
                faculty.Deans.Add(deans[Dean_Box.SelectedIndex]);
                dbl.AddFaculty(faculty);
                Added_Event();
            }
            else if (Name_Field.Text.Length > 0 && NameError.Visibility != Visibility.Visible)
            {
                NameError.Visibility = Visibility.Hidden;
                DeanError.Visibility = Visibility.Hidden;
                faculty.Name = Name_Field.Text;
                faculty.University = dbl.LoggedPerson.University;
                dbl.AddFaculty(faculty);
                Added_Event();
            }
            else if (Name_Field.Text.Length > 0)
            {
                NameError.Visibility = Visibility.Hidden;
                DeanError.Visibility = Visibility.Visible;
            }
            else
            {
                DeanError.Visibility = Visibility.Hidden;
                NameError.Visibility = Visibility.Visible;
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Name_Field.Text.Length > 0 && Dean_Box.SelectedIndex != -1 && NameError.Visibility != Visibility.Visible)
            {
                NameError.Visibility = Visibility.Hidden;
                Success.Visibility = Visibility.Hidden;
                DeanError.Visibility = Visibility.Hidden;
                faculty.Name = Name_Field.Text;
                faculty.Deans.Clear();
                faculty.Deans.Add(deans[Dean_Box.SelectedIndex]);
                dbl.ChangeObject(faculty);
                Success.Visibility = Visibility.Visible;
            }
            else if (Name_Field.Text.Length > 0)
            {
                NameError.Visibility = Visibility.Hidden;
                Success.Visibility = Visibility.Hidden;
                DeanError.Visibility = Visibility.Visible;
            }
            else
            {
                DeanError.Visibility = Visibility.Hidden;
                Success.Visibility = Visibility.Hidden;
                NameError.Visibility = Visibility.Visible;
            }
        }

        public delegate void AddedEventHandler();
        public event AddedEventHandler Added_Event;

        private void Name_Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (Faculty f in dbl.GiveFaculties())
            {
                if (f.Name == Name_Field.Text && faculty.Name != Name_Field.Text)
                {
                    Success.Visibility = Visibility.Hidden;
                    NameError.Visibility = Visibility.Visible;
                }
                else
                    NameError.Visibility = Visibility.Hidden;
            }
        }
    }
}