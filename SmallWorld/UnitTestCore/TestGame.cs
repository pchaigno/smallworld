using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SmallWorld;

namespace UnitTestCore {

    [TestClass]
    public class TestGame {
        private static IGame demo = new DemoGameBuilder().buildGame("test1", new VikingFactory(), "test2", new GauloisFactory());
        private static IGame small = new SmallGameBuilder().buildGame("test1", new VikingFactory(), "test2", new GauloisFactory());
        private static IGame normal = new NormalGameBuilder().buildGame("test1", new VikingFactory(), "test2", new GauloisFactory());

        [TestMethod]
        public void TestRounds() {
            this.TestRounds(demo);
            this.TestRounds(small);
            this.TestRounds(normal);
        }

        private void TestRounds(IGame game) {
            for(int i = 1; i <= game.getMaxNbRound(); i++) {
                Assert.AreEqual(i, game.getCurrentRound());
                game.endRound();
                Assert.IsTrue(game.getPlayer2().Equals(game.getCurrentPlayer()));
                game.endRound();
                Assert.IsTrue(game.getPlayer1().Equals(game.getCurrentPlayer()));
            }
            Assert.IsTrue(game.isEndOfGame());
        }

        [TestMethod]
        public void testMap() {
            Assert.AreEqual(5, demo.getMap().getSize());
            Assert.AreEqual(10, small.getMap().getSize());
            Assert.AreEqual(15, normal.getMap().getSize());
        }

        [TestMethod]
        public void testWinner() {
            this.testWinner(demo);
            this.testWinner(small);
            this.testWinner(normal);
        }

        private void testWinner(IGame game) {
            game.getPlayer1().addPoints(1000);
            while(!game.isEndOfGame()) {
                game.endRound();
                game.endRound();
            }
            Assert.IsTrue(game.getWinner().Equals(game.getPlayer1()));

            game.getPlayer2().addPoints(10000);
            Assert.IsTrue(game.getWinner().Equals(game.getPlayer2()));
        }
    }
}