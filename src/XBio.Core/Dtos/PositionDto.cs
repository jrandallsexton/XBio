
using System;
using System.Collections.Generic;

namespace XBio.Core.Dtos
{
    public class PositionDto
    {
        public string Company { get; set; }
        public string Title { get; set; }
        public bool Telecommute { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public IEnumerable<PositionDetailDto> Details { get; set; }

        public PositionDto()
        {
            this.Details = new List<PositionDetailDto>();
        }
    }
}