using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace UnitTestCore {
    [TestClass]
    public class TestUnit {
        private static IViking viking = new Viking(new Player("test", new VikingFactory()));
        private static IGaulois gaulois = new Gaulois(new Player("test", new GauloisFactory()));
        private static IDwarf dwarf = new Dwarf(new Player("test", new DwarfFactory()));

        [TestMethod]
        public void TestEquals() {
            Assert.IsTrue(viking.Equals(viking));
            Assert.IsFalse(dwarf.Equals(viking));
            Assert.IsFalse(gaulois.Equals(dwarf));
        }

        [TestMethod]
        public void TestMovementPoints() {
            this.TestMovementPoints(viking);
            this.TestMovementPoints(gaulois);
            this.TestMovementPoints(dwarf);
        }

        private void TestMovementPoints(IUnit unit) {
            int defaultMovementPoint = unit.RemainingMovementPoints;
            Assert.IsTrue(unit.Move(new Forest()));
            Assert.AreEqual(defaultMovementPoint - 2, unit.RemainingMovementPoints);
            unit.ResetMovementPoints();
            Assert.AreEqual(defaultMovementPoint, unit.RemainingMovementPoints);
        }

        [TestMethod]
        public void TestLifePoints() {
            this.TestLifePoints(viking);
            this.TestLifePoints(gaulois);
            this.TestLifePoints(dwarf);
        }

        private void TestLifePoints(IUnit unit) {
            Assert.AreEqual(unit.DefaultLifePoints, unit.LifePoints);
            Assert.IsTrue(unit.IsAlive());
            Assert.IsTrue(unit.DecreaseLifePoints());
            Assert.AreEqual(unit.DefaultLifePoints - 1, unit.LifePoints);
            while(unit.LifePoints > 0) {
                Assert.IsTrue(unit.IsAlive());
                Assert.IsTrue(unit.DecreaseLifePoints());
            }
            Assert.IsFalse(unit.IsAlive());
        }

        [TestMethod]
        public void TestSerializationUnit() {
            this.TestSerializationUnit(viking);
            this.TestSerializationUnit(gaulois);
            this.TestSerializationUnit(dwarf);
        }

        private void TestSerializationUnit(IUnit unit) {
            Stream stream = File.Open("Unit.sav", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, unit);
            stream.Close();

            stream = File.Open("Unit.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            IUnit savedUnit = (IUnit)formatter.Deserialize(stream);
            stream.Close();
            Assert.IsTrue(unit.Equals(savedUnit));
            Assert.AreEqual(unit.LifePoints, savedUnit.LifePoints);
            Assert.AreEqual(unit.RemainingMovementPoints, savedUnit.RemainingMovementPoints);
            Assert.IsTrue(unit.Owner.Equals(savedUnit.Owner));
        }
    }
}