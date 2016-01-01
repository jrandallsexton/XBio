
using XBio.Core.Interfaces;

namespace XBio.Core.Models
{
    public class Company : ICompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}