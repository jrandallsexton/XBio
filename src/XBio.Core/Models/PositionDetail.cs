﻿
using System;

using XBio.Core.Interfaces;

namespace XBio.Core.Models
{
    public class PositionDetail : IPositionDetail
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
    }
}