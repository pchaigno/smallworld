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
        public void TestMovementPoints() {
            this.TestMovementPoints(viking);
            this.TestMovementPoints(gaulois);
            this.TestMovementPoints(dwarf);
        }

        private void TestMovementPoints(IUnit unit) {
            int defaultMovementPoint = unit.getRemainingMovementPoints();
            unit.move(new Forest());
            Assert.AreEqual(defaultMovementPoint - 2, unit.getRemainingMovementPoints());
            unit.resetMovementPoints();
            Assert.AreEqual(defaultMovementPoint, unit.getRemainingMovementPoints());
        }

        [TestMethod]
        public void TestLifePoints() {
            this.TestLifePoints(viking);
            this.TestLifePoints(gaulois);
            this.TestLifePoints(dwarf);
        }

        private void TestLifePoints(IUnit unit) {
            Assert.AreEqual(unit.getDefaultLifePoints(), unit.getLifePoints());
            Assert.IsTrue(unit.isAlive());
            unit.decreaseLifePoints();
            Assert.AreEqual(unit.getDefaultLifePoints() - 1, unit.getLifePoints());
            while(unit.getLifePoints() > 0) {
                Assert.IsTrue(unit.isAlive());
                unit.decreaseLifePoints();
            }
            Assert.IsFalse(unit.isAlive());
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
            savedUnit.setOwner(unit.getOwner());
            stream.Close();
            Assert.AreEqual(unit.getLifePoints(), savedUnit.getLifePoints());
            Assert.AreEqual(unit.getRemainingMovementPoints(), savedUnit.getRemainingMovementPoints());
            Assert.AreEqual(unit.getOwner().getNumber(), savedUnit.getOwner().getNumber());
        }
    }
}