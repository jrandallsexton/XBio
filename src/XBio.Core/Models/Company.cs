
using System.Collections.Generic;
using XBio.Core.Interfaces;

namespace XBio.Core.Models
{
    public class Company// : ICompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int UriId { get; set; }
        public Address HQ { get; set; }
        public List<Address> Locations { get; set; }
    }
}