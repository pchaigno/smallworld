using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestCore {
    [TestClass]
    public class TestPlayer {
        private static IPlayer viking = new Player("viking", new VikingFactory());
        private static IPlayer gaulois = new Player("gaulois", new GauloisFactory());
        private static IPlayer dwarf = new Player("dwarf", new DwarfFactory());

        [TestMethod]
        public void TestPoints() {
            this.TestPoints(viking);
            this.TestPoints(gaulois);
            this.TestPoints(dwarf);
        }

        private void TestPoints(IPlayer player) {
            Assert.AreEqual(0, player.getPoints());
            player.addPoints(5);
            Assert.AreEqual(5, player.getPoints());
            try {
                player.addPoints(-5);
                Assert.Fail();
            } catch(ArgumentOutOfRangeException e) {
            
            }
            Assert.AreEqual(5, player.getPoints());
        }

        [TestMethod]
        public void TestCreateUnits() {
            this.TestCreateUnits(viking, typeof(IViking));
            this.TestCreateUnits(gaulois, typeof(IGaulois));
            this.TestCreateUnits(dwarf, typeof(IDwarf));
        }

        private void TestCreateUnits(IPlayer player, Type unitType) {
            List<IUnit> units = player.createUnits(2);
            Assert.AreEqual(2, units.Count);
            foreach(IUnit unit in units) {
                Assert.IsInstanceOfType(unit, unitType);
            }
        }

        [TestMethod]
        public void TestEquals() {
            Assert.IsFalse(viking.Equals(gaulois));
            Assert.IsFalse(gaulois.Equals(viking));
            Assert.IsTrue(viking.Equals(viking));
            Assert.IsFalse(viking.Equals(null));
            Assert.IsFalse(viking.Equals(new Sea()));
        }

        [TestMethod]
        public void TestNumber() {
            Assert.AreNotEqual(viking.getNumber(), gaulois.getNumber());
            Assert.AreNotEqual(gaulois.getNumber(), dwarf.getNumber());
            Assert.AreNotEqual(dwarf.getNumber(), viking.getNumber());
        }
    }
}
