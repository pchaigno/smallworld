using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestCore {

    [TestClass]
    public class TestViking {
        private static IPlayer playerViking = new Player("test", new VikingFactory());
        private static IViking viking = new Viking(new Player("test", new VikingFactory()));

        [TestMethod]
        public void TestCanMove() {
            Assert.IsTrue(viking.canMove(new Point(0, 0), new Point(0, 1), new Sea()));
        }

        [TestMethod]
        public void TestGetPoints() {
            Assert.AreEqual(1, viking.getPoint(new Forest()));
            Assert.AreEqual(0, viking.getPoint(new Sea()));
            // TODO Near sea instead of lowlands.
            Assert.AreEqual(2, viking.getPoint(new Lowland()));
            Assert.AreEqual(0, viking.getPoint(new Desert()));
            Assert.AreEqual(1, viking.getPoint(new Mountain()));
        }

        [TestMethod]
        public void TestNationNumber() {
            Assert.AreEqual(1, playerViking.getNationNumber());
        }
    }
}