
using System;

namespace XBio.Core.Interfaces
{
    public interface IPositionDetail
    {
        int Id { get; set; }
        int PositionId { get; set; }
        string Title { get; set; }
        string Value { get; set; }
        int Order { get; set; }
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
        DateTime? Deleted { get; set; }
    }
}