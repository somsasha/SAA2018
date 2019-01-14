using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAA2018.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string MidleName { get; set; }
        public string PhoneNumber { get; set; }

        public int? AccountId { get; set; }
        public Account Account { get; set; }

        public int? UniversityId { get; set; }
        public virtual University University { get; set; }
    }
}