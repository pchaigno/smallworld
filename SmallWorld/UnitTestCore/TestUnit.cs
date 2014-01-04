﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;
using System.Drawing;

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
        public void TestIsNext() {
            Assert.IsTrue(Unit.isNext(new Point(0, 1), new Point(0, 0)));
            Assert.IsFalse(Unit.isNext(new Point(1, 1), new Point(0, 0)));
            Assert.IsTrue(Unit.isNext(new Point(14, 14), new Point(14, 13)));
            Assert.IsFalse(Unit.isNext(new Point(14, 14), new Point(13, 13)));
        }
    }
}