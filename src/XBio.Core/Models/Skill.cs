
using System;

namespace XBio.Core.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int TechnologyId { get; set; }
        public int FirstUsedYear { get; set; }
        public int LastUsedYear { get; set; }
        public decimal NumYearsUsed { get; set; }
        public decimal Strength { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
    }
}