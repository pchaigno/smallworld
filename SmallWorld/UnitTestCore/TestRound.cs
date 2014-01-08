using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;
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
            IGame game = new NormalGameBuilder().BuildGame("test1", new VikingFactory(), "test2", new GauloisFactory());
            int size = game.GetMap().GetSize();
            IRound round = game.GetRound();
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    IPoint pos = new Point(i, j);
                    if(round.IsCurrentPlayerPosition(pos)) {
                        IUnit unit = round.GetUnits(pos)[0];
                        List<IUnit> unitsL = new List<IUnit>();
                        unitsL.Add(unit);
                        round.SelectUnits(unitsL, pos);
                        IPoint destination = GetDestination(game, pos, unit);
                        Assert.IsTrue(round.SetDestination(destination));
                        round.ExecuteMove();

                        List<IUnit> units = game.GetMap().GetUnits(destination);
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
            IGame game = new NormalGameBuilder().BuildGame("test1", new VikingFactory(), "test2", new GauloisFactory());
            int size = game.GetMap().GetSize();
            IRound round = game.GetRound();
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    IPoint pos = new Point(i, j);
                    if(round.IsCurrentPlayerPosition(pos)) {
                        IUnit unit = round.GetUnits(pos)[0];
                        List<IUnit> unitsL = new List<IUnit>();
                        unitsL.Add(unit);
                        round.SelectUnits(unitsL, pos);
                        IPoint destination = GetDestination(game, pos, unit);
                        Assert.IsTrue(game.GetCurrentPlayer().Equals(game.GetPlayer1()));
                        IUnit enemy = game.GetPlayer2().CreateUnits(1)[0];
                        game.GetMap().PlaceUnit(enemy, destination);
                        Assert.IsTrue(round.SetDestination(destination));
                        Assert.IsTrue(game.GetMap().IsEnemyPosition(destination, unit));
                        round.ExecuteMove();

                        List<IUnit> unitsAtDestination = game.GetMap().GetUnits(destination);
                        List<IUnit> unitsAtOrigin = game.GetMap().GetUnits(pos);
                        if(round.GetLastMoveInfo() == "The fight ended with a draw (Grammar ??)") {
                            Assert.IsTrue(unitsAtOrigin.Contains(unit));
                            Assert.IsTrue(unitsAtDestination.Contains(enemy));
                        } else if(round.GetLastMoveInfo() == game.GetPlayer1().GetName() + " lost the fight.") {
                            Assert.IsTrue(!unitsAtOrigin.Contains(unit));
                            Assert.IsTrue(unitsAtDestination.Contains(enemy));
                        } else if(round.GetLastMoveInfo() == game.GetPlayer1().GetName() + " won the fight.") {
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

        private IPoint GetDestination(IGame game, IPoint currentPos, IUnit unit) {
            int size = game.GetMap().GetSize();
            int[] xOffsets = new int[4] {0, -1, 1, 0};
            int[] yOffsets = new int[4] {-1, 0, 0, 1};
            for(int i=0; i<4; i++) {
                int x = currentPos.X + xOffsets[i];
                int y = currentPos.Y + yOffsets[i];
                if(x>=0 && y>=0 && x<size && y<size) {
                    IPoint destination = new Point(x, y);
                    if(!(game.GetMap().GetSquare(destination) is ISea) && !game.GetMap().IsEnemyPosition(destination, unit)) {
                        return destination;
                    }
                }
            }
            Assert.Fail("nothing next to " + currentPos);
            return null;
        }
    }
}