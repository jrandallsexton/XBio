
using System;

namespace XBio.Core.Interfaces
{
    public interface IPosition : IAuditable
    {
        int Id { get; set; }
        int PersonId { get; set; }
        int CompanyId { get; set; }
        int TitleId { get; set; }
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }
    }
}