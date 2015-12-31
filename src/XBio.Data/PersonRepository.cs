using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBio.Core.Dtos;
using XBio.Core.Interfaces;
using XBio.Core.Models;

namespace XBio.Data
{
    public class PersonRepository
    {
        public IPerson Get(int id)
        {
            throw new NotImplementedException();
        }

        public PersonDto GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        private List<Person> LoadFromReader(IDataReader rdr)
        {
            throw new NotImplementedException();
        }
    }
}