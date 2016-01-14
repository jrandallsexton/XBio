
using XBio.Core.Models;
using XBio.Data;

namespace XBio.Service
{
    public class CityService
    {
        private readonly CityRepository _repo = new CityRepository();

        public void Save(City city)
        {
            _repo.Save(city);
        }

        public City Search(int stateId, string cityName)
        {
            return _repo.Search(stateId, cityName);
        }
    }
}