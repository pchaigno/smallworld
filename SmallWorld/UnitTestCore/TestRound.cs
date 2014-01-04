using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTestCore {

    [TestClass]
    public class TestRound {
        private const int NB_TESTS = 10;

        [TestMethod]
        public void TestMultipleMovements() {
            for(int i = 0; i < NB_TESTS; i++) {
                this.TestMovement();
            }
        }

        public void TestMovement() {
            IGame game = new NormalGameBuilder().buildGame("test1", new VikingFactory(), "test2", new GauloisFactory());
            int size = game.getMap().getSize();
            IRound round = game.getRound();
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    Point pos = new Point(i, j);
                    if(round.isCurrentPlayerPosition(pos)) {
                        IUnit unit = round.getUnits(pos)[0];
                        round.selectUnit(unit, pos);
                        Point destination = getDestination(game, pos, unit);
                        Assert.IsTrue(round.setDestination(destination));
                        round.executeMove();

                        List<IUnit> units = game.getMap().getUnits(destination);
                        Assert.IsTrue(units.Contains(unit));
                        return;
                    }
                }
            }
        }

        [TestMethod]
        public void TestMultipleAttackMovements() {
            for(int i = 0; i < NB_TESTS; i++) {
                this.TestAttackMovement();
            }
        }

        public void TestAttackMovement() {
            IGame game = new NormalGameBuilder().buildGame("test1", new VikingFactory(), "test2", new GauloisFactory());
            int size = game.getMap().getSize();
            IRound round = game.getRound();
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    Point pos = new Point(i, j);
                    if(round.isCurrentPlayerPosition(pos)) {
                        IUnit unit = round.getUnits(pos)[0];
                        round.selectUnit(unit, pos);
                        Point destination = getDestination(game, pos, unit);
                        Assert.IsTrue(game.getCurrentPlayer().Equals(game.getPlayer1()));
                        IUnit enemy = game.getPlayer2().createUnits(1)[0];
                        game.getMap().placeUnit(enemy, destination);
                        Assert.IsTrue(round.setDestination(destination));
                        Assert.IsTrue(game.getMap().isEnemyPosition(destination, unit));
                        round.executeMove();

                        List<IUnit> unitsAtDestination = game.getMap().getUnits(destination);
                        List<IUnit> unitsAtOrigin = game.getMap().getUnits(pos);
                        if(round.getLastMoveInfo() == "The fight ended with a draw (Grammar ??)") {
                            Assert.IsTrue(unitsAtOrigin.Contains(unit));
                            Assert.IsTrue(unitsAtDestination.Contains(enemy));
                        } else if(round.getLastMoveInfo() == game.getPlayer1().getName() + " lost the fight.") {
                            Assert.IsTrue(!unitsAtOrigin.Contains(unit));
                            Assert.IsTrue(unitsAtDestination.Contains(enemy));
                        } else if(round.getLastMoveInfo() == game.getPlayer1().getName() + " won the fight.") {
                            Assert.IsTrue(!unitsAtDestination.Contains(enemy));
                            Assert.IsTrue(unitsAtOrigin.Contains(unit) || unitsAtDestination.Contains(unit));
                        } else {
                            Assert.Fail();
                        }
                        return;
                    }
                }
            }
        }

        private Point getDestination(IGame game, Point currentPos, IUnit unit) {
            int size = game.getMap().getSize();
            int[] xOffsets = new int[4] {0, -1, 1, 0};
            int[] yOffsets = new int[4] {-1, 0, 0, 1};
            for(int i=0; i<4; i++) {
                int x = currentPos.X + xOffsets[i];
                int y = currentPos.Y + yOffsets[i];
                if(x>=0 && y>=0 && x<size && y<size) {
                    Point destination = new Point(x, y);
                    if(!(game.getMap().getSquare(destination) is ISea) && !game.getMap().isEnemyPosition(destination, unit)) {
                        return destination;
                    }
                }
            }
            Assert.Fail("nothing next to " + currentPos);
            return new Point();
        }
    }
}