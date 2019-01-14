using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA2018.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? UniversityId { get; set; }
        public virtual University University { get; set; }

        public virtual ICollection<Specialty> Specialtys { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Dean> Deans { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public Faculty()
        {
            Specialtys = new List<Specialty>();
            Groups = new List<Group>();
            Deans = new List<Dean>();
            Students = new List<Student>();
        }
    }
}
