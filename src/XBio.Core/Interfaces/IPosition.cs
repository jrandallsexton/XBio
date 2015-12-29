
using System;
using System.Collections.Generic;

namespace XBio.Core.Interfaces
{
    public interface IPosition : IAuditable
    {
        int Id { get; set; }
        int PersonId { get; set; }
        int CompanyId { get; set; }
        int TitleId { get; set; }
        List<IPositionDetail> Details { get; set; }
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }
    }
}