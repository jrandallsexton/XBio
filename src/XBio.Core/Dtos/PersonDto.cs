
using System.Collections.Generic;

using XBio.Core.Interfaces;

namespace XBio.Core.Dtos
{
    public class PersonDto : IPerson
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Display { get; set; }
        public List<PositionDto> Positions { get; set; }

        public PersonDto()
        {
            this.Positions = new List<PositionDto>();
        }
    }
}