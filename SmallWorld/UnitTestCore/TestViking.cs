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
            Assert.IsTrue(viking.canMove(new Point(0, 0), new Lowland(), new Point(0, 1), new Sea()));
        }

        [TestMethod]
        public void TestGetPoints() {
            ISquare[] neighbours = new ISquare[4] {new Lowland(), new Lowland(), new Lowland(), new Lowland()};
            Assert.AreEqual(1, viking.getPoints(new Forest(), neighbours));
            Assert.AreEqual(0, viking.getPoints(new Sea(), neighbours));
            Assert.AreEqual(1, viking.getPoints(new Lowland(), neighbours));
            neighbours[0] = new Sea();
            Assert.AreEqual(2, viking.getPoints(new Lowland(), neighbours));
            Assert.AreEqual(0, viking.getPoints(new Desert(), neighbours));
            Assert.AreEqual(1, viking.getPoints(new Mountain(), neighbours));
        }

        [TestMethod]
        public void TestNationNumber() {
            Assert.AreEqual(1, playerViking.getNationNumber());
        }
    }
}