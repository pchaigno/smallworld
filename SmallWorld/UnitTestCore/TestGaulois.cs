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
            int defaultMovementPoints = gaulois.getRemainingMovementPoints();
            gaulois.move(new Lowland());
            Assert.AreEqual(defaultMovementPoints - 1, gaulois.getRemainingMovementPoints());
        }

        [TestMethod]
        public void TestGetPoints() {
            ISquare[] neighbours = new ISquare[] {new Lowland(), new Lowland(), new Lowland(), new Lowland()};
            Assert.AreEqual(1, gaulois.getPoints(new Forest(), neighbours));
            Assert.AreEqual(0, gaulois.getPoints(new Sea(), neighbours));
            Assert.AreEqual(2, gaulois.getPoints(new Lowland(), neighbours));
            Assert.AreEqual(1, gaulois.getPoints(new Desert(), neighbours));
            Assert.AreEqual(0, gaulois.getPoints(new Mountain(), neighbours));
        }

        [TestMethod]
        public void TestNationNumber() {
            Assert.AreEqual(2, playerGaulois.getNationNumber());
        }
    }
}