
using System;

namespace XBio.Core.Interfaces
{
    public interface IAuditable
    {
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
        DateTime? Deleted { get; set; }
    }
}