using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAA2018.Models;
using System.Data.Entity;

namespace SAA2018
{
    public class DBL
    {
        SAAContext db;
        public Account LoggedAccount;
        public Teacher LoggedTeacher;
        public Dean LoggedDean;
        public Rector LoggedRector;
        public Student LoggedStudent;
        public Person LoggedPerson;
        public enum AccountType
        {
            Null,
            Rector,
            Dean,
            Teacher,
            Student
        }
        public AccountType accounttype;

        public DBL()
        {
            db = new SAAContext();
            db.Accounts.Load();
            db.Students.Load();
            db.Teachers.Load();
            db.Deans.Load();
            db.Rectors.Load();
        }

        public void FindLoggedUser()
        {
            if (LoggedAccount != null)
            {
                var account = from a in db.People
                               where a.Account.Login == LoggedAccount.Login
                               select a;

                foreach (Person person in account)
                {
                    LoggedPerson = person;
                    if(person is Rector)
                        accounttype = AccountType.Rector;
                    if (person is Dean)
                        accounttype = AccountType.Dean;
                    if (person is Teacher)
                        accounttype = AccountType.Teacher;
                    if (person is Student)
                        accounttype = AccountType.Student;
                }
            }
        }

        public bool LogIn(string login, string password)
        {
            bool logged = false;
            foreach (Account account in db.Accounts)
            {
                if (account.Login == login && account.Password == password)
                {
                    logged = true;
                    LoggedAccount = account;
                }
            }
            return logged;
        }

        public void LogOff()
        {
            LoggedAccount = null;
            LoggedDean = null;
            LoggedRector = null;
            LoggedTeacher = null;
            LoggedStudent = null;
            LoggedPerson = null;
        }

        public void AddPerson(Person p)
        {
            db.People.Add(p);
            db.SaveChanges();
        }

        public void AddUniversity(University a)
        {
            db.Universitys.Add(a);
            db.SaveChanges();
        }

        public void AddFaculty(Faculty a)
        {
            db.Facultys.Add(a);
            db.SaveChanges();
        }

        public void AddSpecialty(Specialty a)
        {
            db.Specialtys.Add(a);
            db.SaveChanges();
        }

        public void AddGroup(Group a)
        {
            db.Groups.Add(a);
            db.SaveChanges();
        }

        public void ChangeObject(Object o)
        {
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemovePerson(Person p)
        {
            db.People.Remove(p);
            db.SaveChanges();
        }

        public void RemoveAccount(Account a)
        {
            db.Accounts.Remove(a);
            db.SaveChanges();
        }

        public void RemoveUniversity(University a)
        {
            db.Universitys.Remove(a);
            db.SaveChanges();
        }

        public void RemoveFaculty(Faculty a)
        {
            db.Facultys.Remove(a);
            db.SaveChanges();
        }

        public void RemoveSpecialty(Specialty a)
        {
            db.Specialtys.Remove(a);
            db.SaveChanges();
        }

        public void RemoveGroup(Group a)
        {
            db.Groups.Remove(a);
            db.SaveChanges();
        }

        public List<Faculty> GiveFaculties()
        {
            List<Faculty> Faculties = new List<Faculty>();
            var stu = db.Facultys.OrderBy(s => s.Name);
            Faculties = new List<Faculty>(stu);
            return Faculties;
        }

        public List<Specialty> GiveSpecialties()
        {
            List<Specialty> Specialties = new List<Specialty>();
            var stu = db.Specialtys.OrderBy(s => s.Name);
            Specialties = new List<Specialty>(stu);
            return Specialties;
        }

        public List<Group> GiveGroups()
        {
            List<Group> Groups = new List<Group>();
            var stu = db.Groups.OrderBy(s => s.Course);
            Groups = new List<Group>(stu);
            return Groups;
        }

        public List<Student> GiveStudents()
        {
            List<Student> Students = new List<Student>();
            var stu = db.Students.OrderBy(s => s.SurName);
            Students = new List<Student>(stu);
            return Students;
        }

        public List<Dean> GiveDeans()
        {
            List<Dean> Deans = new List<Dean>();
            var stu = db.Deans.OrderBy(s => s.SurName);
            Deans = new List<Dean>(stu);
            return Deans;
        }

        public List<Teacher> GiveTeachers()
        {
            List<Teacher> Teachers = new List<Teacher>();
            var stu = db.Teachers.OrderBy(s => s.SurName);
            Teachers = new List<Teacher>(stu);
            return Teachers;
        }

        public List<Account> GiveAccounts()
        {
            List<Account> Accounts = new List<Account>();
            var stu = db.Accounts.OrderBy(s => s.Login);
            Accounts = new List<Account>(stu);
            return Accounts;
        }
    }
}
