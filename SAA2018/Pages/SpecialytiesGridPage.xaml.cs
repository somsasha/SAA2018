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
    /// Логика взаимодействия для SpecialytiesGridPage.xaml
    /// </summary>
    public partial class SpecialytiesGridPage : Page
    {
        ObservableCollection<Specialty> Specialties;
        DBL dbl;
        public SpecialytiesGridPage(DBL d)
        {
            InitializeComponent();
            dbl = d;
            Specialties = new ObservableCollection<Specialty>();
            if (dbl.accounttype == DBL.AccountType.Dean)
            {
                foreach (Specialty s in dbl.GiveSpecialties())
                if (s.University == dbl.LoggedPerson.University && s.Faculty == ((Dean)dbl.LoggedPerson).Faculty)
                        Specialties.Add(s);
            }
            else
            {
                foreach (Specialty s in dbl.GiveSpecialties())
                    if (s.University == dbl.LoggedPerson.University)
                        Specialties.Add(s);
            }
            SpecialtiesGrid.ItemsSource = Specialties;
        }

        private void SpecialtiesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpecialtiesGrid.SelectedItem != null)
            {
                RemoveSpecialty.IsEnabled = true;
                EditSpecialty.IsEnabled = true;
            }
            else
            {
                RemoveSpecialty.IsEnabled = false;
                EditSpecialty.IsEnabled = false;
            }
        }

        private void AddSpecialty_Click(object sender, RoutedEventArgs e)
        {
            Specialty specialty = null;
            SpecialtyEdite_Event(specialty);
        }

        private void RemoveSpecialty_Click(object sender, RoutedEventArgs e)
        {
            int SelectedSpecialtyId;
            SelectedSpecialtyId = SpecialtiesGrid.SelectedIndex;
            Specialty SelectedSpecialty;
            SelectedSpecialty = (Specialty)SpecialtiesGrid.Items[SelectedSpecialtyId];
            dbl.RemoveSpecialty(SelectedSpecialty);
            Specialties.Remove(SelectedSpecialty);
        }

        private void EditSpecialty_Click(object sender, RoutedEventArgs e)
        {
            int SelectedSpecialtyId;
            SelectedSpecialtyId = SpecialtiesGrid.SelectedIndex;
            Specialty SelectedSpecialty;
            SelectedSpecialty = (Specialty)SpecialtiesGrid.Items[SelectedSpecialtyId];
            SpecialtyEdite_Event(SelectedSpecialty);
        }

        public delegate void SpecialtyDataEventHandler(Specialty specialty);
        public event SpecialtyDataEventHandler SpecialtyEdite_Event;
    }
}