using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA2018.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Faculty> Facultys { get; set; }
        public virtual ICollection<Specialty> Specialtys { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Rector> Rectors { get; set; }
        public virtual ICollection<Dean> Deans { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public University()
        {
            Facultys = new List<Faculty>();
            Specialtys = new List<Specialty>();
            Groups = new List<Group>();
            Rectors = new List<Rector>();
            Deans = new List<Dean>();
            Teachers = new List<Teacher>();
            Students = new List<Student>();
        }
    }
}
