
using System;
using System.Collections.Generic;

using XBio.Core.Dtos;
using XBio.Core.Models;
using XBio.Data;

namespace XBio.Service
{
    public class TechnologyService
    {
        public IEnumerable<Technology> GetTechnologies()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KvpItem> GetTechnologiesByType(int technologyTypeId)
        {
            return new TechnologyRepository().GetTechnologiesByType(technologyTypeId);
        }

        public void Save(Technology technology)
        {
            throw new NotImplementedException();
        }
    }
}