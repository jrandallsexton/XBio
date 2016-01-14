
using XBio.Core.Models;
using XBio.Data;

namespace XBio.Service
{
    public class PostalService
    {
        public void Save(Postal postal)
        {
            new PostalRepository().Save(postal);
        }
    }
}