using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA2018.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? UniversityId { get; set; }
        public virtual University University { get; set; }
        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public Specialty()
        {
            Groups = new List<Group>();
            Students = new List<Student>();
        }
    }
}
