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
            Assert.AreEqual(2, dwarf.getPoint(new Forest()));
            Assert.AreEqual(0, dwarf.getPoint(new Sea()));
            Assert.AreEqual(0, dwarf.getPoint(new Lowland()));
            Assert.AreEqual(1, dwarf.getPoint(new Desert()));
            Assert.AreEqual(1, dwarf.getPoint(new Mountain()));
        }

        [TestMethod]
        public void TestNationNumber() {
            Assert.AreEqual(3, playerDwarf.getNationNumber());
        }
    }
}