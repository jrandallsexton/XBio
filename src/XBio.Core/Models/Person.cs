using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBio.Core.Interfaces;

namespace XBio.Core.Models
{
    public class Person : IPerson
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Display { get; set; }
    }
}
