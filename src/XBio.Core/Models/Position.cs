
using System;
using System.Collections.Generic;
using XBio.Core.Interfaces;

namespace XBio.Core.Models
{
    public class Position //: IPosition
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int CompanyId { get; set; }
        public int TitleId { get; set; }
        public string Summary { get; set; }
        public List<PositionDetail> Details { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public Position()
        {
            this.Details = new List<PositionDetail>();
        }
    }
}