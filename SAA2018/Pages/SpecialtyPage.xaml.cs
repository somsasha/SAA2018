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
    /// Логика взаимодействия для SpecialtyPage.xaml
    /// </summary>
    public partial class SpecialtyPage : Page
    {
        Specialty specialty;
        List<Faculty> faculties;
        DBL dbl;
        public SpecialtyPage(Specialty s, DBL d)
        {
            InitializeComponent();
            specialty = new Specialty();
            specialty = s;
            dbl = d;
            faculties = new List<Faculty>();
            foreach (Faculty faculty in dbl.GiveFaculties())
            {
                if (faculty.University == dbl.LoggedPerson.University)
                {
                    faculties.Add(faculty);
                    Faculty_Box.Items.Add(faculty.Name);
                }
            }
            if (specialty == null)
            {
                specialty = new Specialty();
                NewSpecialty();
            }
            else
                EditSpecialty();
        }

        private void EditSpecialty()
        {
            Name_Field.Text = specialty.Name;
            if(specialty.Faculty!=null)
                Faculty_Box.SelectedIndex = faculties.IndexOf(specialty.Faculty);
            SaveSpecialty_Button.Visibility = Visibility.Visible;
        }

        private void NewSpecialty()
        {
            AddSpecialty_Button.Visibility = Visibility.Visible;
        }

        private void Name_Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (Specialty s in dbl.GiveSpecialties())
            {
                if (s.Name == Name_Field.Text && specialty.Name != Name_Field.Text)
                {
                    Success.Visibility = Visibility.Hidden;
                    NameError.Visibility = Visibility.Visible;
                }
                else
                    NameError.Visibility = Visibility.Hidden;
            }
        }

        private void SaveSpecialty_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Name_Field.Text.Length > 0 && Faculty_Box.SelectedIndex != -1 && NameError.Visibility != Visibility.Visible)
            {
                NameError.Visibility = Visibility.Hidden;
                Success.Visibility = Visibility.Hidden;
                FacultyError.Visibility = Visibility.Hidden;
                specialty.Name = Name_Field.Text;
                specialty.Faculty = faculties[Faculty_Box.SelectedIndex];
                dbl.ChangeObject(specialty);
                Success.Visibility = Visibility.Visible;
            }
            else if (Name_Field.Text.Length > 0)
            {
                NameError.Visibility = Visibility.Hidden;
                Success.Visibility = Visibility.Hidden;
                FacultyError.Visibility = Visibility.Visible;
            }
            else
            {
                FacultyError.Visibility = Visibility.Hidden;
                Success.Visibility = Visibility.Hidden;
                NameError.Visibility = Visibility.Visible;
            }
        }

        private void AddSpecialty_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Name_Field.Text.Length > 0 && Faculty_Box.SelectedIndex != -1 && NameError.Visibility != Visibility.Visible)
            {
                NameError.Visibility = Visibility.Hidden;
                FacultyError.Visibility = Visibility.Hidden;
                specialty.Name = Name_Field.Text;
                specialty.University = dbl.LoggedPerson.University;
                specialty.Faculty = faculties[Faculty_Box.SelectedIndex];
                dbl.AddSpecialty(specialty);
                Added_Event();
            }
            else if (Name_Field.Text.Length > 0)
            {
                NameError.Visibility = Visibility.Hidden;
                FacultyError.Visibility = Visibility.Visible;
            }
            else
            {
                FacultyError.Visibility = Visibility.Hidden;
                NameError.Visibility = Visibility.Visible;
            }
        }

        public delegate void AddedEventHandler();
        public event AddedEventHandler Added_Event;
    }
}
