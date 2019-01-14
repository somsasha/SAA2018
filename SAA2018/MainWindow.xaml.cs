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
using System.Data.Entity;
using System.Windows.Controls.Primitives;
using SAA2018.Models;
using SAA2018.Pages;
using System.Threading;

namespace SAA2018
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DBL dbl;
        private List<ListBoxItem> RectorsMenu = new List<ListBoxItem>() { (new ListBoxItem { Name = "University", Content = "Университет" }), (new ListBoxItem { Name = "Deans", Content = "Деканы" }), (new ListBoxItem { Name = "Faculties", Content = "Факультеты" }), (new ListBoxItem { Name = "Specialyties", Content = "Специальности" }), (new ListBoxItem { Name = "Groups", Content = "Группы" }), (new ListBoxItem { Name = "Students", Content = "Студенты" }), (new ListBoxItem { Name = "Teachers", Content = "Преподаватели" }), (new ListBoxItem { Name = "Profile", Content = "Личный кабинет" }) };
        private List<ListBoxItem> DeansMenu = new List<ListBoxItem>() { (new ListBoxItem { Name = "Specialyties", Content = "Специальности" }), (new ListBoxItem { Name = "Groups", Content = "Группы" }), (new ListBoxItem { Name = "Students", Content = "Студенты" }), (new ListBoxItem { Name = "Profile", Content = "Личный кабинет" }) };
        private List<ListBoxItem> TeachersMenu = new List<ListBoxItem>() { (new ListBoxItem { Name = "Students", Content = "Студенты" }), (new ListBoxItem { Name = "Profile", Content = "Личный кабинет" }) };
        private List<ListBoxItem> StudentsMenu = new List<ListBoxItem>() { (new ListBoxItem { Name = "Profile", Content = "Личный кабинет" }) };
        Object SelectedMenuItem;

        public MainWindow()
        {
            InitializeComponent();
            dbl = new DBL();
            LogIn();
        }

        private void LogIn()
        {
            App.Current.MainWindow.Hide();
            LoginWindow lw = new LoginWindow(dbl);
            lw.NewUniversity_Event += NewUniversity_Event;
            lw.ShowDialog();
            dbl.FindLoggedUser();
            ChangeMenu();
        }

        private void LogOff()
        {
            dbl.LogOff();
            LogIn();
        }

        private void ChangeMenu()
        {
            Menu.Items.Clear();
            SelectedMenuItem = null;
            switch (dbl.accounttype)
            {
                case DBL.AccountType.Rector:
                    foreach (ListBoxItem item in RectorsMenu)
                        Menu.Items.Add(item);
                    break;
                case DBL.AccountType.Dean:
                    foreach (ListBoxItem item in DeansMenu)
                        Menu.Items.Add(item);
                    break;
                case DBL.AccountType.Teacher:
                    foreach (ListBoxItem item in TeachersMenu)
                        Menu.Items.Add(item);
                    break;
                case DBL.AccountType.Student:
                    foreach (ListBoxItem item in StudentsMenu)
                        Menu.Items.Add(item);
                    break;
                default:
                    break;
            }
            if (dbl.LoggedPerson != null)
            {
                UniversityName.Text = dbl.LoggedPerson.University.Name;
                GridsFrame.Navigate(null);
            }
        }

        private void Page_Select(SelectionChangedEventArgs e)
        {
            ListBoxItem lbi = (ListBoxItem)Menu.SelectedItem;
            switch(lbi.Name)
            {
                case "Students":
                    StudentsGridPage studentsgridpage = new StudentsGridPage(dbl);
                    studentsgridpage.StudentEdite_Event += EditeStudent_Event;
                    GridsFrame.Navigate(studentsgridpage);
                    break; 
                case "Teachers":
                    TeachersGridPage teachersgridpage = new TeachersGridPage(dbl);
                    teachersgridpage.TeacherEdite_Event += EditeTeacher_Event;
                    GridsFrame.Navigate(teachersgridpage);
                    break;
                case "Deans":
                    DeansGridPage deansgridpage = new DeansGridPage(dbl);
                    deansgridpage.DeanEdite_Event += EditeDean_Event;
                    GridsFrame.Navigate(deansgridpage);
                    break;
                case "Faculties":
                    FacultiesGridPage facultiesgridpage = new FacultiesGridPage(dbl);
                    facultiesgridpage.FacultyEdite_Event += EditeFaculty_Event;
                    GridsFrame.Navigate(facultiesgridpage);
                    break;
                case "Specialyties":
                    SpecialytiesGridPage specialtiesgridpage = new SpecialytiesGridPage(dbl);
                    specialtiesgridpage.SpecialtyEdite_Event += EditeSpecialty_Event;
                    GridsFrame.Navigate(specialtiesgridpage);
                    break;
                case "Groups":
                    GroupsGridPage groupsgridpage = new GroupsGridPage(dbl);
                    groupsgridpage.GroupEdite_Event += EditeGroup_Event;
                    GridsFrame.Navigate(groupsgridpage);
                    break;
                case "University":
                    UniversityPage universitypage = new UniversityPage(dbl.LoggedPerson.University, dbl);
                    GridsFrame.Navigate(universitypage);
                    break;
                case "Profile":
                    GridsFrame.Navigate(new ProfilePage(dbl.LoggedPerson, dbl, ProfilePage.ProfilePageType.Edit));
                    break;
                default:
                    break;
            }
        }

        private void EditeStudent_Event(Student student)
        {
            if (student != null)
                GridsFrame.Navigate(new ProfilePage(student, dbl, ProfilePage.ProfilePageType.Edit));
            else
            {
                ProfilePage addprofilepage = new ProfilePage(student, dbl, ProfilePage.ProfilePageType.NewStudent);
                addprofilepage.Added_Event += Add_Event;
                GridsFrame.Navigate(addprofilepage);
            }
        }

        private void EditeDean_Event(Dean dean)
        {
            if (dean != null)
                GridsFrame.Navigate(new ProfilePage(dean, dbl, ProfilePage.ProfilePageType.Edit));
            else
            {
                ProfilePage addprofilepage = new ProfilePage(dean, dbl, ProfilePage.ProfilePageType.NewDean);
                addprofilepage.Added_Event += Add_Event;
                GridsFrame.Navigate(addprofilepage);
            }
        }

        private void EditeTeacher_Event(Teacher teacher)
        {
            if (teacher != null)
                GridsFrame.Navigate(new ProfilePage(teacher, dbl, ProfilePage.ProfilePageType.Edit));
            else
            {
                ProfilePage addprofilepage = new ProfilePage(teacher, dbl, ProfilePage.ProfilePageType.NewTeacher);
                addprofilepage.Added_Event += Add_Event;
                GridsFrame.Navigate(addprofilepage);
            }
        }

        private void EditeFaculty_Event(Faculty faculty)
        {
            FacultyPage facultypage =  new FacultyPage(faculty, dbl);
            facultypage.Added_Event += Add_Event;
            GridsFrame.Navigate(facultypage);
        }

        private void EditeSpecialty_Event(Specialty specialty)
        {
            SpecialtyPage specialtypage = new SpecialtyPage(specialty, dbl);
            specialtypage.Added_Event += Add_Event;
            GridsFrame.Navigate(specialtypage);
        }

        private void EditeGroup_Event(Group group)
        {
            GroupPage grouppage = new GroupPage(group, dbl);
            grouppage.Added_Event += Add_Event;
            GridsFrame.Navigate(grouppage);
        }

        private void Add_Event()
        {
            Menu.SelectedItem = null;
            Menu.SelectedItem = SelectedMenuItem;
        }

        private void UniversityAdd_Event(University university)
        {
            ProfilePage profilepage = new ProfilePage(university, dbl);
            profilepage.RectorAdded_Event += RectorAdd_Event;
            GridsFrame.Navigate(profilepage);
        }

        private void NewUniversity_Event()
        {
            University university = null;
            UniversityPage universitypage = new UniversityPage(university, dbl);
            universitypage.UniversityAdded_Event += UniversityAdd_Event;
            GridsFrame.Navigate(universitypage);
        }

        private void RectorAdd_Event(Rector rector)
        {
            dbl.LogIn(rector.Account.Login, rector.Account.Password);
            dbl.FindLoggedUser();
            ChangeMenu();
            GridsFrame.Navigate(null);
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Menu.SelectedItem != null)
            {
                Page_Select(e);
                UniversityName.Text = dbl.LoggedPerson.University.Name + " - " + ((ListBoxItem)Menu.SelectedItem).Content;
                SelectedMenuItem = Menu.SelectedItem;
            }
            Menu.SelectedItem = null;
        }

        private void LogOff_Button_Click(object sender, RoutedEventArgs e)
        {
            LogOff();
        }
    }
}