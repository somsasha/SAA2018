using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAA2018.Models
{
    [Table("Students")]
    public class Student : Person
    {
        public string CardNumber { get; set; }
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
