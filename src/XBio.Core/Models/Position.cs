
using System;
using System.Collections.Generic;
using XBio.Core.Interfaces;

namespace XBio.Core.Models
{
    public class Position : IPosition
    {
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int CompanyId { get; set; }
        public int TitleId { get; set; }
        public IEnumerable<IPositionDetail> Details { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}