
using System;
using System.Collections.Generic;
using XBio.Core.Dtos;
using XBio.Core.Interfaces;
using XBio.Core.Models;
using XBio.Data;

namespace XBio.Service
{
    public class CompanyService
    {
        public Company Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KvpItem> GetLookups()
        {
            return new CompanyRepository().GetLookups();
        }

        public void Save(ICompany company)
        {
            throw new NotImplementedException();
        }
    }
}