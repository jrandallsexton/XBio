
using System.Collections.Generic;

using XBio.Core.Dtos;
using XBio.Data;

namespace XBio.Service
{
    public class StateService
    {
        public IEnumerable<KvpItem> GetStates(int countryId)
        {
            return new StateRepository().GetStates(countryId);
        }

        public int GetIdByAbbreviation(int countryId, string abbreviation)
        {
            return new StateRepository().GetIdByAbbreviation(countryId, abbreviation);
        }
    }
}