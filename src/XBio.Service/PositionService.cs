
using XBio.Core.Models;
using XBio.Data;

namespace XBio.Service
{
    public class PositionService
    {
        public Position Get(int id)
        {
            return new PositionRepository().Get(id);
        }
    }
}