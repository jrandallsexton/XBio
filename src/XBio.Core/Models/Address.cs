using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBio.Core.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public int PostalId { get; set; }
        public string Name { get; set; }
    }
}
