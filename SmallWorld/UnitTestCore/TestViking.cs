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
            Assert.IsTrue(viking.CanMove(new Point(0, 0), new Lowland(), new Point(0, 1), new Sea(), true));
        }

        [TestMethod]
        public void TestGetPoints() {
            ITile[] neighbours = new ITile[4] { new Lowland(), new Lowland(), new Lowland(), new Lowland() };
            Assert.AreEqual(1, viking.GetPoints(new Forest(), neighbours));
            Assert.AreEqual(0, viking.GetPoints(new Sea(), neighbours));
            Assert.AreEqual(1, viking.GetPoints(new Lowland(), neighbours));

            // Vikings have one more point if they are next the sea:
            neighbours[0] = new Sea();
            neighbours[1] = new Sea();
            Assert.AreEqual(2, viking.GetPoints(new Lowland(), neighbours));
            Assert.AreEqual(1, viking.GetPoints(new Desert(), neighbours));
            Assert.AreEqual(2, viking.GetPoints(new Mountain(), neighbours));
        }

        [TestMethod]
        public void TestNationNumber() {
            Assert.AreEqual(1, playerViking.NationNumber);
        }
    }
}