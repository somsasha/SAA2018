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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        Person person;
        DBL dbl;
        List<Faculty> FacultyBoxList;
        List<Specialty> SpecialtyBoxList;
        List<Group> GroupBoxList;
        public enum ProfilePageType
        {
            Edit,
            NewStudent,
            NewTeacher,
            NewDean,
            NewRector
        }
        ProfilePageType pageType;
        public ProfilePage(Person p, DBL d, ProfilePageType pt)
        {
            InitializeComponent();
            person = p;
            dbl = d;
            pageType = pt;
            FacultyBoxList = new List<Faculty>(dbl.GiveFaculties());
            foreach(Faculty f in FacultyBoxList)
                if(f.University==dbl.LoggedPerson.University)
                    Facylty_Box.Items.Add(new ComboBoxItem { Content = f.Name});
            switch (pageType)
            {
                case ProfilePageType.Edit:
                    EditType();
                    break;
                case ProfilePageType.NewStudent:
                    AddType();
                    NewStudentType();
                    break;
                case ProfilePageType.NewTeacher:
                    AddType();
                    NewTeacherType();
                    break;
                case ProfilePageType.NewDean:
                    AddType();
                    NewDeanType();
                    break;
                default:
                    break;
            }
        }

        public ProfilePage(University u, DBL d)
        {
            InitializeComponent();
            dbl = d;
            person = new Rector();
            person.University = u;
            Add_Button.Visibility = Visibility.Visible;
            Login_Field.Visibility = Visibility.Visible;
            Passwod_Field.Visibility = Visibility.Visible;
            Email_Field.Visibility = Visibility.Visible;
            University_Field.Text = person.University.Name;
            pageType = ProfilePageType.NewRector;
        }

        private void AddType()
        {
            Add_Button.Visibility = Visibility.Visible;
            Login_Field.Visibility = Visibility.Visible;
            Passwod_Field.Visibility = Visibility.Visible;
            Email_Field.Visibility = Visibility.Visible;
            University_Field.Text = dbl.LoggedPerson.University.Name;
        }

        private void NewStudentType()
        {
            person = new Student();
            Facylty_Box.Visibility = Visibility.Visible;
            Specialty_Box.Visibility = Visibility.Visible;
            Group_Box.Visibility = Visibility.Visible;
            CardNumber_Field.Visibility = Visibility.Visible;

            var stu = dbl.GiveStudents().OrderByDescending(s => s.CardNumber);
            string NewStudentCardNumber = new List<Student>(stu)[0].CardNumber;
            int scn;
            if (Int32.TryParse(NewStudentCardNumber, out scn))
                NewStudentCardNumber = (scn + 1).ToString();
            else
                NewStudentCardNumber = null;

            CardNumber_Field.Text = NewStudentCardNumber;
            Login_Field.Text = NewStudentCardNumber;
            Passwod_Field.Text = NewStudentCardNumber;
        }

        private void NewTeacherType()
        {
            person = new Teacher();
        }

        private void NewDeanType()
        {
            Facylty_Box.Visibility = Visibility.Visible;
            person = new Dean();
        }

        private void EditType()
        {
            Save_Button.Visibility = Visibility.Visible;
            if (person == dbl.LoggedPerson)
            {
                Login_Field.Visibility = Visibility.Visible;
                Login_Field.Text = person.Account.Login;
                Passwod_Field.Visibility = Visibility.Visible;
                Passwod_Field.Text = person.Account.Password;
                Email_Field.Visibility = Visibility.Visible;
                Email_Field.Text = person.Account.Email;
            }

            SurName_Field.Text = person.SurName;
            Name_Field.Text = person.Name;
            MidleName_Field.Text = person.MidleName;
            Phone_Field.Text = person.PhoneNumber;
            University_Field.Text = person.University.Name;

            if (person is Student)
                StudentProfile();
            else if (person is Teacher)
                TeacherProfile();
            else if (person is Dean)
                DeanProfile();
            else if (person is Rector)
                RectorProfile();
        }

        private void StudentProfile()
        {
            Student student = (Student)person;
            Facylty_Box.Visibility = Visibility.Visible;
            Facylty_Box.SelectedIndex = FacultyBoxList.FindIndex(item => item == student.Group.Faculty);
            Specialty_Box.Visibility = Visibility.Visible;
            Specialty_Box.SelectedIndex = SpecialtyBoxList.FindIndex(item => item == student.Group.Specialty);
            Group_Box.Visibility = Visibility.Visible;
            Group_Box.SelectedIndex = GroupBoxList.FindIndex(item => item == student.Group);

            CardNumber_Field.Visibility = Visibility.Visible;
            CardNumber_Field.Text = student.CardNumber;
        }

        private void TeacherProfile()
        {
            Teacher teacher = (Teacher)person;
            Post_Field.Text = "Преподователь";
        }

        private void DeanProfile()
        {
            Dean dean = (Dean)person;
            Post_Field.Text = "Декан";
            Facylty_Box.Visibility = Visibility.Visible;
            Facylty_Box.SelectedIndex = FacultyBoxList.FindIndex(item => item == dean.Faculty);
        }

        private void RectorProfile()
        {
            Rector rector = (Rector)person;
            Post_Field.Text = "Ректор";
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            bool LoginExists = false;
            bool LoggedEdited = false;
            NullableError.Visibility = Visibility.Hidden;
            if (SurName_Field.Text.Length > 0 && Name_Field.Text.Length > 0 && MidleName_Field.Text.Length > 0 && Phone_Field.Text.Length > 0)
            {
                if (person.Account.Login != Login_Field.Text && person == dbl.LoggedPerson)
                {
                    foreach (Account a in dbl.GiveAccounts())
                        if (a.Login == Login_Field.Text)
                        { LoginExists = true; LoginError.Visibility = Visibility.Visible; }
                    if (!LoginExists && Login_Field.Text.Length > 0 && Passwod_Field.Text.Length > 0)
                        person.Account.Login = Login_Field.Text;
                    else if (!LoginExists)
                        NullableError.Visibility = Visibility.Visible;
                }
                if (!LoginExists && person == dbl.LoggedPerson)
                {
                    if (Email_Field.Text.Length > 0 && EmailError.Visibility == Visibility.Hidden)
                    {
                        person.Account.Password = Passwod_Field.Text;
                        person.Account.Email = Email_Field.Text;
                        LoggedEdited = true;
                    }
                    else
                    {
                        EmailError.Visibility = Visibility.Visible;
                    }
                }
                if (!LoginExists && EmailError.Visibility == Visibility.Hidden)
                {
                    person.SurName = SurName_Field.Text;
                    person.Name = Name_Field.Text;
                    person.MidleName = MidleName_Field.Text;
                    person.PhoneNumber = Phone_Field.Text;

                    dbl.ChangeObject(person);
                    if (LoggedEdited)
                        dbl.LoggedPerson = person;

                    if (person is Student)
                    {
                        EditeStudent();
                    }
                    else if (person is Teacher)
                    {
                        EditeTeacher();
                    }
                    else if (person is Dean)
                    {
                        EditeDean();
                    }
                }
            }
            else if(EmailError.Visibility != Visibility.Visible)
                NullableError.Visibility = Visibility.Visible;
        }

        private void EditeStudent()
        {
            Student student = (Student)person;
            bool CardNumberExists = false;
            foreach (Student s in dbl.GiveStudents())
                if ((s.CardNumber == CardNumber_Field.Text && student.CardNumber != CardNumber_Field.Text) || CardNumber_Field == null)
                { CardNumberExists = true; CardNumberError.Visibility = Visibility.Visible; }
            if(!CardNumberExists)
                student.CardNumber = CardNumber_Field.Text;
            if (Group_Box.SelectedIndex == -1)
                GroupNullableError.Visibility = Visibility.Visible;
            else
            {
                student.Group = GroupBoxList[Group_Box.SelectedIndex];
                dbl.ChangeObject(student);
                Success.Visibility = Visibility.Visible;
            }
        }

        private void EditeTeacher()
        {
            Teacher teacher = (Teacher)person;
            dbl.ChangeObject(teacher);
            Success.Visibility = Visibility.Visible;
        }

        private void EditeDean()
        {
            Dean dean = (Dean)person;
            foreach (Faculty f in dbl.GiveFaculties())
                if (f.Name == Facylty_Box.Text)
                {
                    dean.Faculty = f;
                    dbl.ChangeObject(dean);
                    Success.Visibility = Visibility.Visible;
                } else
                    NullableError.Visibility = Visibility.Visible;
        }

        private void Login_Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool LoginExists = false;
            foreach (Account a in dbl.GiveAccounts())
                if (a.Login == Login_Field.Text && dbl.LoggedPerson.Account.Login != Login_Field.Text)
                    LoginExists = true;
            if (LoginExists)
                LoginError.Visibility = Visibility.Visible;
            else
                LoginError.Visibility = Visibility.Hidden;
        }

        private void CardNumber_Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool CardNumberExists = false;
            foreach (Student s in dbl.GiveStudents())
                if (s.CardNumber == CardNumber_Field.Text)
                    CardNumberExists = true;
            if (!CardNumberExists)
                CardNumberError.Visibility = Visibility.Hidden;
            else
                CardNumberError.Visibility = Visibility.Visible;
            if(CardNumberExists && person!=null && ((Student)person).CardNumber == CardNumber_Field.Text)
                CardNumberError.Visibility = Visibility.Hidden;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            bool LoginExists = false;
            NullableError.Visibility = Visibility.Hidden;
            LoginError.Visibility = Visibility.Hidden;
            FacultyNullableError.Visibility = Visibility.Hidden;
            CardNumberError.Visibility = Visibility.Hidden;
            GroupNullableError.Visibility = Visibility.Hidden;
            foreach (Account a in dbl.GiveAccounts())
                if (a.Login == Login_Field.Text)
                    LoginExists = true;
            if (!LoginExists && Login_Field.Text.Length > 0 && SurName_Field.Text.Length > 0 && Name_Field.Text.Length > 0 && MidleName_Field.Text.Length > 0 && Phone_Field.Text.Length > 0 && Passwod_Field.Text.Length > 0 && Email_Field.Text.Length > 0 && EmailError.Visibility != Visibility.Visible)
            {
                if (person is Student)
                {
                    Student student = new Student();
                    student = AddStudent();
                    if (student != null)
                    {
                        student.Account = new Account { Login = Login_Field.Text, Password = Passwod_Field.Text, Email = Email_Field.Text };
                        student.SurName = SurName_Field.Text;
                        student.Name = Name_Field.Text;
                        student.MidleName = MidleName_Field.Text;
                        student.PhoneNumber = Phone_Field.Text;
                        student.University = dbl.LoggedPerson.University;
                        dbl.AddPerson(student);
                        Added_Event();
                    }
                }
                else if (person is Teacher)
                {
                    Teacher teacher = new Teacher();
                    teacher = AddTeacher();
                    if (teacher != null)
                    {
                        teacher.Account = new Account { Login = Login_Field.Text, Password = Passwod_Field.Text, Email = Email_Field.Text };
                        teacher.SurName = SurName_Field.Text;
                        teacher.Name = Name_Field.Text;
                        teacher.MidleName = MidleName_Field.Text;
                        teacher.PhoneNumber = Phone_Field.Text;
                        teacher.University = dbl.LoggedPerson.University;
                        dbl.AddPerson(teacher);
                        Added_Event();
                    }
                }
                else if (person is Dean)
                {
                    Dean dean = new Dean();
                    dean = AddDean();
                    if (dean != null)
                    {
                        dean.Account = new Account { Login = Login_Field.Text, Password = Passwod_Field.Text, Email = Email_Field.Text };
                        dean.SurName = SurName_Field.Text;
                        dean.Name = Name_Field.Text;
                        dean.MidleName = MidleName_Field.Text;
                        dean.PhoneNumber = Phone_Field.Text;
                        dean.University = dbl.LoggedPerson.University;
                        dbl.AddPerson(dean);
                        Added_Event();
                    }
                }
                else
                {
                    Rector rector = new Rector();
                    rector.Account = new Account { Login = Login_Field.Text, Password = Passwod_Field.Text, Email = Email_Field.Text };
                    rector.SurName = SurName_Field.Text;
                    rector.Name = Name_Field.Text;
                    rector.MidleName = MidleName_Field.Text;
                    rector.PhoneNumber = Phone_Field.Text;
                    rector.University = person.University;
                    dbl.AddPerson(rector);
                    RectorAdded_Event(rector);
                }
            }
            else if (LoginExists)
                LoginError.Visibility = Visibility.Visible;
            else NullableError.Visibility = Visibility.Visible;
        }

        private Dean AddDean()
        {
            Dean dean = new Dean();
            if (Facylty_Box.SelectedIndex != -1)
            {
                dean.Faculty = FacultyBoxList[Facylty_Box.SelectedIndex];
                return dean;
            }
            else
            {
                FacultyNullableError.Visibility = Visibility.Visible;
                return null;
            }
        }

        private Teacher AddTeacher()
        {
            Teacher teahcer = new Teacher();
            return teahcer;
        }

        private Student AddStudent()
        {
            Student student = new Student();
            bool CardNumberExists = false;
            foreach (Student s in dbl.GiveStudents())
                if (s.CardNumber == CardNumber_Field.Text || CardNumber_Field.Text == null)
                { CardNumberExists = true; CardNumberError.Visibility = Visibility.Visible; }
            if (!CardNumberExists)
                student.CardNumber = CardNumber_Field.Text;
            if (Group_Box.SelectedIndex != -1)
                student.Group = GroupBoxList[Group_Box.SelectedIndex];
            else
            {
                GroupNullableError.Visibility = Visibility.Visible;
                return null;
            }
            if (!CardNumberExists)
                return student;
            else return null;
        }

        public delegate void AddedEventHandler();
        public event AddedEventHandler Added_Event;

        public delegate void RectorAddedEventHandler(Rector r);
        public event RectorAddedEventHandler RectorAdded_Event;

        private void Specialty_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Specialty_Box.SelectedIndex != (-1))
            {
                GroupBoxList = new List<Group>();
                Group_Box.Items.Clear();
                foreach (Group g in dbl.GiveGroups())
                    if (g.Specialty == SpecialtyBoxList[Specialty_Box.SelectedIndex])
                        GroupBoxList.Add(g);
                foreach (Group g in GroupBoxList)
                    Group_Box.Items.Add(g.Course.ToString() + "-" + g.Number.ToString());
            }
            else
            {
                GroupBoxList = new List<Group>();
                Group_Box.Items.Clear();
            }
        }

        private void Facylty_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Facylty_Box.SelectedIndex != (-1))
            {
                SpecialtyBoxList = new List<Specialty>();
                Specialty_Box.Items.Clear();
                foreach (Specialty s in dbl.GiveSpecialties())
                    if (s.Faculty == FacultyBoxList[Facylty_Box.SelectedIndex])
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

        private void Email_Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!System.Text.RegularExpressions.Regex.IsMatch(Email_Field.Text, @"\w+[@]\w+[.]\w+"))
                EmailError.Visibility = Visibility.Visible;
            else
                EmailError.Visibility = Visibility.Hidden;
        }

        private void Phone_Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Phone_Field.Text, @"[+]\d{3}-\d{2}-\d{3}-\d{2}-\d{2}"))
                PhoneError.Visibility = Visibility.Visible;
            else
                PhoneError.Visibility = Visibility.Hidden;
        }
    }
}