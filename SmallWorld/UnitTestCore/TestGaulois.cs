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
            gaulois.ResetMovementPoints();
            int defaultMovementPoints = gaulois.RemainingMovementPoints;
            Assert.IsTrue(gaulois.Move(new Lowland()));
            Assert.AreEqual(defaultMovementPoints - 1, gaulois.RemainingMovementPoints);
        }

        [TestMethod]
        public void TestCanMove() {
            gaulois.ResetMovementPoints();
            Assert.IsTrue(gaulois.Move(new Lowland()));
            Assert.IsTrue(gaulois.CanMove(new Point(0, 0), new Lowland(), new Point(0, 1), new Lowland(), true));
            Assert.IsFalse(gaulois.CanMove(new Point(0, 0), new Lowland(), new Point(0, 1), new Forest(), true));

            Assert.IsTrue(gaulois.Move(new Lowland()));
            Assert.IsFalse(gaulois.CanMove(new Point(0, 1), new Lowland(), new Point(1, 1), new Forest(), true));
            Assert.IsFalse(gaulois.CanMove(new Point(0, 1), new Lowland(), new Point(1, 1), new Lowland(), true));
        }

        [TestMethod]
        public void TestGetPoints() {
            ITile[] neighbours = new ITile[] {new Lowland(), new Lowland(), new Lowland(), new Lowland()};
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