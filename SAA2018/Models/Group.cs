using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA2018.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int Course { get; set; }
        public int Number { get; set; }

        public int? UniversityId { get; set; }
        public virtual University University { get; set; }
        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public int? SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }

        public Group()
        {
            Students = new List<Student>();
            Teachers = new List<Teacher>();
        }
    }
}
