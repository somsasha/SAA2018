using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SAA2018.Models;

namespace SAA2018
{
    class SAAContext : DbContext
    {
        static SAAContext()
        {
            Database.SetInitializer<SAAContext>(new SAAContextInitializer());
        }

        public SAAContext()
            : base("SAADB")
        { }

        public DbSet<Person> People { get; set; }
        public DbSet<University> Universitys { get; set; }
        public DbSet<Faculty> Facultys { get; set; }
        public DbSet<Specialty> Specialtys { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rector> Rectors { get; set; }
        public DbSet<Dean> Deans { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
