using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestCore {

    [TestClass]
    public class TestDwarf {
        private static IPlayer playerDwarf = new Player("test", new DwarfFactory());
        private static IDwarf dwarf = new Dwarf(new Player("test", new DwarfFactory())); 

        [TestMethod]
        public void TestCanMove() {
            // TODO What??
            Assert.IsFalse(dwarf.canMove(new Point(0, 0), new Point(0, 1), new Mountain()));
            Assert.IsFalse(dwarf.canMove(new Point(0, 0), new Point(0, 1), new Sea()));
        }

        [TestMethod]
        public void TestGetPoints() {
            ISquare[] neighbours = new ISquare[] { new Lowland(), new Lowland(), new Lowland(), new Lowland() };
            Assert.AreEqual(2, dwarf.getPoints(new Forest(), neighbours));
            Assert.AreEqual(0, dwarf.getPoints(new Sea(), neighbours));
            Assert.AreEqual(0, dwarf.getPoints(new Lowland(), neighbours));
            Assert.AreEqual(1, dwarf.getPoints(new Desert(), neighbours));
            Assert.AreEqual(1, dwarf.getPoints(new Mountain(), neighbours));
        }

        [TestMethod]
        public void TestNationNumber() {
            Assert.AreEqual(3, playerDwarf.getNationNumber());
        }
    }
}