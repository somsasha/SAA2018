using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SAA2018.Models;

namespace SAA2018
{
    class SAAContextInitializer : DropCreateDatabaseIfModelChanges<SAAContext>
    {
        protected override void Seed(SAAContext db)
        {
            University u1 = new University { Name = "БГТУ" };
            Rector r1 = new Rector { SurName = "Войтов", Name = "Игорь", MidleName = "Витальевич", PhoneNumber = "+375-17-226-14-32", University = u1, Account = new Account { Login = "R1Login", Password = "R1Password", Email = "rector1.bstu@mail.ru" } };

            Teacher t1 = new Teacher { SurName = "Ловенецкая", Name = "Елена", MidleName = "Ивановна", PhoneNumber = "+375-17-327-10-20", University = u1, Account = new Account { Login = "T1Login", Password = "T1Password", Email = "teacher1.bstu@mail.ru" } };
            Teacher t2 = new Teacher { SurName = "Доморад", Name = "Алексей", MidleName = "Анатольевич", PhoneNumber = "+375-17-322-11-53", University = u1, Account = new Account { Login = "T2Login", Password = "T2Password", Email = "teacher1.bstu@mail.ru" } };
            Teacher t3 = new Teacher { SurName = "Шишло", Name = "Сергей", MidleName = "Валерьевич", PhoneNumber = "+375-17-421-13-33", University = u1, Account = new Account { Login = "T3Login", Password = "T3Password", Email = "teacher1.bstu@mail.ru" } };
            Teacher t4 = new Teacher { SurName = "Пустовалова", Name = "Наталья", MidleName = "Николаевна", PhoneNumber = "+375-17-327-43-76", University = u1, Account = new Account { Login = "T4Login", Password = "T4Password", Email = "teacher1.bstu@mail.ru" } };
            

            Faculty f1 = new Faculty { Name = "Информационных технологий", University = u1 };
            Dean d1 = new Dean { SurName = "Шиман", Name = "Дмитрий", MidleName = "Васильевич", PhoneNumber = "+375-17-399-33-89", Faculty = f1, University = u1, Account = new Account { Login = "D1Login", Password = "D1Password", Email = "dean1.bstu@mail.ru" } };

            Specialty sp1_1 = new Specialty { Name = "Информационные системы и технологии", Faculty = f1, University = u1 };
            Group g1 = new Group { Course = 1, Number = 1, Specialty = sp1_1, Faculty = f1, University = u1 };
            Student s1 = new Student { SurName = "Иванов", Name = "Иван", MidleName = "Иванович", PhoneNumber = "+375-14-136-24-12", CardNumber="123141", Group = g1, University = u1, Account = new Account { Login = "S1Login", Password = "S1Password", Email = "student1.bstu@mail.ru" } };
            Student s2 = new Student { SurName = "Торкун", Name = "Василий", MidleName = "Александрович", PhoneNumber = "+375-11-116-14-13", CardNumber = "123142", Group = g1, University = u1, Account = new Account { Login = "S2Login", Password = "S2Password", Email = "student1.bstu@mail.ru" } };
            Student s3 = new Student { SurName = "Заболотин", Name = "Дмитрий", MidleName = "Васильевич", PhoneNumber = "+375-12-126-22-54", CardNumber = "123143", Group = g1, University = u1, Account = new Account { Login = "S3Login", Password = "S3Password", Email = "student1.bstu@mail.ru" } };
            Student s4 = new Student { SurName = "Горбачёв", Name = "Денис", MidleName = "Павлович", PhoneNumber = "+375-31-355-32-12", CardNumber = "123144", Group = g1, University = u1, Account = new Account { Login = "S4Login", Password = "S4Password", Email = "student1.bstu@mail.ru" } };
            Student s5 = new Student { SurName = "Захарёв", Name = "Иван", MidleName = "Никифорович", PhoneNumber = "+375-12-563-24-55", CardNumber = "123145", Group = g1, University = u1, Account = new Account { Login = "S5Login", Password = "S5Password", Email = "student1.bstu@mail.ru" } };

            Specialty sp1_2 = new Specialty { Name = "Программное обеспечение информационных технологий", Faculty = f1, University = u1 };
            Group g2 = new Group { Course = 2, Number = 5, Specialty = sp1_2, Faculty = f1, University = u1};
            Student s6 = new Student { SurName = "Понаморёв", Name = "Никита", MidleName = "Васильевич", PhoneNumber = "+375-33-303-77-06", CardNumber = "123146", Group = g2, University = u1, Account = new Account { Login = "S6Login", Password = "S6Password", Email = "student1.bstu@mail.ru" } };
            Student s7 = new Student { SurName = "Донцова", Name = "Александра", MidleName = "Потаповна", PhoneNumber = "+375-11-533-14-55", CardNumber = "123147", Group = g2, University = u1, Account = new Account { Login = "S7Login", Password = "S7Password", Email = "student1.bstu@mail.ru" } };
            Student s8 = new Student { SurName = "Павлов", Name = "Иван", MidleName = "Александрович", PhoneNumber = "+375-12-579-67-88", CardNumber = "123148", Group = g2, University = u1, Account = new Account { Login = "S8Login", Password = "S8Password", Email = "student1.bstu@mail.ru" } };
            Student s9 = new Student { SurName = "Тарасов", Name = "Захар", MidleName = "Викторович", PhoneNumber = "+375-12-658-78-99", CardNumber = "123149", Group = g2, University = u1, Account = new Account { Login = "S9Login", Password = "S9Password", Email = "student1.bstu@mail.ru" } };
            Student s10 = new Student { SurName = "Захарёв", Name = "Рустам", MidleName = "Викторович", PhoneNumber = "+375-12-776-89-55", CardNumber = "123150", Group = g2, University = u1, Account = new Account { Login = "S10Login", Password = "S10Password", Email = "student1.bstu@mail.ru" } };

            Group g3 = new Group { Course = 1, Number = 4, Specialty = sp1_2, Faculty = f1, University = u1 };
            Student s11 = new Student { SurName = "Исаченков", Name = "Василий", MidleName = "Иванович", PhoneNumber = "+375-11-656-64-12", CardNumber = "123151", Group = g3, University = u1, Account = new Account { Login = "S11Login", Password = "S11Password", Email = "student1.bstu@mail.ru" } };
            Student s12 = new Student { SurName = "Васильев", Name = "Тарас", MidleName = "Павлович", PhoneNumber = "+375-88-166-28-82", CardNumber = "123152", Group = g3, University = u1, Account = new Account { Login = "S12Login", Password = "S12Password", Email = "student1.bstu@mail.ru" } };
            Student s13 = new Student { SurName = "Дудкин", Name = "Николай", MidleName = "Фёдорович", PhoneNumber = "+375-14-157-98-02", CardNumber = "123153", Group = g3, University = u1, Account = new Account { Login = "S13Login", Password = "S13Password", Email = "student1.bstu@mail.ru" } };
            Student s14 = new Student { SurName = "Ивашкин", Name = "Андрей", MidleName = "Олегович", PhoneNumber = "+375-14-100-20-10", CardNumber = "123154", Group = g3, University = u1, Account = new Account { Login = "S14Login", Password = "S14Password", Email = "student1.bstu@mail.ru" } };
            Student s15 = new Student { SurName = "Турецкий", Name = "Гавриил", MidleName = "Андреевич", PhoneNumber = "+375-17-197-94-12", CardNumber = "123155", Group = g3, University = u1, Account = new Account { Login = "S15Login", Password = "S15Password", Email = "student1.bstu@mail.ru" } };

            Faculty f2 = new Faculty { Name = "Принттехнологий и медиакоммуникаций", University = u1 };
            Dean d2 = new Dean { SurName = "Долгова", Name = "Татьяна", MidleName = "Александровна", PhoneNumber = "+375-17-327-71-98", Faculty = f2, University = u1, Account = new Account { Login = "D2Login", Password = "D2Password", Email = "dean1.bstu@mail.ru" } };

            Specialty sp2_1 = new Specialty { Name = "Издательское дело", Faculty = f2, University = u1 };
            Group g4 = new Group { Course = 3, Number = 2, Specialty = sp2_1, Faculty = f2, University = u1 };
            Student s16 = new Student { SurName = "Советская", Name = "Елизовета", MidleName = "Ивановна", PhoneNumber = "+375-14-658-90-12", CardNumber = "123156", Group = g4, University = u1, Account = new Account { Login = "S16Login", Password = "S16Password", Email = "student1.bstu@mail.ru" } };
            Student s17 = new Student { SurName = "Горбачёва", Name = "Нина", MidleName = "Павловна", PhoneNumber = "+375-14-567-24-09", CardNumber = "123157", Group = g4, University = u1, Account = new Account { Login = "S17Login", Password = "S17Password", Email = "student1.bstu@mail.ru" } };
            Student s18 = new Student { SurName = "Сергеева", Name = "Полина", MidleName = "Андреевна", PhoneNumber = "+375-14-456-24-98", CardNumber = "123158", Group = g4, University = u1, Account = new Account { Login = "S18Login", Password = "S18Password", Email = "student1.bstu@mail.ru" } };
            Student s19 = new Student { SurName = "Перусенко", Name = "Надежда", MidleName = "Викторовна", PhoneNumber = "+375-67-890-09-12", CardNumber = "123159", Group = g4, University = u1, Account = new Account { Login = "S19Login", Password = "S19Password", Email = "student1.bstu@mail.ru" } };
            Student s20 = new Student { SurName = "Куприк", Name = "Оксана", MidleName = "Васильевна", PhoneNumber = "+375-45-136-07-56", CardNumber = "123160", Group = g4, University = u1, Account = new Account { Login = "S20Login", Password = "S20Password", Email = "student1.bstu@mail.ru" } };

            db.Universitys.Add(u1);
            db.Rectors.Add(r1);
            db.Facultys.AddRange(new List<Faculty> { f1, f2 });
            db.Specialtys.AddRange(new List<Specialty> { sp1_1, sp1_2, sp2_1 });
            db.Deans.AddRange(new List<Dean> { d1, d2 });
            db.Groups.AddRange(new List<Group> { g1, g2, g3, g4 });
            db.Teachers.AddRange(new List<Teacher> { t1, t2, t3, t4 });
            db.Students.AddRange(new List<Student> { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20 });

            db.SaveChanges();
        }
    }
}
