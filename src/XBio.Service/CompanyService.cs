﻿
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
            return new CompanyRepository().Get(id);
        }

        public IEnumerable<KvpItem> GetLookups()
        {
            return new CompanyRepository().GetLookups();
        }

        public IEnumerable<Select2Item> Search(string searchTerm)
        {
            return new CompanyRepository().Search(searchTerm);
        }

        public void Save(ICompany company)
        {
            throw new NotImplementedException();
        }
    }
}