
using System;
using System.Collections.Generic;

using XBio.Core.Dtos;
using XBio.Core.Interfaces;
using XBio.Core.Models;
using XBio.Data;

namespace XBio.Service
{
    public class PersonService
    {

        public IEnumerable<KvpItem> GetPositionsByPersonId(int personId)
        {
            return new PositionRepository().GetPositionsByPersonId(personId);
        }

        public Person Get(int id)
        {
            throw new NotImplementedException();
        }

        public PersonDto GetPersonDto(int id)
        {
            return new PersonRepository().GetPerson(id);
        }

        public void SavePosition(IPosition position)
        {
            new PositionRepository().Save(position);
        }

        public void DeletePosition(int personId, int positionId)
        {
            throw new NotImplementedException();
        }
    }
}