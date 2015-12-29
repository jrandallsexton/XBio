using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using XBio.Core.Models;
using XBio.Data;

namespace XBio.Tests.Repository
{
    [TestFixture]
    public class PositionRepositoryTests
    {
        private PositionRepository _repo = new PositionRepository();

        [Test]
        public void PositionGet_Succeeds()
        {
            var pos = new PositionRepository().Get(2);
            Assert.NotNull(pos);
        }

        [Test]
        public void PositionSave_Succeeds()
        {
            var positionId = 2;

            var pos = _repo.Get(positionId);
            Assert.NotNull(pos);

            // Add some details to it
            pos.Details.Add(new PositionDetail
            {
                Order = 0,
                PositionId = pos.Id,
                Value =
                    "Lead developer on a dynamic metadata-driven database system which facilitated management and tracking of Bechtel’s IT assets (hardware and software)."
            });

            _repo.Save(pos);

            pos = _repo.Get(positionId);
            Assert.That(pos.Details.Count == 1);
        }
    }
}