using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitTestCore {

    [TestClass]
    public class TestRound {
        // Change to a few hundreds to test.
        private const int NB_TESTS = 10;

        [TestMethod]
        public void TestMultipleMovements() {
            // The movements are hightly dependent of the map
            // so we decided to test it a several times to be "sure" it works.
            // As a unit test it's really not great but from the C# we can't really do any better.
            for(int i = 0; i < NB_TESTS; i++) {
                this.TestMovement();
            }
        }

        public void TestMovement() {
            // Generates a new independent game each time.
            IGame game = new NormalGameBuilder().BuildGame("test1", new VikingFactory(), "test2", new GauloisFactory());
            int size = game.Map.Size;
            IRound round = game.Round;

            // Gets an unit of the current player:
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    IPoint pos = new Point(i, j);
                    if(round.IsCurrentPlayerPosition(pos)) {
                        IUnit unit = round.GetUnits(pos)[0];

                        // Selects it:
                        List<IUnit> unitsL = new List<IUnit>();
                        unitsL.Add(unit);
                        round.SelectUnits(unitsL, pos);

                        // Sets the destination point:
                        IPoint destination = GetDestination(game, pos, unit);
                        Assert.IsTrue(round.SetDestination(destination));

                        // Executes the movement and checks its result:
                        round.ExecuteMove();
                        List<IUnit> units = game.Map.GetUnits(destination);
                        Assert.IsTrue(units.Contains(unit));
                        return;
                    }
                }
            }
        }

        [TestMethod]
        public void TestMultipleAttackMovements() {
            // The attacks are hightly dependent of the map
            // so we decided to test it a great number of times to be "sure" it works.
            // As a unit test it's really not great but from the C# we can't really do any better.
            for(int i = 0; i < NB_TESTS; i++) {
                this.TestAttackMovement();
            }
        }

        public void TestAttackMovement() {
            // Generates a new independent game each time.
            IGame game = new NormalGameBuilder().BuildGame("test1", new VikingFactory(), "test2", new GauloisFactory());
            int size = game.Map.Size;
            IRound round = game.Round;

            // Gets an unit of the current player:
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    IPoint pos = new Point(i, j);
                    if(round.IsCurrentPlayerPosition(pos)) {
                        IUnit unit = round.GetUnits(pos)[0];

                        // Selects it:
                        List<IUnit> unitsL = new List<IUnit>();
                        unitsL.Add(unit);
                        round.SelectUnits(unitsL, pos);

                        // Sets the destination point and place an enemy unit on it:
                        IPoint destination = GetDestination(game, pos, unit);
                        Assert.IsTrue(game.CurrentPlayer.Equals(game.Player1));
                        IUnit enemy = game.Player2.CreateUnits(1)[0];
                        game.Map.PlaceUnit(enemy, destination);
                        Assert.IsTrue(round.SetDestination(destination));
                        Assert.IsTrue(game.Map.IsEnemyPosition(destination, unit));

                        // Executes the movement and checks its result:
                        round.ExecuteMove();
                        List<IUnit> unitsAtDestination = game.Map.GetUnits(destination);
                        List<IUnit> unitsAtOrigin = game.Map.GetUnits(pos);
                        // The results depend on the result from the fight:
                        switch(round.LastCombatResult) {
                            case CombatResult.DRAW:
                                Assert.IsTrue(unitsAtOrigin.Contains(unit));
                                Assert.IsTrue(unitsAtDestination.Contains(enemy));
                                break;
                            case CombatResult.LOSE:
                                Assert.IsFalse(unitsAtOrigin.Contains(unit));
                                Assert.IsTrue(unitsAtDestination.Contains(enemy));
                                break;
                            case CombatResult.WIN:
                                Assert.IsFalse(unitsAtDestination.Contains(enemy));
                                Assert.IsTrue(unitsAtOrigin.Contains(unit) || unitsAtDestination.Contains(unit));
                                break;
                            default:
                                Assert.Fail();
                                break;
                        }
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a "good" destination for the current selected unit.
        /// A "good" destination is a destination with no enemy on it that is not a sea position.
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="currentPos">The current position of the unit.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>The destination choosen.</returns>
        private IPoint GetDestination(IGame game, IPoint currentPos, IUnit unit) {
            int[] xOffsets = new int[4] {0, -1, 1, 0};
            int[] yOffsets = new int[4] {-1, 0, 0, 1};
            for(int i=0; i<4; i++) {
                IPoint destination = new Point(currentPos.X + xOffsets[i], currentPos.Y + yOffsets[i]);
                if(destination.isValid(game.Map.Size)) {
                    if(!(game.Map.GetTile(destination) is ISea) && !game.Map.IsEnemyPosition(destination, unit)) {
                        return destination;
                    }
                }
            }
            // We decided to fail in that case as it is hightly improbable that
            // no vacant destination point is available.
            // Indeed our map is made so that every position can be reached.
            Assert.Fail("nothing next to " + currentPos);
            return null;
        }
    }
}