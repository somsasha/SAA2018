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
    /// Логика взаимодействия для DeansGridPage.xaml
    /// </summary>
    public partial class DeansGridPage : Page
    {
        ObservableCollection<Dean> Deans;
        DBL dbl;
        public DeansGridPage(DBL d)
        {
            InitializeComponent();
            dbl = d;
            Deans = new ObservableCollection<Dean>();
            foreach (Dean dean in dbl.GiveDeans())
                if (dean.University == dbl.LoggedPerson.University)
                    Deans.Add(dean);
            DeansGrid.ItemsSource = Deans;
        }

        private void DeansGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DeansGrid.SelectedItem != null)
            {
                RemoveDean.IsEnabled = true;
                EditDean.IsEnabled = true;
            }
            else
            {
                RemoveDean.IsEnabled = false;
                EditDean.IsEnabled = false;
            }
        }

        private void AddDean_Click(object sender, RoutedEventArgs e)
        {
            Dean NewDean = null;
            DeanEdite_Event(NewDean);
        }

        private void RemoveDean_Click(object sender, RoutedEventArgs e)
        {
            int SelectedDeanId;
            SelectedDeanId = DeansGrid.SelectedIndex;
            Dean SelectedDean;
            SelectedDean = (Dean)DeansGrid.Items[SelectedDeanId];
            if (SelectedDean.Account != null)
                dbl.RemoveAccount(SelectedDean.Account);
            dbl.RemovePerson(SelectedDean);
            Deans.Remove(SelectedDean);
        }

        private void EditDean_Click(object sender, RoutedEventArgs e)
        {
            int SelectedDeanId;
            SelectedDeanId = DeansGrid.SelectedIndex;
            Dean SelectedDean;
            SelectedDean = (Dean)DeansGrid.Items[SelectedDeanId];
            DeanEdite_Event(SelectedDean);
        }

        public delegate void DeanDataEventHandler(Dean d);
        public event DeanDataEventHandler DeanEdite_Event;
    }
}
