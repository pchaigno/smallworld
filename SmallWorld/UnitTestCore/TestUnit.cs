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
            int defaultMovementPoint = unit.GetRemainingMovementPoints();
            unit.Move(new Forest());
            Assert.AreEqual(defaultMovementPoint - 2, unit.GetRemainingMovementPoints());
            unit.ResetMovementPoints();
            Assert.AreEqual(defaultMovementPoint, unit.GetRemainingMovementPoints());
        }

        [TestMethod]
        public void TestLifePoints() {
            this.TestLifePoints(viking);
            this.TestLifePoints(gaulois);
            this.TestLifePoints(dwarf);
        }

        private void TestLifePoints(IUnit unit) {
            Assert.AreEqual(unit.GetDefaultLifePoints(), unit.GetLifePoints());
            Assert.IsTrue(unit.IsAlive());
            unit.DecreaseLifePoints();
            Assert.AreEqual(unit.GetDefaultLifePoints() - 1, unit.GetLifePoints());
            while(unit.GetLifePoints() > 0) {
                Assert.IsTrue(unit.IsAlive());
                unit.DecreaseLifePoints();
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
            savedUnit.SetOwner(unit.GetOwner());
            stream.Close();
            Assert.AreEqual(unit.GetLifePoints(), savedUnit.GetLifePoints());
            Assert.AreEqual(unit.GetRemainingMovementPoints(), savedUnit.GetRemainingMovementPoints());
            Assert.AreEqual(unit.GetOwner().GetNumber(), savedUnit.GetOwner().GetNumber());
        }
    }
}