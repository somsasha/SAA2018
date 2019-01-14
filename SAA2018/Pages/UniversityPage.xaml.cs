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
    /// Логика взаимодействия для UniversityPage.xaml
    /// </summary>
    public partial class UniversityPage : Page
    {
        DBL dbl;
        University university;
        public UniversityPage(University u, DBL d)
        {
            InitializeComponent();
            dbl = d;
            university = u;
            if (university == null)
            {
                university = new University();
                NewUniversity();
            }
            else
                EditUniversity();
        }

        private void EditUniversity()
        {
            Name_Field.Text = university.Name;
            SaveUniversity_Button.Visibility = Visibility.Visible;
        }

        private void NewUniversity()
        {
            AddUniversity_Button.Visibility = Visibility.Visible;
        }

        private void SaveUniversity_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Name_Field.Text.Length > 0)
            {
                NameError.Visibility = Visibility.Hidden;
                university.Name = Name_Field.Text;
                dbl.ChangeObject(university);
                Success.Visibility = Visibility.Visible;
            }
            else
            {
                Success.Visibility = Visibility.Hidden;
                NameError.Visibility = Visibility.Visible;
            }
        }

        private void AddUniversity_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Name_Field.Text.Length > 0)
            {
                NameError.Visibility = Visibility.Hidden;
                university.Name = Name_Field.Text;
                dbl.AddUniversity(university);
                UniversityAdded_Event(university);
            }
            else
            {
                NameError.Visibility = Visibility.Visible;
            }
        }

        public delegate void UniversityAddedEventHandler(University u);
        public event UniversityAddedEventHandler UniversityAdded_Event;
    }
}
