
using NUnit.Framework;
using XBio.Data;

namespace XBio.Tests.Repository
{
    [TestFixture]
    public class RepositoryBaseTests
    {
        [Test]
        public void BaseClass_HasConnectionString()
        {
            var baseClass = new RepositoryBase();
            Assert.NotNull(baseClass.ConnectionString);
        }
    }
}