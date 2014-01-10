using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SmallWorld;

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
    }
}