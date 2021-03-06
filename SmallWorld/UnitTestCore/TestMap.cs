﻿using System;
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
        IMap map = MapBuilder.Instance.BuildMap(15);

        [TestMethod]
        public void TestConstructor() {
            Assert.IsTrue(15 == map.Size);
            ITile[,] tiles = map.Tiles;
            for(int i = 0; i < 15; i++) {
                for(int j = 0; j < 15; j++) {
                    // We can use AreSame because we use a flyweight pattern.
                    Assert.AreSame(tiles[i, j], map.GetTile(new Point(i, j)));
                }
            }
        }

        [TestMethod]
        public void TestUnitPositions() {
            // Places a few units for tests:
            Player player1 = new Player("test1", new VikingFactory());
            Player player2 = new Player("test2", new GauloisFactory());
            IViking vikingA = new Viking(player1);
            IViking vikingB = new Viking(player1);
            IGaulois gauloisA = new Gaulois(player2);
            IGaulois gauloisB = new Gaulois(player2);
            map.PlaceUnit(vikingA, new Point(0, 0));
            map.PlaceUnit(vikingB, new Point(0, 0));
            map.PlaceUnit(gauloisA, new Point(14, 14));
            map.PlaceUnit(gauloisB, new Point(14, 14));
            List<IUnit> units = map.GetUnits(new Point(0, 0));
            Assert.AreEqual(2, units.Count);
            units = map.GetUnits(new Point(14, 14));
            Assert.AreEqual(2, units.Count);

            // Tests RemoveUnit:
            Assert.IsTrue(map.RemoveUnit(gauloisB, new Point(14, 14)));
            units = map.GetUnits(new Point(14, 14));
            Assert.AreEqual(1, units.Count);

            // Tests MoveUnit:
            map.MoveUnit(vikingB, new Point(0, 0), new Point(0, 1));
            units = map.GetUnits(new Point(0, 0));
            Assert.AreEqual(1, units.Count);
            units = map.GetUnits(new Point(0, 1));
            Assert.AreEqual(1, units.Count);
            
            // Tests EnemyPosition:
            Assert.IsFalse(map.IsEnemyPosition(new Point(14, 14), gauloisA));
            Assert.IsTrue(map.IsEnemyPosition(new Point(0, 0), gauloisB));
        }

        [TestMethod]
        public void TestGetIdleUnit() {
            Player player = new Player("test", new VikingFactory());
            IViking vikingA = new Viking(player);
            map.PlaceUnit(vikingA, new Point(0, 0));

            Tuple<IUnit, IPoint> idleUnit = map.GetIdleUnit(player);
            Assert.IsTrue(idleUnit.Item2.Equals(new Point(0, 0)));
            Assert.AreSame(idleUnit.Item1, vikingA);

            IViking vikingB = new Viking(player);
            map.PlaceUnit(vikingB, new Point(1, 1));
            Assert.IsTrue(vikingA.Move(new Forest()));

            idleUnit = map.GetIdleUnit(player);
            Assert.IsTrue(idleUnit.Item2.Equals(new Point(1, 1)));
            Assert.AreSame(idleUnit.Item1, vikingB);
        }

        [TestMethod]
        public void TestSerializationMap() {
            // Serializes:
            Stream stream = File.Open("Map.sav", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, map);
            stream.Close();

            // Deserializes and checks the values:
            stream = File.Open("Map.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            IMap savedMap = (IMap)formatter.Deserialize(stream);
            stream.Close();
            Assert.AreEqual(map.Size, savedMap.Size);
            // Checks the map composition:
            for(int i = 0; i < map.Size; i++) {
                for(int j = 0; j < map.Size; j++) {
                    Assert.IsInstanceOfType(savedMap.GetTile(new Point(i, j)), map.GetTile(new Point(i, j)).GetType());
                }
            }
            // Checks the units on the map:
            for(int i = 0; i < map.Size; i++) {
                for(int j = 0; j < map.Size; j++) {
                    List<IUnit> units = map.GetUnits(new Point(i, j));
                    List<IUnit> savedUnits = savedMap.GetUnits(new Point(i, j));
                    Assert.AreEqual(units.Count, savedUnits.Count);
                    for(int k=0; k<savedUnits.Count; k++) {
                        Assert.IsTrue(units.Contains(savedUnits[k]));
                    }
                }
            }
        }
    }
}