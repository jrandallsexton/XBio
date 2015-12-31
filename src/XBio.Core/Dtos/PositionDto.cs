
using System;
using System.Collections.Generic;

namespace XBio.Core.Dtos
{
    public class PositionDto
    {
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<PositionDetailDto> Details { get; set; }

        public PositionDto()
        {
            this.Details = new List<PositionDetailDto>();
        }
    }
}