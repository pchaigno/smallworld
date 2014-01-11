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
            Assert.IsTrue(dwarf.CanMove(new Point(0, 0), new Mountain(), new Point(0, 1), new Mountain()));
            Assert.IsTrue(dwarf.CanMove(new Point(0, 0), new Mountain(), new Point(4, 4), new Mountain()));
            Assert.IsFalse(dwarf.CanMove(new Point(0, 0), new Lowland(), new Point(0, 1), new Sea()));
        }

        [TestMethod]
        public void TestGetPoints() {
            ISquare[] neighbours = new ISquare[] { new Lowland(), new Lowland(), new Lowland(), new Lowland() };
            Assert.AreEqual(2, dwarf.GetPoints(new Forest(), neighbours));
            Assert.AreEqual(0, dwarf.GetPoints(new Sea(), neighbours));
            Assert.AreEqual(0, dwarf.GetPoints(new Lowland(), neighbours));
            Assert.AreEqual(1, dwarf.GetPoints(new Desert(), neighbours));
            Assert.AreEqual(1, dwarf.GetPoints(new Mountain(), neighbours));
        }

        [TestMethod]
        public void TestNationNumber() {
            Assert.AreEqual(3, playerDwarf.NationNumber);
        }
    }
}