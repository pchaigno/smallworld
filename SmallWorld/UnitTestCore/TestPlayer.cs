using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using SmallWorld;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
            Assert.AreEqual(0, player.Points);
            player.AddPoints(5);
            Assert.AreEqual(5, player.Points);
            try {
                player.AddPoints(-5);
                Assert.Fail();
            } catch(ArgumentOutOfRangeException e) {
            
            }
            Assert.AreEqual(5, player.Points);
        }

        [TestMethod]
        public void TestCreateUnits() {
            this.TestCreateUnits(viking, typeof(IViking));
            this.TestCreateUnits(gaulois, typeof(IGaulois));
            this.TestCreateUnits(dwarf, typeof(IDwarf));
        }

        private void TestCreateUnits(IPlayer player, Type unitType) {
            List<IUnit> units = player.CreateUnits(2);
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
            Assert.AreNotEqual(viking.Number, gaulois.Number);
            Assert.AreNotEqual(gaulois.Number, dwarf.Number);
            Assert.AreNotEqual(dwarf.Number, viking.Number);
        }

        [TestMethod]
        public void TestSerialization() {
            this.TestSerialization(viking);
            this.TestSerialization(gaulois);
            this.TestSerialization(dwarf);
        }

        private void TestSerialization(IPlayer player) {
            Stream stream = File.Open("Player.sav", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, player);
            stream.Close();

            stream = File.Open("Player.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            IPlayer savedPlayer = (IPlayer)formatter.Deserialize(stream);
            stream.Close();
            Assert.IsTrue(player.Equals(savedPlayer));
            Assert.AreEqual(player.Points, savedPlayer.Points);
            Assert.AreEqual(player.Name, savedPlayer.Name);
            Assert.AreEqual(player.NationNumber, savedPlayer.NationNumber);
        }
    }
}
