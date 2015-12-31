
using System;
using System.Collections.Generic;

using XBio.Core.Dtos;
using XBio.Data;

namespace XBio.Service
{
    public class PersonService
    {
        public IEnumerable<KvpItem> GetPositionsByPersonId(int personId)
        {
            return new PositionRepository().GetPositionsByPersonId(personId);
        }

        public PersonDto GetPersonDto(int id)
        {
            throw new NotImplementedException();
        }
    }
}