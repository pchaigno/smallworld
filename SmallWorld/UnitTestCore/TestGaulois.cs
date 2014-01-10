using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestCore {

    [TestClass]
    public class TestGaulois {
        private static IPlayer playerGaulois = new Player("test", new GauloisFactory());
        private static IGaulois gaulois = new Gaulois(new Player("test", new GauloisFactory()));

        [TestMethod]
        public void TestMove() {
            int defaultMovementPoints = gaulois.RemainingMovementPoints;
            gaulois.Move(new Lowland());
            Assert.AreEqual(defaultMovementPoints - 1, gaulois.RemainingMovementPoints);
        }

        [TestMethod]
        public void TestGetPoints() {
            ISquare[] neighbours = new ISquare[] {new Lowland(), new Lowland(), new Lowland(), new Lowland()};
            Assert.AreEqual(1, gaulois.GetPoints(new Forest(), neighbours));
            Assert.AreEqual(0, gaulois.GetPoints(new Sea(), neighbours));
            Assert.AreEqual(2, gaulois.GetPoints(new Lowland(), neighbours));
            Assert.AreEqual(1, gaulois.GetPoints(new Desert(), neighbours));
            Assert.AreEqual(0, gaulois.GetPoints(new Mountain(), neighbours));
        }

        [TestMethod]
        public void TestNationNumber() {
            Assert.AreEqual(2, playerGaulois.NationNumber);
        }
    }
}