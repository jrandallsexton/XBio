using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBio.Core.Models
{
    public class City
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; }
        public float? Lat { get; set; }
        public float? Lon { get; set; }
    }
}
