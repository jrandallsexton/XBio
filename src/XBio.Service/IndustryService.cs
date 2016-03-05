 
using System.Collections.Generic;

using XBio.Core.Dtos;
using XBio.Data;

namespace XBio.Service
{
    public class IndustryService
    {
        public List<Select2Item> Lookup()
        {
            return new IndustryRepository().Lookup();
        }
    }
}