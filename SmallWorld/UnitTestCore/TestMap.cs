using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;
using System.Collections.Generic;

namespace UnitTestCore {

    [TestClass]
    public class TestMap {
        IMap map = new MapBuilder().buildMap(15);

        [TestMethod]
        public void TestConstructor() {
            Assert.IsTrue(15 == map.getSize());
            ISquare[,] squares = map.getSquares();
            for(int i = 0; i < 15; i++) {
                for(int j = 0; j < 15; j++) {
                    // We can use AreSame because we use a flyweight pattern.
                    Assert.AreSame(squares[i, j], map.getSquare(new Point(i, j)));
                }
            }
        }

        [TestMethod]
        public void TestUnitPositions() {
            Player player1 = new Player("test1", new VikingFactory());
            Player player2 = new Player("test2", new GauloisFactory());
            IViking vikingA = new Viking(player1);
            IViking vikingB = new Viking(player1);
            IGaulois gauloisA = new Gaulois(player2);
            IGaulois gauloisB = new Gaulois(player2);
            map.placeUnit(vikingA, new Point(0, 0));
            map.placeUnit(vikingB, new Point(0, 0));
            map.placeUnit(gauloisA, new Point(14, 14));
            map.placeUnit(gauloisB, new Point(14, 14));
            List<IUnit> units = map.getUnits(new Point(0, 0));
            Assert.AreEqual(2, units.Count);
            units = map.getUnits(new Point(14, 14));
            Assert.AreEqual(2, units.Count);

            map.removeUnit(gauloisB, new Point(14, 14));
            units = map.getUnits(new Point(14, 14));
            Assert.AreEqual(1, units.Count);

            map.moveUnit(vikingB, new Point(0, 0), new Point(0, 1));
            units = map.getUnits(new Point(0, 0));
            Assert.AreEqual(1, units.Count);
            units = map.getUnits(new Point(0, 1));
            Assert.AreEqual(1, units.Count);
            
            Assert.IsFalse(map.isEnemyPosition(new Point(14, 14), gauloisA));
            Assert.IsTrue(map.isEnemyPosition(new Point(0, 0), gauloisB));
        }
    }
}