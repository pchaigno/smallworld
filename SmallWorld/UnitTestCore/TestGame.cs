using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SmallWorld;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

namespace UnitTestCore {

    [TestClass]
    public class TestGame {
        private static IGame demo = new DemoGameBuilder().BuildGame("test1", new VikingFactory(), "test2", new GauloisFactory());
        private static IGame small = new SmallGameBuilder().BuildGame("test1", new VikingFactory(), "test2", new GauloisFactory());
        private static IGame normal = new NormalGameBuilder().BuildGame("test1", new VikingFactory(), "test2", new GauloisFactory());

        [TestMethod]
        public void TestRounds() {
            this.TestRounds(demo);
            this.TestRounds(small);
            this.TestRounds(normal);
        }

        private void TestRounds(IGame game) {
            for(int i = 1; i <= game.MaxNbRound; i++) {
                Assert.AreEqual(i, game.CurrentRound);
                game.EndRound();
                Assert.IsTrue(game.Player2.Equals(game.CurrentPlayer));
                game.EndRound();
                Assert.IsTrue(game.Player1.Equals(game.CurrentPlayer));
            }
            Assert.IsTrue(game.IsEndOfGame());
        }

        [TestMethod]
        public void TestMap() {
            Assert.AreEqual(5, demo.Map.Size);
            Assert.AreEqual(10, small.Map.Size);
            Assert.AreEqual(15, normal.Map.Size);
        }

        [TestMethod]
        public void TestWinner() {
            this.TestWinner(demo);
            this.TestWinner(small);
            this.TestWinner(normal);
        }

        private void TestWinner(IGame game) {
            game.Player1.AddPoints(1000);
            while(!game.IsEndOfGame()) {
                game.EndRound();
                game.EndRound();
            }
            Assert.IsTrue(game.GetWinner().Equals(game.Player1));

            game.Player2.AddPoints(10000);
            Assert.IsTrue(game.GetWinner().Equals(game.Player2));
        }

        [TestMethod]
        public void TestSerializationGame() {
            Stream stream = File.Open("Game.sav", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, normal);
            stream.Close();

            stream = File.Open("Game.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            IGame savedGame = (IGame)formatter.Deserialize(stream);
            stream.Close();
            Assert.IsTrue(normal.Player1.Equals(savedGame.Player1));
            Assert.IsTrue(normal.Player2.Equals(savedGame.Player2));
            Assert.IsTrue(normal.CurrentPlayer.Equals(savedGame.CurrentPlayer));
            Assert.AreEqual(normal.CurrentRound, savedGame.CurrentRound);

            IMap map = normal.Map;
            IMap savedMap = savedGame.Map;
            Assert.AreEqual(map.Size, savedMap.Size);
            for(int i = 0; i < map.Size; i++) {
                for(int j = 0; j < map.Size; j++) {
                    Assert.IsInstanceOfType(savedMap.GetTile(new Point(i, j)), map.GetTile(new Point(i, j)).GetType());
                }
            }
            for(int i = 0; i < map.Size; i++) {
                for(int j = 0; j < map.Size; j++) {
                    List<IUnit> units = map.GetUnits(new Point(i, j));
                    List<IUnit> savedUnits = savedMap.GetUnits(new Point(i, j));
                    Assert.AreEqual(units.Count, savedUnits.Count);
                    for(int k = 0; k < savedUnits.Count; k++) {
                        Assert.IsTrue(units.Contains(savedUnits[k]));
                    }
                }
            }
        }
    }
}