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
            Assert.AreEqual(1, gaulois.getPoint(new Forest()));
            Assert.AreEqual(0, gaulois.getPoint(new Sea()));
            Assert.AreEqual(2, gaulois.getPoint(new Lowland()));
            Assert.AreEqual(1, gaulois.getPoint(new Desert()));
            Assert.AreEqual(0, gaulois.getPoint(new Mountain()));
        }

        [TestMethod]
        public void TestNationNumber() {
            Assert.AreEqual(2, playerGaulois.getNationNumber());
        }
    }
}