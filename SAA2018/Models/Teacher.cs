using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAA2018.Models
{
    [Table("Teachers")]
    public class Teacher : Person
    {
        public virtual ICollection<Group> Groups { get; set; }

        public Teacher()
        {
            Groups = new List<Group>();
        }
    }
}
