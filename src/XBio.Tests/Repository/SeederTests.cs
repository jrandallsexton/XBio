using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBio.Data;

namespace XBio.Tests.Repository
{
    public class SeederTests
    {
        public void SeedCountries()
        {
            new Seeder().SeedCountries();
        }

        public void SeedStates()
        {
            new Seeder().SeedStates();
        }
    }
}
