
using NUnit.Framework;
using XBio.Service;

namespace XBio.Tests.Service
{

    [TestFixture]
    public class ResumeServiceTests
    {

        private ResumeService _service = new ResumeService();

        [Test]
        public void ResumeGet_Succeeds()
        {
            var resume = _service.Get(1);
            Assert.That(resume != null);
        }

    }

}