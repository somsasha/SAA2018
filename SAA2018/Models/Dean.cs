using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAA2018.Models
{
    [Table("Deans")]
    public class Dean : Person
    {
        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
