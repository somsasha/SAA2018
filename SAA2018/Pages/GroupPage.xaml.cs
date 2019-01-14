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
    /// Логика взаимодействия для GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        Group group;
        List<Faculty> faculties;
        List<Specialty> SpecialtyBoxList;
        DBL dbl;
        public GroupPage(Group g, DBL d)
        {
            InitializeComponent();
            group = new Group();
            group = g;
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
            if (group == null)
            {
                group = new Group();
                NewGroup();
            }
            else
                EditGroup();
        }

        private void EditGroup()
        {
            Course_Field.Text = group.Course.ToString();
            Number_Field.Text = group.Number.ToString();
            if (group.Faculty != null)
                Faculty_Box.SelectedIndex = faculties.IndexOf(group.Faculty);
            if (group.Specialty != null)
                Specialty_Box.SelectedIndex = SpecialtyBoxList.IndexOf(group.Specialty);
            SaveGroup_Button.Visibility = Visibility.Visible;
        }

        private void NewGroup()
        {
            AddGroup_Button.Visibility = Visibility.Visible;
        }

        private void Group_Fields_TextChanged()
        {
            foreach (Group g in dbl.GiveGroups())
            {
                if (Specialty_Box.SelectedIndex != -1)
                {
                    if (g.Number.ToString() == Number_Field.Text && g.Specialty.Name == SpecialtyBoxList[Specialty_Box.SelectedIndex].Name && g != group)
                    {
                        Success.Visibility = Visibility.Hidden;
                        NumberError.Visibility = Visibility.Visible;
                    }
                    else
                        NumberError.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Number_Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            Group_Fields_TextChanged();
        }

        private void Course_Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            Group_Fields_TextChanged();
        }

        private void Faculty_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Faculty_Box.SelectedIndex != (-1))
            {
                SpecialtyBoxList = new List<Specialty>();
                Specialty_Box.Items.Clear();
                foreach (Specialty s in dbl.GiveSpecialties())
                    if (s.Faculty == faculties[Faculty_Box.SelectedIndex])
                        SpecialtyBoxList.Add(s);
                foreach (Specialty s in SpecialtyBoxList)
                    Specialty_Box.Items.Add(s.Name);
            }
            else
            {
                SpecialtyBoxList = new List<Specialty>();
                Specialty_Box.Items.Clear();
            }
        }

        private void AddGroup_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Course_Field.Text.Length > 0 && Number_Field.Text.Length > 0 && Faculty_Box.SelectedIndex != -1 && Specialty_Box.SelectedIndex != -1 && NumberError.Visibility != Visibility.Visible)
            {
                int course;
                int number;
                if (Int32.TryParse(Course_Field.Text, out course) && Int32.TryParse(Number_Field.Text, out number))
                {
                    NumberError.Visibility = Visibility.Hidden;
                    SpecialtyError.Visibility = Visibility.Hidden;
                    group.Number = number;
                    group.Course = course;
                    group.University = dbl.LoggedPerson.University;
                    group.Faculty = faculties[Faculty_Box.SelectedIndex];
                    group.Specialty = SpecialtyBoxList[Specialty_Box.SelectedIndex];
                    dbl.AddGroup(group);
                    Added_Event();
                }
                else
                {
                    NumberError.Visibility = Visibility.Visible;
                }
            }
            else if (Number_Field.Text.Length > 0 && Course_Field.Text.Length > 0)
            {
                NumberError.Visibility = Visibility.Hidden;
                SpecialtyError.Visibility = Visibility.Visible;
            }
            else
            {
                SpecialtyError.Visibility = Visibility.Hidden;
                NumberError.Visibility = Visibility.Visible;
            }
        }

        private void SaveGroup_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Course_Field.Text.Length > 0 && Number_Field.Text.Length > 0 && Faculty_Box.SelectedIndex != -1 && Specialty_Box.SelectedIndex != -1 && NumberError.Visibility != Visibility.Visible)
            {
                int course;
                int number;
                if (Int32.TryParse(Course_Field.Text, out course) && Int32.TryParse(Number_Field.Text, out number))
                {
                    NumberError.Visibility = Visibility.Hidden;
                    Success.Visibility = Visibility.Hidden;
                    SpecialtyError.Visibility = Visibility.Hidden;
                    group.Number = number;
                    group.Course = course;
                    group.Faculty = faculties[Faculty_Box.SelectedIndex];
                    group.Specialty = SpecialtyBoxList[Specialty_Box.SelectedIndex];
                    dbl.ChangeObject(group);
                    Success.Visibility = Visibility.Visible;
                }
                else
                {
                    NumberError.Visibility = Visibility.Visible;
                }
            }
            else if (Number_Field.Text.Length > 0 && Course_Field.Text.Length > 0)
            {
                NumberError.Visibility = Visibility.Hidden;
                Success.Visibility = Visibility.Hidden;
                SpecialtyError.Visibility = Visibility.Visible;
            }
            else
            {
                SpecialtyError.Visibility = Visibility.Hidden;
                Success.Visibility = Visibility.Hidden;
                NumberError.Visibility = Visibility.Visible;
            }
        }

        public delegate void AddedEventHandler();
        public event AddedEventHandler Added_Event;
    }
}
