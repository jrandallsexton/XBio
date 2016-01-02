using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBio.Core.Dtos;
using XBio.Data;

namespace XBio.Service
{
    public class TitleService
    {
        public IEnumerable<KvpItem> GetLookups()
        {
            return new TitleRepository().GetLookups();
        }
    }
}
