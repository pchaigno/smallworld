using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;

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

        [TestMethod]
        public void TestSerializationMap() {
            Stream stream = File.Open("Map.sav", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, map);
            stream.Close();

            stream = File.Open("Map.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            IMap savedMap = (IMap)formatter.Deserialize(stream);
            stream.Close();
            Assert.AreEqual(map.getSize(), savedMap.getSize());
            for(int i = 0; i < map.getSize(); i++) {
                for(int j = 0; j < map.getSize(); j++) {
                    Assert.IsInstanceOfType(savedMap.getSquare(new Point(i, j)), map.getSquare(new Point(i, j)).GetType());
                }
            }
            for(int i = 0; i < map.getSize(); i++) {
                for(int j = 0; j < map.getSize(); j++) {
                    List<IUnit> units = map.getUnits(new Point(i, j));
                    List<IUnit> savedUnits = savedMap.getUnits(new Point(i, j));
                    Assert.AreEqual(units.Count, savedUnits.Count);
                    for(int k=0; k<savedUnits.Count; k++) {
                        Assert.IsTrue(units.Contains(savedUnits[k]));
                    }
                }
            }
        }
    }
}