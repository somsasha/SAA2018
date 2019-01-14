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
    /// Логика взаимодействия для FacultiesGridPage.xaml
    /// </summary>
    public partial class FacultiesGridPage : Page
    {
        ObservableCollection<Faculty> Faculties;
        DBL dbl;
        public FacultiesGridPage(DBL d)
        {
            InitializeComponent();
            dbl = d;
            Faculties = new ObservableCollection<Faculty>();
            foreach (Faculty f in dbl.GiveFaculties())
                if (f.University == dbl.LoggedPerson.University)
                    Faculties.Add(f);
            FacultiesGrid.ItemsSource = Faculties;
        }

        private void FacultiesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FacultiesGrid.SelectedItem != null)
            {
                RemoveFaculty.IsEnabled = true;
                EditFaculty.IsEnabled = true;
            }
            else
            {
                RemoveFaculty.IsEnabled = false;
                EditFaculty.IsEnabled = false;
            }
        }

        private void AddFaculty_Click(object sender, RoutedEventArgs e)
        {
            Faculty faculty = null;
            FacultyEdite_Event(faculty);
        }

        private void RemoveFaculty_Click(object sender, RoutedEventArgs e)
        {
            int SelectedFacultyId;
            SelectedFacultyId = FacultiesGrid.SelectedIndex;
            Faculty SelectedFaculty;
            SelectedFaculty = (Faculty)FacultiesGrid.Items[SelectedFacultyId];
            dbl.RemoveFaculty(SelectedFaculty);
            Faculties.Remove(SelectedFaculty);
        }

        private void EditFaculty_Click(object sender, RoutedEventArgs e)
        {
            int SelectedFacultyId;
            SelectedFacultyId = FacultiesGrid.SelectedIndex;
            Faculty SelectedFaculty;
            SelectedFaculty = (Faculty)FacultiesGrid.Items[SelectedFacultyId];
            FacultyEdite_Event(SelectedFaculty);
        }

        public delegate void FacultyDataEventHandler(Faculty faculty);
        public event FacultyDataEventHandler FacultyEdite_Event;
    }
}